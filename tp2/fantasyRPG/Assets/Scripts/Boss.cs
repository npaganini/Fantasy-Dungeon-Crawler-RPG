using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Boss : EnemyManager
{
    public EnemyStaff staff;
    public EnemyBow bow;
    public Weapon[] weapons;
    public Weapon equipped;
    public ParticleSystem[] particles;

    private float switchCooldown = 5f;
    private float timer = 0f;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        enemyType = TypeOfDamage.Melee;
        life = 200;
        equipped = weapons[0];
    }

    // Update is called once per frame
    public override void Update()
    {
        timer += Time.deltaTime;
        if(timer >= switchCooldown)
        {
            timer = 0f;
            SwitchDamageType();
        }
        base.Update();
    }

    private void SwitchDamageType()
    {
        equipped.gameObject.SetActive(false);
        enemyType = (TypeOfDamage)UnityEngine.Random.Range(0, Enum.GetValues(typeof(TypeOfDamage)).Length);
        equipped = weapons[(int) enemyType];
        equipped.gameObject.SetActive(true);
        particles[(int)enemyType].Play();
    }

    protected override void Attack()
    {
        if (!attacking)
        {
            Vector3 pos;
            switch (enemyType)
            {
                case TypeOfDamage.Magic:
                    pos = new Vector3(player.transform.position.x,
                    player.transform.position.y + 2, player.transform.position.z);
                    staff.GetComponent<EnemyStaff>().Attack(anmCtrl, pos);
                    attacking = true;
                    break;
                case TypeOfDamage.Melee:
                    anmCtrl.SetInteger("WeaponType_int", 12);
                    anmCtrl.SetInteger("MeleeType_int", 1);
                    PlayerLogic.Attacked(enemyDamage);
                    attacking = true;
                    break;
                case TypeOfDamage.Ranged:
                    pos = new Vector3(player.transform.position.x,
                    player.transform.position.y + 2, player.transform.position.z);
                    bow.GetComponent<EnemyBow>().Attack(anmCtrl, pos);
                    attacking = true;
                    break;
                default: break;
            }

        }
        
    }

    protected override bool LineOfSight()
    {
        var distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= fov)
        {
            switch (enemyType)
            {
                case TypeOfDamage.Magic:
                case TypeOfDamage.Ranged:

                    {
                        if (distance < 160)
                        {
                            LookAt();
                            Attack();
                            return false;
                        }
                        return true;
                    }
                case TypeOfDamage.Melee:
                    if (distance < 2.5)
                    {
                        Attack();
                        return false;
                    }
                    return true;
            }
            if (distance <= fov)
            {
                if (distance < 160)
                {
                    LookAt();
                    Attack();
                    return false;
                }
                return true;
            }
            return false;
        }
        return false;
    }
}
