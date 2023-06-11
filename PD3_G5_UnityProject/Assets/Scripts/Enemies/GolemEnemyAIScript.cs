using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.ProBuilder;

public class GolemEnemyAIScript : ParentEnemyIAScript
{


    [SerializeField] LayerMask obstacleMask;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject previewPrefab;

    Vector3 distanceToPlayer;
    [SerializeField] UnityEvent<GameObject> objectIsDead;

    //SHOOTING 
    [Header("Forces")]
    [SerializeField] float maxShootForce;
    [SerializeField] float maxUpwardForce;
    float shootForce;
    float upwardForce;
    [SerializeField] Transform bulletOrigin;
    Vector3 directionWithoutSpread;
    float xSpread;
    float ySpread;
    float zSpread;

    [Header("Stats")]
    [SerializeField] float timeBetweenShooting;
    [SerializeField] float spread;
    [SerializeField] float reloadTime;
    [SerializeField] float timeBetweenShots;
    [SerializeField] float bulletDamage;
    [SerializeField] int bulletsPerTap;
    [SerializeField] bool allowButtonHold;


    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot;
    [SerializeField] bool allowInvoke;
    Queue<GameObject> bulletPool;
    Vector3 shootingPoint;
    GameObject previewBullet;



    enum State { IDLE, CHASE, ATTACK, SHOOTING, HIT, DIE }
    [SerializeField] State currentState;
    [SerializeField] float secondsToSetEnemy;
    [SerializeField] float secondsToCDMeleeAttack;
    private bool canAttack = true;

    [Header("IDLE")]
    State lastState;


    [Header("CHASE")]
    [SerializeField] float RANGED_RANGE;
    [SerializeField] float MELEE_RANGE;
    [SerializeField] float CHASE_RANGE;



    [Header("ATTACK")]
    [SerializeField] float damage;
    float lastTimeCollisioned;


    [Header("HIT")]
    float lastCheckedHealth;

    [Header("DIE")]
    [SerializeField] float fadeSpeed;
    [SerializeField] MeshRenderer enemyRenderer;


    private bool enemySetted = false;


    [Header("FMOD")]
    public StudioEventEmitter AttackRangedEmitter;
    public StudioEventEmitter AttackMeleeEmitter;

