using UnityEngine;
using System.Collections;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] float respawnTime = 5f;
    const string playerString = "Player";

    // Cache references
    Collider pickupCollider;
    Renderer[] renderers;

    void Awake()
    {
        pickupCollider = GetComponent<Collider>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerString))
        {
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
            OnPickup(activeWeapon);

            PlayerAudio playerAudio = other.GetComponent<PlayerAudio>();
            if (playerAudio != null)
            {
                playerAudio.playReloadAudio();
            }
            else
            {
                Debug.Log("PlayerAudio not found on player.");
            }

            // Destroy(this.gameObject);

            // Instead of destroying, deactivate and respawn after delay
            StartCoroutine(DisableAndRespawn());
        }
    }

    IEnumerator DisableAndRespawn()
    {
        // Disable visuals and collider
        pickupCollider.enabled = false;
        foreach (var r in renderers)
        {
            r.enabled = false;
        }

        yield return new WaitForSeconds(respawnTime);

        // Reactivate visuals and collider
        pickupCollider.enabled = true;
        foreach (var r in renderers)
        {
            r.enabled = true;
        }
    }

    protected abstract void OnPickup(ActiveWeapon activeWeapon);
}
