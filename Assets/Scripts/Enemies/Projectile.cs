using Unity.Mathematics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject projectileHitVFX;
    
    Rigidbody rb;
    GameObject playerCameraRoot;

    int damage = 2;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        playerCameraRoot = GameObject.Find("PlayerCameraRoot");

        if (playerCameraRoot)
        {
            Vector3 direction = (playerCameraRoot.transform.position - transform.position).normalized;
            rb.linearVelocity = direction * projectileSpeed;
        }
        else
        {
            Debug.Log("PlayerCameraRoot not found");
        }
    }

    public void Init(int damage)
    {
        this.damage = damage;
    }

    void OnCollisionEnter(Collision collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponentInParent<PlayerHealth>();
        playerHealth?.TakeDamage(damage);
        Instantiate(projectileHitVFX, transform.position, quaternion.identity);
        Destroy(this.gameObject);
    }
}
