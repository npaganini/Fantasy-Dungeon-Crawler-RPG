using UnityEngine;

public class Arrow : Projectile
{
    private const int DamagePerAttack = 20;
    private const TypeOfDamage DamageType = TypeOfDamage.Ranged;

    public override void OnTriggerEnter(Collider col)
    {
        // Debug.Log(col.gameObject.tag);
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerLogic>().Attacked(10);
            transform.parent = col.transform;
            Stuck();

        }
        else if (col.gameObject.CompareTag("Enemy"))
        {
            transform.parent = col.transform;
            if (col.gameObject.name == "Head_jnt")
            {
                col.gameObject.GetComponentInParent<EnemyManager>().Attacked(DamagePerAttack * 2, DamageType);
            }
            else
            {
                col.gameObject.GetComponent<EnemyManager>().Attacked(DamagePerAttack, DamageType);
            }
            Stuck();
        }
        else if (col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Door"))
        {
            transform.position = Vector3.MoveTowards(transform.position, DirectionVector, 0.7f);
            Stuck();
        }
    }

    protected override void Stuck()
    {
        Hit = true;
        Rb.isKinematic = true;
        Rb.constraints =  RigidbodyConstraints.FreezePosition;
        Destroy(GetComponent<BoxCollider>());
        Destroy(GetComponent<MeshCollider>());
    }
}
