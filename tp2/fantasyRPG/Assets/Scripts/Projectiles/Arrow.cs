using UnityEngine;

public class Arrow : Projectile
{
    private const int DamagePerAttack = 20;
    private const TypeOfDamage DamageType = TypeOfDamage.Ranged;

    public override void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.tag);
        
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerLogic>().Attacked(10);
        }
        else if (col.gameObject.CompareTag("Enemy"))
        {
            if (col.gameObject.name == "Head_jnt")
            {
                col.gameObject.GetComponentInParent<EnemyManager>().Attacked(DamagePerAttack * 2, DamageType);
            }
            else
            {
                col.gameObject.GetComponent<EnemyManager>().Attacked(DamagePerAttack, DamageType);
            }
        }
        Destroy(gameObject);
    }

    protected override void Stuck()
    {
        Rb.constraints =  RigidbodyConstraints.FreezePosition;
        TimeStuck +=  (0.5f * Time.deltaTime);
        // gameObject.GetComponentInParent<BoxCollider>().enabled = false;
        if (TimeStuck > MaxTimeStuck)
        {
            Destroy(gameObject);
        }
    }
}
