using UnityEngine;
using UnityEngine.AI;

public enum MovingState
{
    IDLE,
    WALK,
    WALKTOATTACK,
    CHASE,
    ATTACK
}

public class Cannibal : MonoBehaviour
{
    //comments here
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Transform player;

    private CannibalHealth cannibalHealth;

    private float walkSpeed = 1.0f;
    private float walkToAttackSpeed = 2.0f;
    private float maxWalkTime = 10.0f;
    private float totalWalkTime = 10.0f;
    private float destinationRadiusMin = 100.0f, destinationRadiusMax = 200.0f;

    private float chaseSpeed = 5.0f;
    private float playerChaseDistance = 15.0f;
    private float playerChaseFromAttactDistance = 4.0f;

    private float playerAttackDistance = 1.2f;
    private float timeBeforeAttack = 3.0f;
    private float totalBeforeAttackTime = 3.0f;

    public float rotationSpeed = 10f;

    private MovingState movingState = MovingState.WALK;
   
    void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        cannibalHealth = GetComponent<CannibalHealth>();
    }

    void Update()
    {
        //Debug.Log(movingState);
        //in branch
        switch (movingState)
        {
            case MovingState.WALK:
                {
                    Walk();
                    break;
                }
            case MovingState.WALKTOATTACK:
                {
                    WalkToAttack();
                    break;
                }
            case MovingState.CHASE:
                {
                    Chase();
                    break;
                }
            case MovingState.ATTACK:
                {
                    Attack();
                    break;
                }
        }
        if (cannibalHealth.isDead())
        {
            gameObject.SetActive(false);
        }
    }

    private void Idle()
    {

    }

    private void Walk()
    {
        navMeshAgent.isStopped = false;
        movingState = MovingState.WALK;
        totalWalkTime += Time.deltaTime;
        navMeshAgent.speed = walkSpeed;

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            animator.Play("Walk");
        }
        //animator.Play("Walk");

        if (CheckChaseDistance())
        {
            movingState = MovingState.CHASE;
        }
        else
        {
            if (totalWalkTime > maxWalkTime)
            {
                totalWalkTime = 0.0f;
                NavMeshHit nextDestination = GetNavMeshDestination();
                navMeshAgent.SetDestination(nextDestination.position);
            }
        }
    }

    private void WalkToAttack()
    {
        navMeshAgent.isStopped = false;
        movingState = MovingState.WALKTOATTACK;
        navMeshAgent.SetDestination(player.position);
        navMeshAgent.speed = walkToAttackSpeed;
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            animator.Play("Walk");
        }
        //animator.Play("Walk");
        RotateTowardsPlayer();
        if (!CheckChaseFromAttackDistance())
        {
            movingState = MovingState.CHASE;
        }
        else if (CheckAttackDistance())
        {
            totalBeforeAttackTime = timeBeforeAttack + 1;
            movingState = MovingState.ATTACK;
        }
    }

    private void Chase()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.position);
        navMeshAgent.speed = chaseSpeed;


        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Chase"))
        {
            animator.Play("Chase");
        }
        RotateTowardsPlayer();

        if (!CheckChaseDistance())
        {
            movingState = MovingState.WALK;
            totalWalkTime = maxWalkTime;
        }
        else if (CheckAttackDistance())
        {
            totalBeforeAttackTime = timeBeforeAttack + 1;
            movingState = MovingState.ATTACK;
        }
    }

    private void Attack()
    {
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped = true;
        totalBeforeAttackTime += Time.deltaTime;
        RotateTowardsPlayer();

        if (totalBeforeAttackTime >= timeBeforeAttack)
        {
            totalBeforeAttackTime = 0.0f;
            animator.SetTrigger("Punch Left");
        }
        else if (!CheckAttackDistance())
        {
            totalBeforeAttackTime = timeBeforeAttack;
            movingState = MovingState.WALKTOATTACK;
        }
    }

    private bool CheckAttackDistance()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        return dist <= playerAttackDistance;
    }

    private bool CheckChaseDistance()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        return dist <= playerChaseDistance;
    }

    private bool CheckChaseFromAttackDistance()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        return dist <= playerChaseFromAttactDistance;
    }

    //understand
    private void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private NavMeshHit GetNavMeshDestination()
    {
        float destinationRadius = Random.Range(
            destinationRadiusMin,
            destinationRadiusMax
        );

        Vector3 direction = Random.insideUnitSphere * destinationRadius;
        direction += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(direction, out navHit, destinationRadius, NavMesh.AllAreas);

        return navHit;
    }
}
