using UnityEngine;
using UnityEngine.AI;

public class Mutant : MonoBehaviour
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

    private float attackTime = 2.5f;
    private float attackTimeTotal = 1f;

    private bool attacking;
    private bool gettingHit;
   
    void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;

        movingState = State.CHASE;
        navMeshAgent.updateRotation = false;
    }

    void Start() {
        attacking = false;
        gettingHit = false;
    }

    void Update()
    {
        CheckIfHit();
        if (!gettingHit) {
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
    }

    private void Chase()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.position);
        navMeshAgent.speed = chaseSpeed;
        rotateTowardsDirection();
        animator.SetBool("Run", true);

        if (CheckAttackDistance())
        {
            animator.SetBool("Run", false);
            movingState = State.ATTACK;
        }
    }

    private void Attack()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = Vector3.zero;

        if (!CheckAttackDistance() && !attacking)
        {
            attackTimeTotal = 2f;
            movingState = State.CHASE;
        } else {
            RotateTowardsPlayer();
            attackTimeTotal += Time.deltaTime;
            if (attackTimeTotal >= attackTime) {
                attackTimeTotal = 0f;
                attacking = true;
                int randomAnimationNumber = UnityEngine.Random.Range(0, 5);
                if (randomAnimationNumber == 0) {
                    animator.SetTrigger("Attack 1");
                } else if (randomAnimationNumber == 1) {
                    animator.SetTrigger("Attack 2");
                } else if (randomAnimationNumber == 2) {
                    animator.SetTrigger("Attack 5");
                } else if (randomAnimationNumber == 3) {
                    animator.SetTrigger("Attack 8");
                } else {
                    animator.SetTrigger("Attack 10");
                }
            }
        }
    }

    public void AttackFinished(string s)
    {
        attacking = false;
    }

    public void HitFinished(string s)
    {
        Debug.Log("hit finished");
        attackTimeTotal = attackTime;
        gettingHit = false;
    }

    private bool CheckIfHit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;
            gettingHit = true;

            int randomAnimationNumber = UnityEngine.Random.Range(0, 4);
            if (randomAnimationNumber == 0) {
                animator.SetTrigger("Hit Left");
            } else if (randomAnimationNumber == 1) {
                animator.SetTrigger("Hit Right");
            } else {
                animator.SetTrigger("Hit Center");
            }
            return true;
        } else {
            return false;
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
