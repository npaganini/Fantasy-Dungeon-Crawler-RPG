
using UnityEngine;
using UnityEngine.Animations;

public class ElfEnemy : EnemyManager
{
    public GameObject bow;
    public float range;

    public override void Start()
    {
        base.Start();
        enemyType = TypeOfDamage.Ranged;
    }

    protected override bool LineOfSight ()
    {
        var distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= fov) {
            if (distance < range)
            {
                LookAt();
                Attack();
                return false;
            }
            return true;
        }
        return false;
    }

    protected override void Attack()
    {
        if (!attacking)
        {
            Vector3 pos = new Vector3(player.transform.position.x, 
                player.transform.position.y + 2 , player.transform.position.z);
            bow.GetComponent<EnemyBow>().Attack(anmCtrl, pos);
            attacking = true;
        }
    }
    
    
}