    [Header("Animator")] 
    [SerializeField] private Animator enemyAnimator;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        lastCheckedHealth = GetComponent<EnemyHealthScript>().GetCurrentHealth();
        currentState = State.IDLE;
        
    }
    override public void Start()
    {
        base.Start();
        previewBullet = Instantiate(previewPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        previewBullet.SetActive(false);
        enemyAnimator.SetBool("Idle", true);
        bulletPool = new Queue<GameObject>();
        GameObject bullets = new GameObject("GolemBullets");
        for (int i = 0; i < 10; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
            bullet.transform.parent = bullets.transform;
        }

        bulletsLeft = 10;
        readyToShoot = true;
        shooting = false;
    }

    void Update()
    {
        if (enemySetted)
        {
            distanceToPlayer = player.transform.position - transform.position;
            switch (currentState)
            {
                case State.ATTACK:
                    //transform.LookAt(new Vector3(player.transform.position.x, 0f, player.transform.position.z), Vector3.up);
                    lastState = State.ATTACK;
                    updateAttack();
                    ChangeFromAttack();
                    break;
                case State.SHOOTING:
                    lastState = State.SHOOTING;
                    break;
                case State.IDLE:
                    lastState = State.IDLE;
                    updateIdle();
                    ChangeFromIdle();
                    break;
                case State.CHASE:
                    transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), Vector3.up);
                    lastState = State.CHASE;
                    updateChase();
                    ChangeFromChase();
                    break;
                case State.HIT:
                    updateHit();
                    ChangeFromHit();
                    break;
                case State.DIE:
                    updateDie();
                    break;
            }
        }

    }


    void updateIdle() { }

    void ChangeFromIdle()
    {
        //seesPlayer() &&
        if (PlayerInChaseRange())
        {
            currentState = State.CHASE;
            enemyAnimator.SetBool("Idle", false);
            enemyAnimator.SetBool("Chase", true);
        }
        CheckHit();
    }


    bool PlayerInMeleeRange()
    {
        return (distanceToPlayer).magnitude < MELEE_RANGE && (distanceToPlayer).magnitude < CHASE_RANGE && (distanceToPlayer).magnitude < RANGED_RANGE;
    }
    bool PlayerInRangedRange()
    {
        return (distanceToPlayer).magnitude < RANGED_RANGE && (distanceToPlayer).magnitude > MELEE_RANGE && (distanceToPlayer).magnitude > CHASE_RANGE;
    }    
    bool PlayerInChaseRange()
    {
        return ((distanceToPlayer).magnitude < CHASE_RANGE && (distanceToPlayer).magnitude > MELEE_RANGE) || (distanceToPlayer).magnitude > RANGED_RANGE;
    }

    bool seesPlayer()
    {
        float playerDistance = (player.transform.position - transform.position).magnitude;

        if (Vector3.Angle(transform.forward, distanceToPlayer.normalized) <= 15)
        {
            Ray r = new Ray(transform.position, distanceToPlayer.normalized);
            if (Physics.Raycast(r, out RaycastHit hitInfo, playerDistance, obstacleMask))
            {
                return false;
            }
            return true;
        }

        return false;

    }

    void updateChase()
    {
        if (agent.isStopped)
            agent.isStopped = !agent.isStopped;

        if (!isTrapped)
            agent.SetDestination(player.transform.position);
    }

    void ChangeFromChase()
    {
        if (PlayerInRangedRange() || PlayerInMeleeRange())  
        {
            currentState = State.ATTACK;
        }
        CheckHit();
    }



    IEnumerator EnemyCollision()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        int i = 0;
        while (i < 5f)
        {
            player.GetComponent<CharacterController>().Move(direction * 1f);
            i++;
            yield return new WaitForEndOfFrame();
        }

    }

    void updateAttack()
    {
        if (PlayerInMeleeRange() && !isTrapped)
        {
            if (canAttack)
            {
                AttackMeleeEmitter.Play();
                
                enemyAnimator.SetTrigger("MeleeAttack");
                //MeleeAttack();
                canAttack = false;
                transform.LookAt(player.GetComponent<Transform>(), Vector3.up);
                StartCoroutine(CooldownAttack());
            }

        }
        else if (PlayerInRangedRange() && !isTrapped)
        {
            if (readyToShoot && canAttack)
            {
                shooting = true;
                enemyAnimator.SetTrigger("RangedAttack");
                currentState = State.SHOOTING;
                shootingPoint = player.transform.position;
                StartCoroutine(RangedShoot());
                //RangedAttack();
            }

        }
        else
        {
            shooting = false;
        }
    }

    public void MeleeAttack()
    {
        player.GetComponent<PlayerHealthScript>().ModifyHealth(damage);
        Debug.Log("MELEE ATTACK");
        StartCoroutine(EnemyCollision());
    }
    IEnumerator RangedShoot()
    {
        agent.isStopped = true;
        PreviewShoot();
        yield return new WaitForSeconds(2f);
        enemyAnimator.SetTrigger("DistanceAttack");
        //Shoot();
        currentState = State.ATTACK;
        agent.isStopped = true;

    }


    public void RangedAttack()
    {
        if (readyToShoot && shooting)
        {
            bulletsShot = 0;
            shootingPoint = player.transform.position;
            agent.isStopped = true;
            AttackRangedEmitter.Play();
            Shoot();

        }
    }

    IEnumerator CooldownAttack()
    {
        enemyAnimator.SetBool("Chase", false);
        enemyAnimator.SetBool("Idle", true);
        
        agent.isStopped = true;
        agent.SetDestination(transform.position);
        yield return new WaitForSeconds(3.0f);
        agent.isStopped = false;
        agent.SetDestination(player.transform.position);
        canAttack = true;
        
        enemyAnimator.SetBool("Idle", false);
        enemyAnimator.SetBool("Chase", true);
        
    }

    void ChangeFromAttack()
    {
        //seesPlayer() &&
        if (!PlayerInMeleeRange() && !PlayerInRangedRange())
        {
            currentState = State.CHASE;
        }
        CheckHit();
    }

    void updateHit()
    {
        lastCheckedHealth = GetComponent<EnemyHealthScript>().GetCurrentHealth();
        if (!isTrapped)
            agent.isStopped = true;
    }

    void ChangeFromHit()
    {
        if (lastCheckedHealth <= 0)
        {
            currentState = State.DIE;
        }
        else
        {
            currentState = State.CHASE;
        }
    }

    void updateDie()
    {
        //objectIsDead.Invoke(gameObject);
    }



    public void Shoot()
    {
        Debug.Log("RANGE SHOT");

        readyToShoot = false;


        GameObject currentBullet = bulletPool.Dequeue();
        currentBullet.SetActive(true);
        currentBullet.transform.position = bulletOrigin.position;
        currentBullet.transform.forward = directionWithoutSpread.normalized;
        currentBullet.GetComponent<EnemyGolemBulletScript>().SetDamage(bulletDamage);
        currentBullet.GetComponent<EnemyGolemBulletScript>().previewBullet = this.previewBullet;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(transform.up * upwardForce, ForceMode.Impulse);

        bulletPool.Enqueue(currentBullet);

        bulletsShot++;

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }



    private void PreviewShoot()
    {
        Debug.Log("PREVIEW");
        directionWithoutSpread = shootingPoint - bulletOrigin.position;

        xSpread = Random.Range(-spread, +spread);
        ySpread = Random.Range(-spread, +spread);
        zSpread = Random.Range(-spread, +spread);


        directionWithoutSpread += new Vector3(xSpread, ySpread, zSpread);
        float normalizedPos = Mathf.InverseLerp(0, maxShootForce, Vector3.Distance(player.transform.position, transform.position) / 2.2f);
        shootForce = Mathf.Lerp(0, maxShootForce, normalizedPos);
        
        normalizedPos = Mathf.InverseLerp(0, maxUpwardForce, Vector3.Distance(player.transform.position, transform.position) / 2.2f);
        upwardForce = Mathf.Lerp(0, maxUpwardForce, normalizedPos);

        previewBullet.SetActive(true);
        previewBullet.transform.position = bulletOrigin.position;
        previewBullet.transform.forward = directionWithoutSpread.normalized;

        previewBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);
        previewBullet.GetComponent<Rigidbody>().AddForce(transform.up * upwardForce, ForceMode.Impulse);
        //enemyAnimator.SetTrigger("DistanceAttack");
    }


    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void CheckHit()
    {
        if (lastCheckedHealth > GetComponent<EnemyHealthScript>().GetCurrentHealth())
        {
            if (GetComponent<EnemyHealthScript>().GetCurrentHealth() <= 0)
            {
                currentState = State.DIE;
            }
            else currentState = State.HIT;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, RANGED_RANGE);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MELEE_RANGE);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, CHASE_RANGE);
    }



    private void OnEnable()
    {
        StartCoroutine(SetEnemySpawn());
    }

    IEnumerator SetEnemySpawn()
    {
        yield return new WaitForSeconds(secondsToSetEnemy);
        enemySetted = true;
    }
    
}
