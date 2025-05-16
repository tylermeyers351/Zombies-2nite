using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject robotExplosionVFX;
    [SerializeField] float positionYoffset = 0f;
    [SerializeField] float rotationYoffset = 190f;
    [SerializeField] float rotationZoffset = 50f;

    [SerializeField] int startingHealth = 3;
    [SerializeField] float timeToDestroy = 8f;

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
        gameManager.AdjustEnemiesLeft(1);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (!isDead && currentHealth <= 0)
        {
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        gameManager.AdjustEnemiesLeft(-1);

        Quaternion positionRotation = Quaternion.Euler(0f, rotationYoffset, rotationZoffset);
        Vector3 spawnPosition = transform.position + Vector3.up * positionYoffset;

        if (robotExplosionVFX)
        {
            Instantiate(robotExplosionVFX, spawnPosition, positionRotation);
            Destroy(gameObject);
        }

        if (agent)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;

            animator.SetTrigger("Die");
            isDead = true;
            Destroy(gameObject, timeToDestroy);
        }

    }
}
