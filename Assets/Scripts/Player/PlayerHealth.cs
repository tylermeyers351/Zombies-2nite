using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Range(1,10)]
    [SerializeField] int startingHealth = 10;
    [SerializeField] int currentHealth;
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] Transform weaponCamera;
    [SerializeField] GameObject overlayCanvas;
    [SerializeField] Image[] shieldBars;
    [SerializeField] GameObject gameOverContainer;
    [SerializeField] GameObject ammoContainer;
    [SerializeField] GameObject shieldContainer;

    int gameOverVirtualCameraPriority = 20;
    // int shieldBarsCount;

    void Awake()
    {
        currentHealth = startingHealth;
        AdjustShieldUI();
        // shieldBarsCount = shieldBars.Length - 1;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        AdjustShieldUI();

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
        // int count = 0;
        // for (int i = shieldBarsCount; i >= 0 && count < damage; i--)
        // {
        //     Debug.Log("Iteration: " + shieldBars[i]); 
        //     shieldBars[i].gameObject.SetActive(false);
        //     count++;
        //     shieldBarsCount--;
        // }
    }
}
