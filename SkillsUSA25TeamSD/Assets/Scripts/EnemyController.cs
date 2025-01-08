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
    public int currentWalkpointNumber;
    public Vector3 walkPoint;
    public bool walkPointSet;
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
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); // these will check a spherical raycast for if the player is in range.
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) { Droning(); }
        if (playerInSightRange && !playerInAttackRange) { Chasing(); }
        if (playerInSightRange && playerInAttackRange) { StartCoroutine(Attacking()); }
    }

    public void Droning()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            navAgent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkpoint = transform.position - walkPoint;

        if (distanceToWalkpoint.magnitude < 1)
        {
            walkPointSet = false;
        }

    }

    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, 0);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) { walkPointSet = true; }
        Debug.Log("found waypoint");
    }

    public void Chasing()
    {
        navAgent.SetDestination(player.transform.position);
    }

    public IEnumerator Attacking()
    {
        navAgent.SetDestination(player.transform.position);
        transform.LookAt(player.transform);

        if (!alreadyAttacked)
        {

            player.UpdateHealth(-damage);
            alreadyAttacked = true;
            yield return new WaitForSeconds(attackSpeed);
            ResetAttack();
        }



    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
