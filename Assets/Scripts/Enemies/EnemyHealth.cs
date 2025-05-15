using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject robotExplosionVFX;
    [SerializeField] float positionYoffset = 0f;
    [SerializeField] float rotationYoffset = 190f;
    [SerializeField] float rotationZoffset = 50f;

    [SerializeField] int startingHealth = 3;
    
    int currentHealth;

    GameManager gameManager;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        gameManager.AdjustEnemiesLeft(1);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
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
        }
        Destroy(gameObject);

    }
}
