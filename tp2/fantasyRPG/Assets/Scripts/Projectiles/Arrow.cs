using System;
using UnityEngine;

public class Arrow : Projectile
{
    private const int DamagePerAttack = 20;
    private const TypeOfDamage DamageType = TypeOfDamage.Ranged;

    public override void OnTriggerEnter(Collider col)
    {
        if (string.Compare(col.gameObject.tag, "Enemy", StringComparison.Ordinal) == 0)
        {
            col.gameObject.GetComponent<EnemyManager>().Attacked(DamagePerAttack, DamageType);
        }
        Destroy(gameObject);
    }

    protected override void Stuck()
    {
        Rb.constraints =  RigidbodyConstraints.FreezePosition;
    }
}
