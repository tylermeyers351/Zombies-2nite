using Cinemachine;
using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] LayerMask interactionLayers;
    
    CinemachineImpulseSource impulseSource;

    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shoot(WeaponSO weaponSO)
    {        
        RaycastHit hit;
        muzzleFlash.Play();
        impulseSource.GenerateImpulse();

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore))
        {   
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Zombie"))
            {
                // Align with the surface normal (facing into the surface)
                Quaternion rotation = Quaternion.LookRotation(-hit.normal);

                // Apply random rotation around that forward axis
                rotation *= Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

                Instantiate(weaponSO.ZombieHitVFX, hit.point, rotation);
            }
            else
            {
                Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);
            }
            
            EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage);

            Debug.Log("HITTING: " + hit.collider);
        }
    }
}
