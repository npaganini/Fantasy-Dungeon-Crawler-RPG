using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    
    public virtual void Attack(Animator anmCtrl) { }

    public abstract Sprite GetIcon();
}
