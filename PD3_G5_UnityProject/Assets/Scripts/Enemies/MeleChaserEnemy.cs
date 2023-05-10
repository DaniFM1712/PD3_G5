using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MeleChaserEnemy : MonoBehaviour
{

    NavMeshAgent agent;
    
    [SerializeField] LayerMask obstacleMask;
    Vector3 distanceToPlayer;
    [SerializeField] UnityEvent<GameObject> objectIsDead;
    private bool blocked = false;



    enum State { IDLE, CHASE, ATTACK, HIT , DIE }
    [SerializeField] State currentState;
    private GameObject player;

    [Header("IDLE")]
    State lastState;

 
    [Header("CHASE")]
    [SerializeField] float CHASE_MAX;



    [Header("ATTACK")]
    [SerializeField] float damage;
    float lastTimeCollisioned;


    [Header("HIT")]
    float lastCheckedHealth;

    [Header("DIE")]
    [SerializeField] float fadeSpeed;
    [SerializeField] MeshRenderer enemyRenderer;

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


    void updateIdle(){}

    void ChangeFromIdle()
    {
        //seesPlayer() &&
        if ( !PlayerInRange())
        {
            currentState = State.CHASE;
        }
        else
        {
            currentState = State.ATTACK;
        }
        isHit();
    }
   

    bool PlayerInRange()
    {
        return (distanceToPlayer).magnitude < CHASE_MAX;
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
        
        if(!blocked)
            agent.SetDestination(player.transform.position);
    }

    void ChangeFromChase()
    {
        if (PlayerInRange())
        {
            currentState = State.ATTACK;
            attack();
            if(!blocked)
                StartCoroutine(CooldownAttack());
        }
        isHit();
    }

    private void attack()
    {
        player.GetComponent<PlayerHealthScript>().ModifyHealth(damage);
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
        if (!agent.isStopped && !PlayerInRange())
        {
            currentState = State.CHASE;
        }
        isHit();
    }

    void updateHit()
    {
        lastCheckedHealth = GetComponent<EnemyHealthScript>().GetCurrentHealth();
        if(!blocked)
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
        Debug.Log("AGENT STOPED");
        blocked = true;
        agent.SetDestination(transform.position);

    }

    public void RestartAgent()
    {
        Debug.Log("AGENT RESTART");
        blocked = false;
        agent.SetDestination(player.transform.position);

    }

    public void GetStunned(float stunTimer)
    {
        StartCoroutine(StunEffect(stunTimer));
    }

    IEnumerator StunEffect(float timer)
    {

        Debug.Log("2. MELEE SCRIPT");
        StopAgent();

        yield return new WaitForSeconds(timer);

        RestartAgent();


    }

}
