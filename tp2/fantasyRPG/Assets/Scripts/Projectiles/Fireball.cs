using System;
using UnityEngine;

public class Fireball : Projectile
{
    private const int DamagePerAttack = 35;
    private const float MaxTimeStuck = 2.5f;
    private const TypeOfDamage DamageType = TypeOfDamage.Magic;
    private float _timeStuck = 0;
    public GameObject explosionEffect;

    public override void OnTriggerEnter(Collider other)
    {
        if (string.Compare(other.gameObject.tag, "Enemy", StringComparison.Ordinal) == 0)
        {
            other.gameObject.GetComponent<EnemyManager>().Attacked(DamagePerAttack, DamageType);
        }
        Explode();
        Destroy(gameObject);
    }

    private void Explode()
    {
        // do explosion animation
        var t = transform;
        GameObject explosion = Instantiate(explosionEffect, t.position, t.rotation);
        Destroy(explosion, 0.9f);
    }

    protected override void Stuck()
    {
        // Rb.constraints =  RigidbodyConstraints.FreezePosition;
        if (_timeStuck == 0)
        {
            Explode();
        }
        _timeStuck +=  (0.5f * Time.deltaTime);
        // gameObject.GetComponentInParent<BoxCollider>().enabled = false;
        if (_timeStuck > MaxTimeStuck)
        {
            Destroy(gameObject);
        }
    }
}
