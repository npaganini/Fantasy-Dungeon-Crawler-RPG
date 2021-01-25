using UnityEngine;

public class Fireball : Projectile
{
    private const int DamagePerAttack = 32;
    private const TypeOfDamage DamageType = TypeOfDamage.Magic;
    public GameObject explosionEffect;

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.name == "Head_jnt")
            {
                other.gameObject.GetComponentInParent<EnemyManager>().Attacked(DamagePerAttack * 2, DamageType);
            }
            else
            {
                other.gameObject.GetComponent<EnemyManager>().Attacked(DamagePerAttack, DamageType);
            }
            Stuck();
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerLogic>().Attacked(15); 
            Stuck();
        }
        else if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Door"))
        {
            Stuck();
        }
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
        Hit = true;
        Explode();
        Destroy(gameObject);
        
    }
}
