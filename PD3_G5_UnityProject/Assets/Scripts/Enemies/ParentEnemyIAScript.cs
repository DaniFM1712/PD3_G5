using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParentEnemyIAScript : MonoBehaviour
{
    bool isTrapped = false;
    protected bool blocked = false;
    protected NavMeshAgent agent;
    protected GameObject player;

    public virtual void Start()
    {
        player = GameObject.Find("Player");
    }

    public void GetStunned(float stunTimer)
    {
        isTrapped = true;
        StartCoroutine(StunEffect(stunTimer));
        isTrapped = false;
    }

    IEnumerator StunEffect(float timer)
    {
        StopAgent();
        yield return new WaitForSeconds(timer);
        RestartAgent();
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

}
