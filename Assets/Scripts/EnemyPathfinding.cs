using UnityEngine;
using UnityEngine.AI;

public class EnemyPathfinding : MonoBehaviour
{
    public float patrolRange = 100f;    // Max range for random patrol points
    public float chaseRange = 20f;      // Range at which the enemy starts chasing the player
    public float detectionRadius = 5f;  // Radius to detect player in chase range

    private NavMeshAgent agent;
    private Vector3 patrolPoint;
    private Transform player;
    private bool isChasingPlayer = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false; // Smooth movement without abrupt stops
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  // Find the player
        SetNewPatrolPoint();  // Start patrolling immediately
    }

    void Update()
    {
        // Check the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (isChasingPlayer)
        {
            // If we're chasing the player, stop patrolling and move towards the player
            agent.SetDestination(player.position);

            // If the player is out of chase range, go back to patrolling
            if (distanceToPlayer > chaseRange)
            {
                isChasingPlayer = false;
                SetNewPatrolPoint();  // Resume patrolling
            }
        }
        else
        {
            // If not chasing, patrol
            if (!agent.pathPending && agent.remainingDistance < 1f)
            {
                SetNewPatrolPoint();  // Continue patrolling to new points
            }

            // Check if the player enters chase range
            if (distanceToPlayer <= chaseRange)
            {
                isChasingPlayer = true;  // Start chasing the player
            }
        }
    }

    void SetNewPatrolPoint()
    {
        // Generate a random point on the NavMesh across the entire map
        Vector3 randomDirection = Random.insideUnitSphere * patrolRange;
        randomDirection += transform.position;  // Offset to the enemy's current position

        // Make sure the random point is on the NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRange, NavMesh.AllAreas))
        {
            patrolPoint = hit.position;  // Set the new patrol point
            agent.SetDestination(patrolPoint);  // Move the agent to the new patrol point
        }
    }
}
