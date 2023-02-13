using UnityEngine;
using UnityEngine.AI;

public class Crustacean : MonoBehaviour
{
    private enum State
    {
        IDLE,
        WALK,
        CHASE,
        ATTACK
    }

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Transform player;

    private State movingState;

    private float chaseSpeed = 2.0f;
    private float attackDistance = 4f;
    private float stopNavMeshAgentDistance = 1.8f;
    public float rotationSpeed = 10f;
   
    void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;

        movingState = State.CHASE;
        navMeshAgent.updateRotation = false;
    }

    void Start() {
        animator.SetTrigger("Walk_Cycle_1");
    }

    void Update()
    {
        switch (movingState)
        {
            case State.CHASE:
                {
                    Chase();
                    break;
                }
            case State.ATTACK:
                {
                    Attack();
                    break;
                }
        }
    }

    private void Chase()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.position);
        navMeshAgent.speed = chaseSpeed;

        rotateTowardsDirection();


        if (CheckAttackDistance())
        {
            movingState = State.ATTACK;
        }
    }

    private float attackTime = 5f;
    private float attackTimeTotal = 0f;

    private void Attack()
    {
        if (CheckNavMeshAgentDistance()) {
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;
        }

        RotateTowardsPlayer();

        attackTimeTotal += Time.deltaTime;
        if (attackTimeTotal >= attackTime) {
            attackTimeTotal = 0f;
            animator.SetTrigger("Attack_1");
        }

        if (!CheckAttackDistance())
        {
            attackTimeTotal = 0f;
            movingState = State.CHASE;
        }
    }

    private bool CheckAttackDistance()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        return dist <= attackDistance;
    }

    private bool CheckNavMeshAgentDistance()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        return dist <= stopNavMeshAgentDistance;
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void rotateTowardsDirection()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        if (navMeshAgent.velocity.normalized != Vector3.zero) {
            Quaternion lookRotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);   
        }
    }
}
