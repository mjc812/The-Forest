using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class Mutant : MonoBehaviour
{
    private enum State
    {
        WALK,
        CHASE,
        ATTACK,
        DEAD
    }

    public GameObject head;
    public GameObject leftUpperArm;
    public GameObject rightUpperArm;
    public GameObject leftLowerArm;
    public GameObject rightLowerArm;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject leftThigh;
    public GameObject rightThigh;

    public Limb leftHandCollider;
    public Limb rightHandCollider;

    private Animator animator;
    private CannibalHealth cannibalHealth;
    private NavMeshAgent navMeshAgent;
    private Transform player;

    private string[] hitAnimations = new string[] { "Hit Left", "Hit Right", "Hit Center" };
    private string[] attackAnimations = new string[] { "Attack 1", "Attack 2", "Attack 5", "Attack 8" }; 
    private string[] deathAnimations = new string[] { "Dead 1", "Dead 2", "Dead 3", "Dead 4" };

    private float walkSpeed = 0.9f;
    private float maxWalkTime = 20.0f;
    private float totalWalkTime = 0.0f;
    private float destinationRadiusMin = 100.0f, destinationRadiusMax = 200.0f;

    private float chaseDistance = 15.0f;
    private float chaseSpeed = 3.5f;

    private float attackDistance = 1.5f;
    private float stopNavMeshAgentDistance = 1.8f;
    private float rotationSpeed = 5f;

    private float attackTime = 2.5f;
    private float attackTimeTotal = 2.0f;

    private State movingState;

    private bool attacking;
    private bool shouting;
    private bool gettingHit;
    private bool dying;
   
    void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        cannibalHealth = GetComponent<CannibalHealth>();
        player = GameObject.FindWithTag("Player").transform;
        leftHandCollider = leftHand.GetComponent<Limb>();
        rightHandCollider = rightHand.GetComponent<Limb>();
        movingState = State.WALK;
    }

    void Start() {
        attacking = false;
        shouting = false;
        gettingHit = false;
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        NavMeshHit nextDestination = GetNavMeshDestination();
        navMeshAgent.SetDestination(nextDestination.position);
    }

    void Update()
    {
        CheckIfDead();
        if (movingState != State.DEAD && !shouting && !gettingHit && !dying) {
            switch (movingState)
            {
                case State.WALK:
                {
                    Walk();
                    break;
                }
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
        } else if (shouting) {
            RotateTowardsPlayer();   
        }
    }

    private void Walk()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = walkSpeed;
        totalWalkTime += Time.deltaTime;
        animator.SetBool("Walk", true);

        if (CheckChaseDistance())
        {
            animator.SetBool("Walk", false);
            TriggerShout();
            movingState = State.CHASE;
        } else
        {
            if (totalWalkTime > maxWalkTime)
            {
                totalWalkTime = 0.0f;
                NavMeshHit nextDestination = GetNavMeshDestination();
                navMeshAgent.SetDestination(nextDestination.position);
            }
        }
    }

    private void Chase()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.position);
        navMeshAgent.speed = chaseSpeed;
        RotateTowardsPlayer();
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
                TriggerRandomAnimation(attackAnimations);
            }
        }
    }

    public void EnableRightHand(string s)
    {
        rightHandCollider.EnableTrigger();
    }

    public void DisableRightHand(string s)
    {
        rightHandCollider.DisableTrigger();
    }

    public void EnableLeftHand(string s)
    {
        leftHandCollider.EnableTrigger();
    }

    public void DisableLeftHand(string s)
    {
        leftHandCollider.DisableTrigger();
    }

    public void AttackFinished(string s)
    {
        attacking = false;
    }

    public void ShoutFinished(string s)
    {
        shouting = false;
    }

    public void HitFinished(string s)
    {
        attackTimeTotal = attackTime;
        if (movingState == State.ATTACK) {
            attacking = true;
        }
        gettingHit = false;
    }

    public void DeathFinished(string s)
    {
        StartCoroutine(DeactivationWait());
    }

    private void TriggerShout() {
        shouting = true;
        animator.SetTrigger("Shout 2");
        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = Vector3.zero;
    }

    private void CheckIfDead() {
        if (cannibalHealth.isDead() && !dying) {
            dying = true;
            movingState = State.DEAD;
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;
            TriggerRandomAnimation(deathAnimations);
            head.SetActive(false);
            leftUpperArm.SetActive(false);
            rightUpperArm.SetActive(false);
            leftLowerArm.SetActive(false);
            rightLowerArm.SetActive(false);
            leftThigh.SetActive(false);
            rightThigh.SetActive(false);
        }
    }

    public void Hit(float amount, bool isCentral, bool isLeft, bool isRight)
    {
        if (!attacking && !gettingHit && !shouting)
        {
            if (movingState == State.WALK) {
                animator.SetBool("Walk", false);
                TriggerShout();
                movingState = State.CHASE;
            } else {
                navMeshAgent.isStopped = true;
                navMeshAgent.velocity = Vector3.zero;
                gettingHit = true;
                attacking = false;
                TriggerRandomAnimation(hitAnimations);
            }
        }
    }

    private void TriggerRandomAnimation(string[] animations)
    {
        int i = Random.Range(0, animations.Length);
        string animation = animations[i];
        animator.SetTrigger(animation);
    }

    private bool CheckAttackDistance()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        return dist <= attackDistance;
    }

    private bool CheckChaseDistance()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        return dist <= chaseDistance;
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

    IEnumerator DeactivationWait()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }

}
