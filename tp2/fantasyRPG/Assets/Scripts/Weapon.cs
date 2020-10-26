using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    
    virtual public void Attack(Animator anmCtrl) { }
}
