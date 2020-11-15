using System;
using UnityEngine;

public class Sword : Weapon
{
    public CharacterController cc;
    private bool attacking = false;
    private int damagePerAttack = 25;
    private TypeOfDamage damageType = TypeOfDamage.Melee;
    private AudioSource audiosource;
    private float attackingCoolDown;
    public Sprite icon;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            attacking = true;
            attackingCoolDown = 0;
        }else if (attacking)
        {
            attackingCoolDown += Time.deltaTime;
            attacking = (attackingCoolDown < .5);
        }
        
    }

    public override void Attack(Animator animCtrl)
    {
        Debug.Log("SWORD ATACK");
        animCtrl.SetInteger("WeaponType_int", 12);
        animCtrl.SetInteger("MeleeType_int", 1);
        audiosource.Play();
    }
    
    public void OnTriggerEnter(Collider col)
    {

        if (string.Compare(col.gameObject.tag, "Enemy", StringComparison.Ordinal) == 0 && attacking)
        {
            col.gameObject.GetComponent<EnemyManager>().Attacked(damagePerAttack, damageType);
        }
    }

    public override Sprite GetIcon()
    {
        return icon;
    }
}
