using UnityEngine;

public class Staff : Weapon
{
    public GameObject fireball;
    private bool attacking = false;
    private Animator animCtrl;
    private float accum = 0;
    public Transform fireballPos;
    public Transform rotation;

    void Update()
    {
        if (attacking)
        {
            accum += Time.deltaTime; 
            if (accum > 1)
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
    }

    public override void Attack(Animator animCtrl)
    {
        this.animCtrl = animCtrl;
        animCtrl.SetInteger("WeaponType_int", 12);
        animCtrl.SetInteger("MeleeType_int", 2);
        attacking = true;
    }
}
