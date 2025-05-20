using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Robot : MonoBehaviour
{
    [SerializeField] int zombieAttackDamage = 2;

    FirstPersonController player;
    NavMeshAgent agent;
    Animator animator;

    const string PLAYER_STRING = "Player";
    float steeringSpeed;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        steeringSpeed = agent.speed;

        // Randomize animation start time
        animator = GetComponentInChildren<Animator>();
        animator.Play(0, 0, Random.Range(0f, 1f));

    }

    void Start()
    {
        player = FindFirstObjectByType<FirstPersonController>();
    }

    void Update()
    {
        if (!player) return;

        agent.SetDestination(player.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING))
        {

            PlayerHealth playerHealth = other.gameObject.GetComponentInParent<PlayerHealth>();
            playerHealth?.TakeDamage(zombieAttackDamage);

            agent.isStopped = true;
            agent.speed = 0;
            agent.velocity = Vector3.zero;

            animator.SetBool("isAttacking", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            animator.SetBool("isAttacking", false);

            StartCoroutine(WaitForAttackAnimationToEnd());
        }
    }

    IEnumerator WaitForAttackAnimationToEnd()
    {
        // Get the AnimatorStateInfo for the current animation playing on the base layer (usually layer 0)
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Wait while the attack animation is playing
        // Assuming your attack animation has the tag "Attack" or name "Attack"
        while (stateInfo.IsName("ZombieAttack") && stateInfo.normalizedTime < 1.0f)
        {
            yield return null;  // wait for next frame
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        // Animation finished, resume movement
        agent.isStopped = false;
        agent.speed = steeringSpeed;

        if (player != null)
        {
            agent.SetDestination(player.transform.position);
        }
    }
}
