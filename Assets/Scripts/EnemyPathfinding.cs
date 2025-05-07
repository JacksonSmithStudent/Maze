using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyPathfinding : MonoBehaviour
{
    public float patrolRange = 100f;
    public float chaseRange = 20f;
    public float detectionRadius = 5f;

    private NavMeshAgent agent;
    private Vector3 patrolPoint;
    private Transform player;
    private bool isChasingPlayer = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetNewPatrolPoint();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (isChasingPlayer)
        {
            agent.SetDestination(player.position);

            if (distanceToPlayer > chaseRange)
            {
                isChasingPlayer = false;
                SetNewPatrolPoint();
            }
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 1f)
            {
                SetNewPatrolPoint();
            }

            if (distanceToPlayer <= chaseRange)
            {
                isChasingPlayer = true;
            }
        }
    }

    void SetNewPatrolPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRange;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRange, NavMesh.AllAreas))
        {
            patrolPoint = hit.position;
            agent.SetDestination(patrolPoint);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("Lose");
        }
    }
}
