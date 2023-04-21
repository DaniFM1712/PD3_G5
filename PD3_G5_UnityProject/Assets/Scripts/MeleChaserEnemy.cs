using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MeleChaserEnemy : MonoBehaviour
{

    NavMeshAgent enemy;
    [SerializeField] LayerMask obstacleMask;
    Vector3 distanceToPlayer;
    [SerializeField] UnityEvent<GameObject> objectIsDead;



    enum State { IDLE, CHASE, ATTACK, HIT , DIE }
    [SerializeField] State currentState;
    [SerializeField] GameObject player;

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
        enemy = GetComponent<NavMeshAgent>();
        lastCheckedHealth = GetComponent<HealthPlayerSystem>().getCurrentHealth();
        currentState = State.IDLE;
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
        if (seesPlayer() && !PlayerInRange())
        {
            currentState = State.CHASE;
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
        if (enemy.isStopped) enemy.isStopped = !enemy.isStopped;
        enemy.SetDestination(player.transform.position);
    }

    void ChangeFromChase()
    {
        if (PlayerInRange())
        {
            currentState = State.ATTACK;
        }
        isHit();
    }


    void updateAttack()
    {
         
    }

    void ChangeFromAttack()
    {
        if (seesPlayer() && !PlayerInRange())
        {
            currentState = State.CHASE;
        }
        isHit();
    }

    void updateHit()
    {
        lastCheckedHealth = GetComponent<HealthEnemySystem>().getCurrentHealth();
        enemy.isStopped = true;
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
        objectIsDead.Invoke(gameObject);
    }

    

    private void isHit()
    {
        if (lastCheckedHealth > GetComponent<HealthEnemySystem>().getCurrentHealth())
        {
            if (GetComponent<HealthEnemySystem>().getCurrentHealth() <= 0)
            {
                currentState = State.DIE;
            }
            else currentState = State.HIT;
        }
    }



}
