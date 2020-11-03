using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    
    private float life = 100;

    private bool onCoolDown = false;
    private float cooldown = 0;
    private CharacterController cc;
    private float gravity = 30.87f;
    private TypeOfDamage enemyType = TypeOfDamage.Melee; // todo: change to different enemy types
    public float speed = 2f;
    public GameObject player;
    public float Damping= 6.0f;
    public float fov= 160.0f;
    public Animator anmCtrl;
    public PlayerLogic PlayerLogic;
    public float enemyDamage;

    private bool attacking;
    private float attackingCoolDown;

    // Start is called before the first frame update
    void Start()
    {
        anmCtrl = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        PlayerLogic = player.GetComponent<PlayerLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            anmCtrl.SetBool("Dead", true);
        }
        else
        {
            if (attacking)
            {
                attackingCoolDown += Time.deltaTime;
                attacking = !(attackingCoolDown > 2);
            }
            if (LineOfSight())
            {
                Chase();
            }
            else
            {
                anmCtrl.SetFloat("Speed_f", 0);
                
            }

            if (onCoolDown)
            {
                cooldown += Time.deltaTime;
                onCoolDown = !(cooldown > .5);
            }
        }
    }
    private void Chase ()
    {
        LookAt();
        var moveDirection = transform.forward;
     
        moveDirection *= speed;
     
        moveDirection.y -= gravity * Time.deltaTime;
        cc.Move(moveDirection * Time.deltaTime);
        anmCtrl.SetFloat("Speed_f", speed);
         
    }
    
    private void LookAt(){
        Quaternion rotation= Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);
    }
    
    public bool LineOfSight ()
    {
        var distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= fov) {
            if (distance < 2.5)
            {
                Attack();
                return false;
            }
            return true;
        }
        return false;
    }

    private void Attack()
    {
        if (!attacking)
        {
            anmCtrl.SetInteger("WeaponType_int", 12);
            anmCtrl.SetInteger("MeleeType_int", 1);
            PlayerLogic.Attacked(enemyDamage);
            attacking = true;
            attackingCoolDown = 0;
        }
    }

    public void Attacked(float damage, TypeOfDamage damageType)
    {
        if (!onCoolDown)
        {
            life -= getDamageAfterTypeComparison(damage, damageType);
            Debug.Log(life);
            onCoolDown = true;
            cooldown = 0;
        }
    }

    private float getDamageAfterTypeComparison(float damage, TypeOfDamage damageType)
    {
        switch (damageType)
        {
            case TypeOfDamage.Melee:
                if (enemyType != damageType)
                {
                    if (enemyType == TypeOfDamage.Ranged)
                    {
                        return damage * 2;
                    }
                    return damage / 2;
                }
                return damage;
            case TypeOfDamage.Ranged:
                if (enemyType != damageType)
                {
                    if (enemyType == TypeOfDamage.Magic)
                    {
                        return damage * 2;
                    }
                    return damage / 2;
                }
                return damage;
            case TypeOfDamage.Magic:
                if (enemyType != damageType)
                {
                    if (enemyType == TypeOfDamage.Melee)
                    {
                        return damage * 2;
                    }
                    return damage / 2;
                }
                return damage;
        }
        return damage;
    }
}
