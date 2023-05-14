using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.ProBuilder;

public class GolemEnemyAIScript : MonoBehaviour
{

    NavMeshAgent agent;

    [SerializeField] LayerMask obstacleMask;
    [SerializeField] GameObject bulletPrefab;

    Vector3 distanceToPlayer;
    [SerializeField] UnityEvent<GameObject> objectIsDead;
    private bool blocked = false;

    //SHOOTING 
    [Header("Forces")]
    [SerializeField] float shootForce;
    [SerializeField] float upwardForce;
    [SerializeField] Transform bulletOrigin;

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
    [SerializeField] float secondsToSetEnemy;
    private GameObject player;

    [Header("IDLE")]
    State lastState;


    [Header("CHASE")]
    [SerializeField] float RANGED_RANGE;
    [SerializeField] float MELEE_RANGE;



    [Header("ATTACK")]
    [SerializeField] float damage;
    float lastTimeCollisioned;


    [Header("HIT")]
    float lastCheckedHealth;

    [Header("DIE")]
    [SerializeField] float fadeSpeed;
    [SerializeField] MeshRenderer enemyRenderer;


    private bool enemySetted = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        lastCheckedHealth = GetComponent<EnemyHealthScript>().GetCurrentHealth();
        currentState = State.IDLE;
    }
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (enemySetted)
        {
            distanceToPlayer = player.transform.position - transform.position;
            switch (currentState)
            {
                case State.ATTACK:
                    lastState = State.ATTACK;
                    updateAttack();
                    ChangeFromAttack();
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
        if (!PlayerInMeleeRange())
        {
            currentState = State.CHASE;
        }
        else
        {
            currentState = State.ATTACK;
        }
        isHit();
    }


    bool PlayerInMeleeRange()
    {
        return (distanceToPlayer).magnitude < MELEE_RANGE;
    }
    bool PlayerInRangedRange()
    {
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
        if (agent.isStopped)
            agent.isStopped = !agent.isStopped;

        if (!blocked)
            agent.SetDestination(player.transform.position);
    }

    void ChangeFromChase()
    {
        if (PlayerInMeleeRange() || PlayerInRangedRange())
        {
            currentState = State.ATTACK;
            attack();
            if (!blocked)
                StartCoroutine(CooldownAttack());
        }
        isHit();
    }

    private void attack()
    {
        if (PlayerInMeleeRange())
        {
            player.GetComponent<PlayerHealthScript>().ModifyHealth(damage);
        }
        else if(PlayerInRangedRange())
        {
            if (readyToShoot && shooting && !reloading)
            {
                bulletsShot = 0;
                if (bulletsLeft > 0)
                {
                    shootingPoint = player.transform.position;
                    agent.isStopped = true;
                    Shoot();
                }
                else
                    Reload();
            }
        }
    }

    void updateAttack()
    {

    }

    IEnumerator CooldownAttack()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(3.0f);
        agent.isStopped = false;
    }

    void ChangeFromAttack()
    {
        //seesPlayer() &&
        if (!agent.isStopped && !PlayerInMeleeRange())
        {
            currentState = State.CHASE;
        }
        isHit();
    }

    void updateHit()
    {
        lastCheckedHealth = GetComponent<EnemyHealthScript>().GetCurrentHealth();
        if (!blocked)
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
            currentState = lastState;
        }
    }

    void updateDie()
    {
        //objectIsDead.Invoke(gameObject);
    }



    private void Shoot()
    {

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



    private void isHit()
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

    public void StopAgent()
    {
        blocked = true;
        //agent.SetDestination(transform.position);
        agent.isStopped = true;

    }

    public void RestartAgent()
    {
        blocked = false;
        //agent.SetDestination(player.transform.position);
        agent.isStopped = false;


    }

    public void GetStunned(float stunTimer)
    {
        StartCoroutine(StunEffect(stunTimer));
    }

    IEnumerator StunEffect(float timer)
    {

        StopAgent();

        yield return new WaitForSeconds(timer);

        RestartAgent();


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, RANGED_RANGE);
        Gizmos.DrawWireSphere(transform.position, MELEE_RANGE);
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
