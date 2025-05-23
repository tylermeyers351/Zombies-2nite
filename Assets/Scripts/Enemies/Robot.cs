using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Robot : MonoBehaviour
{
    [SerializeField] int zombieAttackDamage = 2;
    [SerializeField] float firstDelayDamageAttack = 1f;
    [SerializeField] float subsequentDelayDamageAttack = 2.6f;
    float delayDamageAttack;

    FirstPersonController player;
    NavMeshAgent agent;
    Animator animator;

    const string PLAYER_STRING = "Player";
    float steeringSpeed;
    int attackCount = 0;

    bool isPlayerInRange = false;
    Coroutine damageCoroutine = null;

    bool isDead = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        steeringSpeed = agent.speed;

        // Randomize animation start time
        animator = GetComponentInChildren<Animator>();
        animator.Play(0, 0, Random.Range(0f, 1f));

        delayDamageAttack = firstDelayDamageAttack;

    }

    void Start()
    {
        player = FindFirstObjectByType<FirstPersonController>();
    }

    void Update()
    {
        if (player == null)
        {
            isPlayerInRange = false;

            // OPTIONAL: Stop attack animation and coroutine if player disappears
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }

            animator.SetBool("isAttacking", false);
            return;
        }

        agent.SetDestination(player.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag(PLAYER_STRING))
        {
            Debug.Log("Entered collider");

            delayDamageAttack = firstDelayDamageAttack;

            isPlayerInRange = true;

            agent.isStopped = true;
            agent.speed = 0;
            agent.velocity = Vector3.zero;


            animator.SetBool("isAttacking", true);

            PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();

            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(RepeatDamage(other));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            Debug.Log("Exited collider");

            isPlayerInRange = false;

            animator.SetBool("isAttacking", false);

            if (damageCoroutine != null)
            {
                if (attackCount > 0)
                {
                    StopCoroutine(damageCoroutine);
                    damageCoroutine = null;
                }
            }
            StartCoroutine(WaitForAttackAnimationToEnd());
        }
        attackCount = 0;
    }

    IEnumerator WaitForAttackAnimationToEnd()
    {
        // Get the AnimatorStateInfo for the current animation playing on the base layer (usually layer 0)
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Wait while the attack animation is playing
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("ZombieAttack"))
        {
            yield return null;
        }

        // Animation finished, resume movement
        agent.isStopped = false;
        if (!isDead)
        {
            agent.speed = steeringSpeed;
        }

        if (player != null)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    IEnumerator RepeatDamage(Collider other)
    {
        PlayerHealth playerHealth = other.gameObject.GetComponentInParent<PlayerHealth>();

        while (isPlayerInRange && !isDead)
        {
            yield return new WaitForSeconds(delayDamageAttack);
            playerHealth?.TakeDamage(zombieAttackDamage);
            delayDamageAttack = subsequentDelayDamageAttack;
            attackCount += 1;
        }
    }

    public void HandleDeath()
    {
        Debug.Log("Death handled");
        isDead = true;
        isPlayerInRange = false;

        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }

        animator.SetBool("isAttacking", false);

        agent.isStopped = true;
        agent.speed = 0;
        agent.velocity = Vector3.zero;
    }

}
