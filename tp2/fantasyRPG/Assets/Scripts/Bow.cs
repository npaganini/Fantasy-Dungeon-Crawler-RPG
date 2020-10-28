using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    public GameObject arrow;
    private bool attacking = false;
    private Animator animCtrl;
    private float accum = 0;
    public Transform shotPos;

    public Transform rotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(shotPos.position);
        if (attacking)
        {
            accum += Time.deltaTime; 
            if (accum >1)
            {
                Instantiate(arrow, shotPos.position, rotation.rotation);
                attacking = false;
                accum = 0;
            }
        }
        
    }

    public override void Attack(Animator animCtrl)
    {
        this.animCtrl = animCtrl;
        //animCtrl.Play("BowShoot");
        animCtrl.SetInteger("WeaponType_int", 11);
        animCtrl.SetBool("Shoot_b", true);
        attacking = true;
        
    }
    
}
