using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Sword : Weapon
{
    public CharacterController cc;
    private bool attacking = false;
    private int damagePerAttack = 50;
    private TypeOfDamage damageType = TypeOfDamage.Melee;

    private float attackingCoolDown;
    // Start is called before the first frame update
    void Start()
    {
        
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
        animCtrl.SetInteger("WeaponType_int", 11);
        animCtrl.SetInteger("MeleeType_int", 1);
    }
    
    public void OnTriggerEnter(Collider col)
    {

        if (string.Compare(col.gameObject.tag, "Enemy", StringComparison.Ordinal) == 0 && attacking)
        {
            col.gameObject.GetComponent<EnemyManager>().Attacked(damagePerAttack, damageType);
        }
    }
    
}
