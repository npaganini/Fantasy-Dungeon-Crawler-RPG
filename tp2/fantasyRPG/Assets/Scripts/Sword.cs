using System;
using UnityEngine;

public class Sword : Weapon
{
    public CharacterController cc;
    private bool attacking = false;
    private const int DamagePerAttack = 25;
    private const TypeOfDamage DamageType = TypeOfDamage.Melee;
    private AudioSource audiosource;
    private float attackingCoolDown;
    public Sprite icon;

    // Start is called before the first frame update
    private void Awake()
    {
        audiosource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            attacking = true;
            attackingCoolDown = 0;
        }else if (attacking)
        {
            attackingCoolDown += Time.deltaTime;
            attacking = (attackingCoolDown < .5);
        }
    }

    public override void Attack(Animator animCtrl)
    {
        if (audiosource != null)
            audiosource.volume = PlayerPrefs.GetFloat("vol");
        Debug.Log("SWORD ATTACK");
        animCtrl.SetInteger("WeaponType_int", 12);
        animCtrl.SetInteger("MeleeType_int", 1);
        audiosource.Play();
    }
    
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy") && attacking)
        {
            if (col.gameObject.name == "Head_jnt")
            {
                col.gameObject.GetComponentInParent<EnemyManager>().Attacked(DamagePerAttack * 2, DamageType);
            }
            else
            {
                col.gameObject.GetComponent<EnemyManager>().Attacked(DamagePerAttack, DamageType);
            }
        }
    }

    public override Sprite GetIcon()
    {
        return icon;
    }
}
