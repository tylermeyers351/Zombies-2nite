using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;
using Cinemachine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{

    [SerializeField] TMP_Text enemiesLeftText;
    [SerializeField] GameObject youWinText;
    [SerializeField] GameObject startGameUI;
    [SerializeField] CinemachineVirtualCamera startVirtualCamera;
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject activeGameUI;

    int startGameVirtualCameraPriority = 0;
    StarterAssetsInputs starterAssetsInputs;

    [SerializeField] Volume damageVolume;
    Vignette vignette;
    public float vignetteValue = .5f;

    int enemiesKilled = 0;

    public bool gameStarted = false;

    const string ENEMIES_LEFT_STRING = "Zombies Killed: ";

    void Start()
    {
        if (damageVolume != null && damageVolume.profile.TryGet(out vignette))
        {
            vignette.intensity.value = vignetteValue;
        }
        else
        {
            Debug.LogWarning("Vignette not found in volume profile.");
        }
        if (!gameStarted)
        {
            starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
            starterAssetsInputs.SetCursorState(false);
        }
    }

    public void AdjustEnemiesKilled(int amount)
    {
        enemiesKilled += amount;
        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesKilled.ToString();

        // if (enemiesLeft <= 0)
        // {
        //     youWinText.SetActive(true);
        // }
    }

    public void RestartLevelButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void QuitButton()
    {
        Debug.LogWarning("Does not work in the Unity Editor.");
        Application.Quit();
    }

    public void StartLevelButton()
    {
        if (!gameStarted)
        {
            Debug.Log("Game started!");
            gameStarted = true;
            startGameUI.SetActive(false);
            startVirtualCamera.Priority = startGameVirtualCameraPriority;
            starterAssetsInputs.SetCursorState(true);
            crosshair.SetActive(true);
            activeGameUI.SetActive(true);

            vignetteValue = 0.1f;
            vignette.intensity.value = vignetteValue;
        }
    }
}
