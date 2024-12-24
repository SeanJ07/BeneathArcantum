using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 5f;

    public NavMeshAgent navAgent;
    public LayerMask whatIsGround, whatIsPlayer;

    private PlayerController player;
    private Rigidbody rigidBody;

    [Header("Droning")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    [Header("Attack")]
    public float attackSpeed;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>(); // finds the player and references it
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) { Droning();  }
        if (playerInSightRange && !playerInAttackRange) { Chasing(); }
        if (playerInSightRange && playerInAttackRange) { Attacking(); }
    }

    public void Droning()
    {
        if (!walkPointSet) { SearchWalkPoint();  }
        if (walkPointSet)
        {

        }

        Vector3 distanceToWalkpoint = transform.position - walkPoint;
        if (distanceToWalkpoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, 0, 0);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) { walkPointSet = true; }
    }

    public void Chasing()
    {
        transform.LookAt(player.transform);
        navAgent.SetDestination(player.transform.position);
    }

    public void Attacking()
    {
        navAgent.SetDestination(transform.position);
        transform.LookAt(player.transform);

        if (!alreadyAttacked)
        {

            // ATTACK CODE

            alreadyAttacked = true;
            Invoke("ResetAttack", attackSpeed);

        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
