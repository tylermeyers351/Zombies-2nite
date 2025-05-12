using System;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;
    [SerializeField] int damageExplosion = 3;

    void Start()
    {
        Explode();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);        
    }

    void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var collider in hitColliders)
        {
            
            PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
            
            if (!playerHealth) continue;

            playerHealth.TakeDamage(damageExplosion);

            break;
        }
    }
}
