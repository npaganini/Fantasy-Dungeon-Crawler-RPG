using UnityEngine;

public class Bow : Weapon
{
    public GameObject arrow;
    protected bool attacking = false;
    protected Animator animCtrl;
    protected float accum = 0;
    public Transform shotPos;
    public Transform rotation;
    protected AudioSource audiosource;
    public Sprite icon;

    void Start()
    {
        audiosource = gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        //Debug.Log(shotPos.position);
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
        //animCtrl.Play("BowShoot");
        animCtrl.SetInteger("WeaponType_int", 11);
        animCtrl.SetBool("Shoot_cross", true);
        audiosource.Play();
        attacking = true;
    }

    protected virtual void Shoot()
    {
     
        var arrowG = Instantiate(arrow, shotPos.position, rotation.rotation).GetComponent<Arrow>();
        audiosource.Play();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            arrowG.DirectionVector = hit.point;
        }
        attacking = false;
        accum = 0;
    }

    public override Sprite GetIcon()
    {
        return icon;
    }
}
