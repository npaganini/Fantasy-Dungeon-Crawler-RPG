using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public Slider healthBar;
    public float life;

    private bool _onCoolDown = false;
    private float _cooldown = 0;
    // private float _gravity = 30.87f;
    protected TypeOfDamage enemyType = TypeOfDamage.Melee;
    public GameObject player;
    public float damping = 6.0f;
    public Animator anmCtrl;
    public PlayerLogic playerLogic;
    public float enemyDamage;
    public GameObject itemDrop;
    protected bool attacking;
    protected float attackingCoolDown;
    public float coolDown = 2;

    private NavMeshAgent _agent;

    protected bool isDead = false;
    private float accumDead = 0;
    public bool isActive = false;
    
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        anmCtrl = GetComponent<Animator>();
        playerLogic = player.GetComponent<PlayerLogic>();
        _agent = GetComponent<NavMeshAgent>();
    }
    

    // Update is called once per frame
    public virtual void Update()
    {
        if (!isActive)
        {
            return;
        }
        if (isDead)
        {
            accumDead += Time.deltaTime;
            if (accumDead > 4)
            {
                DropItem();
                gameObject.SetActive(false);
            }
            return;
        }
        if (life <= 0)
        {
            anmCtrl.SetBool("Dead", true);
            _agent.SetDestination(transform.position);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            Destroy(GetComponent<BoxCollider>());
            isDead = true;
        }
        else
        {
            if (attacking)
            {
                attackingCoolDown += Time.deltaTime;
                if (attackingCoolDown > coolDown)
                {
                    attacking = false;
                    attackingCoolDown = 0;
                }
            }
            if (LineOfSight())
            {
                Chase();
            }
            else
            {
                _agent.SetDestination(transform.position);
                anmCtrl.SetFloat("Speed_f", 0);
            }

            if (_onCoolDown)
            {
                _cooldown += Time.deltaTime;
                _onCoolDown = !(_cooldown > .5);
            }
        }
    }

    protected void Chase ()
    {
        var moveDirection = transform.forward;
        _agent.SetDestination(player.transform.position);
        anmCtrl.SetFloat("Speed_f", 2);
         
    }
    
    protected void LookAt(){
        Quaternion rotation= Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }

    protected virtual bool LineOfSight ()
    {
        var distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < 2.5)
        {
            Attack();
            return false;
        }
        return true;
    }

    protected virtual void Attack()
    {
        if (!attacking)
        {
            anmCtrl.SetInteger("WeaponType_int", 12);
            anmCtrl.SetInteger("MeleeType_int", 1);
            playerLogic.Attacked(enemyDamage);
            attacking = true;
        }
    }

    private void UpdateHealth()
    {
        healthBar.value = life;
    }

    public void Attacked(float damage, TypeOfDamage damageType)
    {
        if (!_onCoolDown)
        {
            life -= getDamageAfterTypeComparison(damage, damageType);
            // Debug.Log(life);
            UpdateHealth();
            _onCoolDown = true;
            _cooldown = 0;
        }
    }
    
    public void Attacked(float damage)
    {
        if (!_onCoolDown)
        {
            life -= damage;
            Debug.Log(life);
            UpdateHealth();
            _onCoolDown = true;
            _cooldown = 0;
        }
    }

    private bool WasItHeadshot()
    {
        return false;
    }

    private float getDamageAfterTypeComparison(float damage, TypeOfDamage damageType)
    {
        switch (damageType)
        {
            case TypeOfDamage.Melee:
                if (enemyType != damageType)
                {
                    if (enemyType == TypeOfDamage.Ranged)
                    {
                        return damage * 2;
                    }
                    return damage / 2;
                }
                return damage;
            case TypeOfDamage.Ranged:
                if (enemyType != damageType)
                {
                    if (enemyType == TypeOfDamage.Magic)
                    {
                        return damage * 2;
                    }
                    return damage / 2;
                }
                return damage;
            case TypeOfDamage.Magic:
                if (enemyType != damageType)
                {
                    if (enemyType == TypeOfDamage.Melee)
                    {
                        return damage * 2;
                    }
                    return damage / 2;
                }
                return damage;
        }
        return damage;
    }

    public void Kill()
    {
        life = 0;
        UpdateHealth();
    }

    public virtual void Activate()
    {
        isActive = true;
    }

    public void PlayerWin()
    {
        enabled = false;
    }

    private void DropItem()
    {
        var t = this.transform;
        var p = t.position;
        Instantiate(itemDrop, new Vector3(p.x, p.y + 1.43f, p.z), t.rotation);
    }
}
