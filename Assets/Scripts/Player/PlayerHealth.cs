using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [Range(1, 10)]
    [SerializeField] int startingHealth = 10;
    [SerializeField] int currentHealth;


    [Header("Camera")]
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] Transform weaponCamera;

    [Header("Canvas")]
    [SerializeField] GameObject overlayCanvas;
    [SerializeField] Image[] shieldBars;
    [SerializeField] GameObject gameOverContainer;
    [SerializeField] GameObject ammoContainer;
    [SerializeField] GameObject shieldContainer;

    [Header("Damage Vignette Settings")]
    [SerializeField] Volume damageVolume;
    [SerializeField] float vignetteFlashIntensity = 0.4f;
    [SerializeField] float vignetteFadeSpeed = 2f;
    Vignette vignette;

    int gameOverVirtualCameraPriority = 20;
    PlayerAudio playerAudio;


    void Awake()
    {
        currentHealth = startingHealth;
        AdjustShieldUI();
        // Try to get the vignette from the volume profile
        if (damageVolume != null && damageVolume.profile.TryGet(out vignette))
        {
            vignette.intensity.value = 0f; // Ensure it starts invisible
        }
        else
        {
            Debug.LogWarning("Vignette not found in volume profile.");
        }

        playerAudio = GetComponent<PlayerAudio>();
    }

    void Update()
    {
        // Fade the vignette back to 0 over time
        if (vignette != null)
        {
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0f, Time.deltaTime * vignetteFadeSpeed);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        AdjustShieldUI();
        ApplyDamageVignette();
        playerAudio.playHurtPlayerAudio();

        if (currentHealth <= 0)
        {
            weaponCamera.parent = null;
            deathVirtualCamera.Priority = gameOverVirtualCameraPriority;
            StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
            starterAssetsInputs.SetCursorState(false);
            Destroy(this.gameObject);

            gameOverContainer.SetActive(true);
            ammoContainer.SetActive(false);
            shieldContainer.SetActive(false);
        }
    }

    void AdjustShieldUI()
    {
        for (int i = 0; i < shieldBars.Length; i++)
        {
            if (i < currentHealth)
            {
                shieldBars[i].gameObject.SetActive(true);
            }
            else
            {
                shieldBars[i].gameObject.SetActive(false);
            }
        }
    }

    void ApplyDamageVignette()
    {
        if (vignette != null)
        {
            vignette.intensity.value = vignetteFlashIntensity;
        }
    }

    

}
