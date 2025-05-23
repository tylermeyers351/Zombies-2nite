using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject robotExplosionVFX;
    [SerializeField] float positionYoffset = 0f;
    [SerializeField] float rotationYoffset = 190f;
    [SerializeField] float rotationZoffset = 50f;

    [SerializeField] int startingHealth = 3;
    [SerializeField] float timeToDestroy = 45f;

    [SerializeField] AudioSource loopedAudioSource;
    [SerializeField] AudioSource deathAudioSource;
    [SerializeField] AudioClip deathGroan1;
    [SerializeField] AudioClip deathGroan2;

    bool isDead = false;
    int currentHealth;
    GameManager gameManager;
    NavMeshAgent agent;
    Animator animator;

    void Awake()
    {
        currentHealth = startingHealth;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        // gameManager.AdjustEnemiesKilled(1);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (!isDead && currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        gameManager.AdjustEnemiesKilled(1);

        Robot robot = GetComponent<Robot>();
        if (robot != null)
        {
            robot.HandleDeath();
        }

        Quaternion positionRotation = Quaternion.Euler(0f, rotationYoffset, rotationZoffset);
        Vector3 spawnPosition = transform.position + Vector3.up * positionYoffset;

        // For non-zombie enemies
        if (robotExplosionVFX)
        {
            Instantiate(robotExplosionVFX, spawnPosition, positionRotation);
            Destroy(gameObject);
        }

        // For zombie enemies
        if (agent)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            agent.speed = 0;

            animator.applyRootMotion = true;
            animator.SetTrigger("Die");
            isDead = true;

            PlayRandomDeathGroan();

            foreach (CapsuleCollider col in GetComponentsInChildren<CapsuleCollider>())
            {
                col.enabled = false;
            }

            Destroy(gameObject, timeToDestroy);
        }
    }

    void PlayRandomDeathGroan()
    {
        loopedAudioSource.Stop();
        AudioClip chosenClip = Random.value < 0.5f ? deathGroan1 : deathGroan2;
        deathAudioSource.clip = chosenClip;
        deathAudioSource.Play();
    }
}
