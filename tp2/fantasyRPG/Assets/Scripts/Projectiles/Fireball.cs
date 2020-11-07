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
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyManager>().Attacked(DamagePerAttack, DamageType);
        } else if (other.gameObject.CompareTag("Player")) 
        {

            other.gameObject.GetComponent<PlayerLogic>().Attacked(15); 
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
