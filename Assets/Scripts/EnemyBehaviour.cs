using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;

    [SerializeField] LayerMask groundLayer, playerLayer;

    private Vector3 destination;
    private bool walkPointSet;
    [SerializeField] float walkingRange;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (!walkPointSet)
        {
            SearchForDest();
        }
        else
        {
            agent.SetDestination(destination);
        }
        if(Vector3.Distance(transform.position, destination) < 2)
        {
            walkPointSet = false;
        }
    }

    private void SearchForDest()
    {
        float x = Random.Range(-walkingRange, walkingRange);
        float z = Random.Range(-walkingRange, walkingRange);

        destination = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if(Physics.Raycast(destination, Vector3.down, groundLayer))
        {
            walkPointSet = true;
        }
    }
}
