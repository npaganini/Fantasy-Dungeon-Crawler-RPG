using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBow : Bow
{

    private Vector3 direction;


    public void Attack(Animator animCtrl, Vector3 direction)
    {
        this.direction = direction;
        this.animCtrl = animCtrl;
        //animCtrl.Play("BowShoot");
        animCtrl.SetInteger("WeaponType_int", 11);
        animCtrl.SetBool("Shoot_cross", true);
        attacking = true;
    }
    
    protected override void Shoot()
    {
        var arrowG = Instantiate(arrow, shotPos.position, rotation.rotation).GetComponent<Arrow>();
        arrowG.DirectionVector = direction;
        attacking = false;
        accum = 0;
    }
    
    
}
