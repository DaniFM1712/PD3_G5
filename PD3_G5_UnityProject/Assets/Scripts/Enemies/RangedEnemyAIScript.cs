using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class RangedEnemyAIScript : ParentEnemyIAScript
{

    [SerializeField] LayerMask obstacleMask;
    [SerializeField] GameObject bulletPrefab;
    Vector3 distanceToPlayer;
    [SerializeField] UnityEvent<GameObject> objectIsDead;

    [Header("Forces")]
    [SerializeField] float shootForce;
    [SerializeField] float upwardForce;
    [SerializeField] Transform bulletOrigin;
    [SerializeField] float turnRate;

    [Header("Stats")]
    [SerializeField] float timeBetweenShooting;
    [SerializeField] float spread;
    [SerializeField] float reloadTime;
    [SerializeField] float timeBetweenShots;
    [SerializeField] float bulletDamage;
    [SerializeField] int magazineSize;
    [SerializeField] int bulletsPerTap;
    [SerializeField] bool allowButtonHold;


    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;
    [SerializeField] bool allowInvoke;
    Queue<GameObject> bulletPool;
    Vector3 shootingPoint;



    enum State { IDLE, CHASE, ATTACK, HIT, DIE }
    [SerializeField] State currentState;
    [SerializeField] public float secondsToSetEnemy;
    private bool enemySetted = false;



    [Header("IDLE")]
    [SerializeField] float maxDetectionCooldown = 2f;
    State lastState;
    private float currentDetectionCooldown;
    private bool detecting = false;


    [Header("CHASE")]
    [SerializeField] float RANGED_RANGE;



    [Header("ATTACK")]
    [SerializeField] float damage;
    float lastTimeCollisioned;


    [Header("HIT")]
    float lastCheckedHealth;

    [Header("DIE")]
    [SerializeField] float fadeSpeed;
    [SerializeField] MeshRenderer enemyRenderer;

    [Header("FMOD")]
    public StudioEventEmitter AttackEmitter;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = State.IDLE;
    }
    override public void Start()
    {
        base.Start();
        lastCheckedHealth = GetComponent<EnemyHealthScript>().GetCurrentHealth();
        currentDetectionCooldown = maxDetectionCooldown;
        bulletPool = new Queue<GameObject>();
        GameObject bullets = new GameObject("RangedBullets");
        for (int i = 0; i < magazineSize + 10; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
            bullet.transform.parent = bullets.transform;
        }

        bulletsLeft = magazineSize;
        readyToShoot = true;
        shooting = false;
    }

    void Update()
    {
        if (enemySetted) {
            distanceToPlayer = player.transform.position - transform.position;
            switch (currentState)
            {
                case State.ATTACK:
                    lastState = State.ATTACK;
                    updateAttack();
                    ChangeFromAttack();
                    transform.LookAt(player.transform, Vector3.up);
                    break;
                case State.IDLE:
                    lastState = State.IDLE;
                    updateIdle();
                    ChangeFromIdle();
                    break;
                case State.CHASE:
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
        if (PlayerInRange())
        {
            detecting = true;
            if(currentDetectionCooldown <= 0)
                currentState = State.ATTACK;
        }
        if(detecting && !PlayerInRange())
        {
            detecting = false;
            currentDetectionCooldown = maxDetectionCooldown;
        }
        CheckHit();


    }


    bool PlayerInRange()
    {
        currentDetectionCooldown -= Time.deltaTime;
        return (distanceToPlayer).magnitude < RANGED_RANGE;
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
        if (agent.isStopped) agent.isStopped = !agent.isStopped;
        agent.SetDestination(player.transform.position);
    }

    void ChangeFromChase()
    {
        if (PlayerInRange())
        {
            agent.SetDestination(transform.position);
            currentState = State.ATTACK;
        }
        CheckHit();
    }

    private void attack()
    {
        if (readyToShoot && shooting && !reloading)
        {
            bulletsShot = 0;
            if (bulletsLeft > 0)
            {
                shootingPoint = player.transform.position;
                agent.isStopped = true;
                AttackEmitter.Play();
                Shoot();
            }
            else
                Reload();
        }
    }

    private void Shoot()
    {
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(player.transform.position), Time.deltaTime * turnRate);
        //transform.LookAt(player.GetComponent<Transform>(), Vector3.up);
        
        readyToShoot = false;
        Vector3 directionWithoutSpread = shootingPoint - bulletOrigin.position;

        float xSpread = Random.Range(-spread, +spread);
        float ySpread = Random.Range(-spread, +spread);
        float zSpread = Random.Range(-spread, +spread);


        directionWithoutSpread += new Vector3(xSpread, ySpread, zSpread);

        GameObject currentBullet = bulletPool.Dequeue();
        currentBullet.SetActive(true);
        currentBullet.transform.position = bulletOrigin.position;
        currentBullet.transform.forward = directionWithoutSpread.normalized;
        currentBullet.GetComponent<EnemyBulletScript>().SetDamage(bulletDamage);

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(transform.up * upwardForce, ForceMode.Impulse);

        bulletPool.Enqueue(currentBullet);

        bulletsLeft--;
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

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    void updateAttack()
    {
        if (PlayerInRange() && readyToShoot && !reloading)
        {
            shooting = true;
            attack();
        }
        else
            shooting = false;
    }

    void ChangeFromAttack()
    {
        //seesPlayer() &&
        if (!PlayerInRange())
        {
            agent.isStopped = false;
            currentState = State.CHASE;
        }
        CheckHit();
    }

    void updateHit()
    {
        lastCheckedHealth = GetComponent<EnemyHealthScript>().GetCurrentHealth();
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RANGED_RANGE);
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
