using System;
using UnityEngine;

public class Arrow : Projectile
{
    private const int DamagePerAttack = 30;
    private const TypeOfDamage DamageType = TypeOfDamage.Ranged;

    public override void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.tag);
        
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerLogic>().Attacked(5);
           
        }else if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<EnemyManager>().Attacked(20, DamageType);
        }
        
        Destroy(gameObject);
    }

    protected override void Stuck()
    {
        Rb.constraints =  RigidbodyConstraints.FreezePosition;
        Destroy(gameObject);
    }
}
