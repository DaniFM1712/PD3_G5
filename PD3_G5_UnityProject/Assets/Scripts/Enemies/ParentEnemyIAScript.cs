using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParentEnemyIAScript : MonoBehaviour
{
    public bool isTrapped = false;
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
    }

    IEnumerator StunEffect(float timer)
    {
        Debug.Log("START");
        StopAgent();
        yield return new WaitForSeconds(timer);
        RestartAgent();
        isTrapped = false;
        Debug.Log("STOP");


    }

    public void StopAgent()
    {
        agent.SetDestination(transform.position);
        agent.isStopped = true;
    }

    public void RestartAgent()
    {
        agent.SetDestination(player.transform.position);
        agent.isStopped = false;
    }

}
