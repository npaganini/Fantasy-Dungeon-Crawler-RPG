using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon
{
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void Attack(Animator animCtrl)
    {
        animCtrl.SetInteger("WeaponType_int", 12);
        animCtrl.SetInteger("MeleeType_int", 2);
    }
}
