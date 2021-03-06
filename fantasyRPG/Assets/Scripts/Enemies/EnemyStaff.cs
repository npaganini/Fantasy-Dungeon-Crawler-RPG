using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStaff : Staff
{

    private Vector3 direction;

    void Start()
    {
        audiosource = gameObject.GetComponent<AudioSource>();
    }
    public void Attack(Animator animCtrl, Vector3 direction)
    {
        this.direction = direction;
        this.animCtrl = animCtrl;
        //animCtrl.Play("BowShoot");
        animCtrl.SetInteger("WeaponType_int", 11);
        animCtrl.SetBool("Shoot_cross", true);
        if(audiosource != null)
            audiosource.Play();
        attacking = true;
    }
    
    protected override void Shoot()
    {
        
        var staff = Instantiate(fireball, fireballPos.position, rotation.rotation).GetComponent<Fireball>();
        staff.DirectionVector = direction;
        attacking = false;
        accum = 0;
    }
    
    
}