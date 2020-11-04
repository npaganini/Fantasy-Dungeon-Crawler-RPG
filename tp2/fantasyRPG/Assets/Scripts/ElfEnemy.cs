
using UnityEngine;
using UnityEngine.Animations;

public class ElfEnemy : EnemyManager
{
    public GameObject bow;
    
    protected override bool LineOfSight ()
    {
        var distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= fov) {
            if (distance < 160)
            {
                LookAt();
                Attack();
                return false;
            }
            return true;
        }
        return false;
    }

    private void Attack()
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
