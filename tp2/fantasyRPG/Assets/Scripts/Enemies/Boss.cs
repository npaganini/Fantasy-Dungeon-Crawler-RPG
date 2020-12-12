using System;
using System.Linq;
using UnityEngine;



public class Boss : EnemyManager
{
    public EnemyStaff staff;
    public EnemyBow bow;
    public Weapon[] weapons;
    public Weapon equipped;
    public ParticleSystem[] particles;
    protected AudioSource audiosource;
    public BossMusic bossMusic;
    private float switchCooldown = 7f;
    private float timer = 0f;
    public Spikes trap;
    public float range;
    private bool sendWin = false;

    // Start is called before the first frame update
    public override void Start()
    {
        audiosource = GetComponent<AudioSource>();
        base.Start();
        enemyType = TypeOfDamage.Melee;
        equipped = weapons[0];
    }

    // Update is called once per frame
    public override void Update()
    {
        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer >= switchCooldown)
            {
                timer = 0f;
                SwitchDamageType();
            }

            if (isDead && !sendWin)
            {
                sendWin = true;
                GameManager.Instance.Win();
                EnemyManager[] enemies = FindObjectsOfType<EnemyManager>();
                foreach (var enemy in enemies)
                {
                    enemy.PlayerWin();
                }
                PlayerLogic.Win();
            }
            base.Update();
        }
    }

    private void SwitchDamageType()
    {
        audiosource.Play();
        equipped.gameObject.SetActive(false);
        enemyType = (TypeOfDamage)UnityEngine.Random.Range(0, Enum.GetValues(typeof(TypeOfDamage)).Length);
        equipped = weapons[(int) enemyType];
        equipped.gameObject.SetActive(true);
        particles[(int)enemyType].Play();
    }

    protected override void Attack()
    {
        if (!attacking)
        {
            Vector3 pos;
            switch (enemyType)
            {
                case TypeOfDamage.Magic:
                    pos = new Vector3(player.transform.position.x,
                    player.transform.position.y + 2, player.transform.position.z);
                    staff.GetComponent<EnemyStaff>().Attack(anmCtrl, pos);
                    attacking = true;
                    break;
                case TypeOfDamage.Melee:
                    anmCtrl.SetInteger("WeaponType_int", 12);
                    anmCtrl.SetInteger("MeleeType_int", 1);
                    PlayerLogic.Attacked(enemyDamage);
                    attacking = true;
                    break;
                case TypeOfDamage.Ranged:
                    pos = new Vector3(player.transform.position.x,
                    player.transform.position.y + 2, player.transform.position.z);
                    bow.GetComponent<EnemyBow>().Attack(anmCtrl, pos);
                    attacking = true;
                    break;
                default: break;
            }

        }
        
    }

    protected override bool LineOfSight()
    {
        var distance = Vector3.Distance(player.transform.position, transform.position);
        switch (enemyType)
        {
            case TypeOfDamage.Magic:
            case TypeOfDamage.Ranged:
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
            case TypeOfDamage.Melee:
                if (distance < 2.5)
                {
                    Attack();
                    return false;
                }
                return true;
        }
        return true;
    }

    public override void Activate()
    {
        base.Activate();
        player.GetComponent<PlayerLogic>().dungeonAmbience.Stop();
        bossMusic.PlayBossMusic();
        trap.active = false;
    }
}
