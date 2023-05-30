using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowAoEScript : MonoBehaviour
{
    [SerializeField] float lifeTime = 5f;
    [SerializeField] float slowMultiplyer = 2f;
    float timeToDestroy;
    private List<NavMeshAgent> enemyList;
    // Start is called before the first frame update
    void Start()
    {
        timeToDestroy = lifeTime;
        enemyList = new List<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        timeToDestroy -= Time.deltaTime;

        if (timeToDestroy <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if(other.gameObject.TryGetComponent(out NavMeshAgent enemyAgent))
            {
                Debug.Log(enemyAgent.speed);
                enemyAgent.speed /= slowMultiplyer;
                Debug.Log(enemyAgent.speed);

                enemyList.Add(enemyAgent);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.TryGetComponent(out NavMeshAgent enemyAgent))
            {
                if (enemyList.Contains(enemyAgent))
                {
                    enemyAgent.speed *= slowMultiplyer;
                    enemyList.Remove(enemyAgent);
                }
            }
        }       
    }


    private void OnDestroy()
    {
        foreach (NavMeshAgent agent in enemyList)
        {
            if(agent!=null)
                agent.speed *= slowMultiplyer;
        } 
        enemyList.Clear();
    }
}
