using UnityEngine;

public class Staff : Weapon
{
    public GameObject fireball;
    protected bool attacking = false;
    protected Animator animCtrl;
    protected float accum = 0;
    public Transform fireballPos;
    public Transform rotation;

    void Update()
    {
        if (attacking)
        {
            accum += Time.deltaTime; 
            if (accum > 1)
            {
                Shoot();
            }
        }
    }

    public override void Attack(Animator animCtrl)
    {
        this.animCtrl = animCtrl;
        animCtrl.SetInteger("WeaponType_int", 12);
        animCtrl.SetInteger("MeleeType_int", 2);
        attacking = true;
    }
    
    protected virtual void Shoot()
    {
        var staff = Instantiate(fireball, fireballPos.position, rotation.rotation).GetComponent<Fireball>();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            staff.DirectionVector = hit.point;
        }
        attacking = false;
        accum = 0;
    }
    
    
}
