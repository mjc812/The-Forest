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
    public Limb headCollider;

    public AudioClip leftFootStep;
    public AudioClip rightFootStep;

    private Animator animator;
    private AudioSource footstepAudio;
    private CannibalHealth cannibalHealth;
    private NavMeshAgent navMeshAgent;
    private Transform player;
    private Health playerHealth;
    private AudioSource audioSource;

    private string[] hitAnimations = new string[] { "Hit Left", "Hit Right", "Hit Center" };
    private string[] attackAnimations = new string[] { "Attack 1", "Attack 2", "Attack 5" }; // "Attack 8"
    private string[] deathAnimations = new string[] { "Dead 1", "Dead 2", "Dead 3", "Dead 4" };

    public AudioClip[] shoutAudioClips;
    public AudioClip[] chaseAudioClips;
    public AudioClip[] attackAudioClips;
    public AudioClip[] hitAudioClips;
    public AudioClip[] deadAudioClips;

    private float audioClipTime = 3f;
    private float audioClipTimeTotal = 3f;

    private float walkSpeed = 0.9f;
    private float maxWalkTime = 20.0f;
    private float totalWalkTime = 0.0f;
    private float destinationRadiusMin = 100.0f, destinationRadiusMax = 200.0f;

    private float chaseDistance = 15.0f;
    private float chaseSpeed = 3.5f;

    private float attackDistance = 1.5f;
    private float stopNavMeshAgentDistance = 1.8f;
    private float rotationSpeed = 5f;

    private float attackTime = 2f;
    private float attackTimeTotal = 2f;

    private State movingState;

    private bool attacking;
    private bool shouting;
    private bool gettingHit;
    private bool dying;
   
    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        cannibalHealth = GetComponent<CannibalHealth>();
        player = GameObject.FindWithTag("Player").transform;
        playerHealth = player.gameObject.GetComponent<Health>();
        leftHandCollider = leftHand.GetComponent<Limb>();
        rightHandCollider = rightHand.GetComponent<Limb>();
        headCollider = head.GetComponent<Limb>();
        footstepAudio = transform.Find("FootStep Audio").GetComponent<AudioSource>();
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
        audioClipTimeTotal += Time.deltaTime;

        if (CheckAttackDistance())
        {
            audioClipTimeTotal = audioClipTime;
            animator.SetBool("Run", false);
            movingState = State.ATTACK;
        } else {
            if (audioClipTimeTotal >= audioClipTime) {
                PlayRandomAudioClip(chaseAudioClips, false);
                audioClipTimeTotal = 0f;
            }
        }
    }

    private void Attack()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = Vector3.zero;
        PlayRandomAudioClip(attackAudioClips, false);

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

    public void PlayLeftFootStep(string s)
    {
        footstepAudio.clip = leftFootStep;
        footstepAudio.Play();
    }

    public void PlayRightFootStep(string s)
    {
        footstepAudio.clip = rightFootStep;
        footstepAudio.Play();
    }

    public void EnableRightHand(string s)
    {
        rightHandCollider.Enable();
    }

    public void DisableRightHand(string s)
    {
        rightHandCollider.Disable();
    }

    public void EnableLeftHand(string s)
    {
        leftHandCollider.Enable();
    }

    public void DisableLeftHand(string s)
    {
        leftHandCollider.Disable();
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
        audioClipTimeTotal = audioClipTime;
        attackTimeTotal = attackTime;
        if (movingState == State.ATTACK) {
            attacking = true;
        }
        gettingHit = false;
    }

    public void DeathFinished(string s)
    {
        StartCoroutine(DestroyWait());
    }

    public void DealDamage(float amount, bool isCentral, bool isLeft, bool isRight)
    {
        playerHealth.ApplyDamage(amount, isCentral, isLeft, isRight);
    }

    private void TriggerShout() {
        shouting = true;
        animator.SetTrigger("Shout 2");
        PlayRandomAudioClip(shoutAudioClips, true);
        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = Vector3.zero;
    }

    private void CheckIfDead() {
        if (cannibalHealth.isDead() && !dying) {
            PlayRandomAudioClip(deadAudioClips, true);
            dying = true;
            headCollider.EnableTrigger();
            movingState = State.DEAD;
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;
            TriggerRandomAnimation(deathAnimations);
        }
    }

    public void Hit(float amount, bool isCentral, bool isLeft, bool isRight)
    {
        if (!attacking && !gettingHit && !shouting && !dying)
        {
            audioClipTimeTotal = 1.5f;
            if (movingState == State.WALK) {
                animator.SetBool("Walk", false);
                TriggerShout();
                movingState = State.CHASE;
            } else {
                PlayRandomAudioClip(hitAudioClips, false);
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

    IEnumerator DestroyWait()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }

    private void PlayRandomAudioClip(AudioClip[] clips, bool interrupt) {
        if (!audioSource.isPlaying || interrupt)
        {
            audioSource.clip = clips[Random.Range(0, clips.Length)];
            audioSource.Play();
        }
    }
}
