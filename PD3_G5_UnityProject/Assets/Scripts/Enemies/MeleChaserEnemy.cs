using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MeleChaserEnemy : ParentEnemyIAScript
{

    
    
    [SerializeField] LayerMask obstacleMask;
    Vector3 distanceToPlayer;
    [SerializeField] UnityEvent<GameObject> objectIsDead;

    [Header("DASH")]
    [SerializeField] GameObject detectionArea;
    [SerializeField] Transform leftPos;
    [SerializeField] Transform rightPos;
    private float dashCooldown = 10f;
    private float dashTimer;
    private bool dashInCooldown = false;
    private bool isDashing = false;


    enum State { IDLE, CHASE, ATTACK, HIT , DIE }
    [SerializeField] State currentState;
    [SerializeField] float secondsToSetEnemy;

    [Header("IDLE")]
    State lastState;

 
    [Header("CHASE")]
    [SerializeField] float MELEE_RANGE;
    [SerializeField] float CHASE_RANGE;


    [Header("ATTACK")]
    [SerializeField] float damage;
    float lastTimeCollisioned;
    bool attackInCooldown = false;


    [Header("HIT")]
    float lastCheckedHealth;

    [Header("DIE")]
    [SerializeField] float fadeSpeed;
    [SerializeField] MeshRenderer enemyRenderer;


    private bool enemySetted = false;

    [Header("FMOD")]
    public StudioEventEmitter ChaseEmitter;
    public StudioEventEmitter AttackEmitter;


    [Header("Animator")] 
    [SerializeField] private Animator enemyAnimator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        lastCheckedHealth = GetComponent<EnemyHealthScript>().GetCurrentHealth();
        currentState = State.IDLE;
        dashTimer = dashCooldown;
    }
    override public void Start()
    {
        base.Start();
    }

    void Update()
    {
        //if (agent.hasPath)
        //agent.acceleration = (agent.remainingDistance < 4f) ? 60f : 2f;

        if (dashInCooldown)
        {
            dashTimer -= Time.deltaTime;

            if(dashTimer <= 0)
            {
                detectionArea.GetComponent<Collider>().enabled = true;
                dashInCooldown = false;
                dashTimer = dashCooldown;
            }
        }
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
                    //Vector3 targetDelta = player.transform.position - transform.position;
                    //float angleToTarget = Vector3.Angle(transform.forward, targetDelta);
                    //Vector3 turnAxis = Vector3.Cross(transform.forward, targetDelta);

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


    void updateIdle()
    {
        enemyAnimator.SetBool("Idle", true);
        enemyAnimator.SetBool("Chase", false); 
    }

    void ChangeFromIdle()
    {
        //seesPlayer() &&
        if (PlayerInChaseRange() &&!PlayerInMeleeRange())
        {
            ChaseEmitter.Play();
            currentState = State.CHASE;
            enemyAnimator.SetBool("Idle", false);
            enemyAnimator.SetBool("Chase", true); 
        }
        //seesPlayer() &&
        else if(PlayerInMeleeRange())
        {
            currentState = State.ATTACK;
            enemyAnimator.SetTrigger("Attack");
        }
        checkHit();
    }
   

    bool PlayerInMeleeRange()
    {
        return (distanceToPlayer).magnitude < MELEE_RANGE;
    }

    bool PlayerInChaseRange()
    {
        return (distanceToPlayer).magnitude < CHASE_RANGE;
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
        if (agent.enabled && !isDashing)
        {
            if (agent.isStopped)
            {
                agent.isStopped = !agent.isStopped;
                agent.SetDestination(agent.transform.position);
            }
            if (!isTrapped)
                agent.SetDestination(player.transform.position);
        }

        
    }

    void ChangeFromChase()
    {
        if (PlayerInMeleeRange())
        {
            currentState = State.ATTACK;
        }
        checkHit();
    }

    public void attack()
    {
        AttackEmitter.Play();
        player.GetComponent<PlayerHealthScript>().ModifyHealth(damage);
    }

    void updateAttack()
    {
        if (!attackInCooldown && !isTrapped && PlayerInMeleeRange())
        {
            attackInCooldown = true;
            enemyAnimator.SetTrigger("Attack");
            //attack();
            StartCoroutine(CooldownAttack());
        }
        checkHit();
    }

    IEnumerator CooldownAttack()
    {
        enemyAnimator.SetBool("Chase", false);
        enemyAnimator.SetBool("Idle", true);
        agent.isStopped = true;
        yield return new WaitForSeconds(2.0f);
        attackInCooldown = false;
        agent.isStopped = false;
        enemyAnimator.SetBool("Idle", false);
        enemyAnimator.SetBool("Chase", true);
        
    }

    void ChangeFromAttack()
    {
        //seesPlayer() &&
        if (!agent.isStopped && !PlayerInMeleeRange())
        {
            ChaseEmitter.Play();
            currentState = State.CHASE;
        }
        checkHit();
    }

    void updateHit()
    {
        lastCheckedHealth = GetComponent<EnemyHealthScript>().GetCurrentHealth();
        if(!isTrapped)
            agent.isStopped = true;
    }

    void ChangeFromHit()
    {
        if (lastCheckedHealth <= 0)
        {
            currentState = State.DIE;
            agent.isStopped = true;
            agent.SetDestination(agent.transform.position);
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

    

    private void checkHit()
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
        Gizmos.DrawWireSphere(transform.position, MELEE_RANGE);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, CHASE_RANGE);
    }



    private void OnEnable()
    {
        StartCoroutine(SetEnemySpawn());    
    }

    IEnumerator SetEnemySpawn() {
        yield return new WaitForSeconds(secondsToSetEnemy);
        enemySetted = true;
    }

    public void BulletDetected()
    {
        detectionArea.GetComponent<Collider>().enabled = false;
        int random = Random.Range(0,100);
        if(random <= 70)
            StartCoroutine(ExecuteDash());
    }

    IEnumerator ExecuteDash()
    {
        isDashing = true;
        Vector3 destination;
        int random = Random.Range(0, 2);

        destination = random > 0 ? rightPos.position : leftPos.position;

        float prevSpeed = agent.speed;
        float prevAngularSpeed = agent.angularSpeed;
        float prevAcceleration = agent.acceleration;

        agent.SetDestination(destination);
        agent.speed = prevSpeed * 10f;
        agent.angularSpeed = prevAngularSpeed * 10f;
        agent.acceleration = prevAcceleration * 10f;

        float dist = agent.remainingDistance; 
        
        while(agent.remainingDistance >= 0.1)
        {
            yield return new WaitForEndOfFrame();
        }

        agent.isStopped = true;
        yield return new WaitForSeconds(0.5f);

        Debug.Log("arribo?");
        agent.isStopped = false;
        agent.SetDestination(player.transform.position);
        agent.speed = prevSpeed;
        agent.angularSpeed = prevAngularSpeed;
        agent.acceleration = prevAcceleration;

        isDashing = false;
        dashInCooldown = true;



    }

}
