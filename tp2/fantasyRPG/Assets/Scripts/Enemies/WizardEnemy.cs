using System.Linq;
using UnityEngine;

public class WizardEnemy : EnemyManager
{
    public GameObject staff;
    public float range;

    public override void Start()
    {
        base.Start();
        enemyType = TypeOfDamage.Magic;
    }

    protected override bool LineOfSight ()
    {
        var distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < range)
        {
            var playerPos = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z);
             RaycastHit[] hits = Physics.
                RaycastAll(transform.position, playerPos - transform.position, range)
                .OrderBy(h=>h.distance).ToArray();
            foreach (var hit in hits)
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    break;
                }
                if (hit.transform.gameObject.CompareTag("Wall") || hit.transform.gameObject.CompareTag("Door"))
                {
                    return true;
                }
            }
            LookAt();
            Attack();
            return false;
        }
        return true;
    }

    protected override void Attack()
    {
        if (!attacking)
        {
            Vector3 pos = new Vector3(player.transform.position.x, 
                player.transform.position.y + 2 , player.transform.position.z);
            staff.GetComponent<EnemyStaff>().Attack(anmCtrl, pos);
            attacking = true;
        }
    }
}