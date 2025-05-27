using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;
using Cinemachine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System.Collections;

public class GameManager : MonoBehaviour
{

    [SerializeField] TMP_Text enemiesLeftText;
    [SerializeField] GameObject youWinText;
    [SerializeField] GameObject startGameUI;
    [SerializeField] CinemachineVirtualCamera startVirtualCamera;
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject activeGameUI;
    [SerializeField] AudioSource laughAudioSource;
    [SerializeField] AudioSource chainAudioSource;
    [SerializeField] GameObject introText;

    int startGameVirtualCameraPriority = 0;
    StarterAssetsInputs starterAssetsInputs;

    [SerializeField] Volume damageVolume;
    Vignette vignette;
    public float vignetteValue = .5f;

    int enemiesKilled = 0;
    float cameraTransitionTime = 1.8f;
    float introTime = 6f;

    public bool gameStarted = false;
    public bool gameEnded = false;

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

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if (gameEnded && Input.GetKeyDown(KeyCode.Space))
        {
            RestartLevel();
            gameEnded = false;
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

    public void RestartLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void QuitButton()
    {
        Debug.LogWarning("Does not work in the Unity Editor.");
        Application.Quit();
    }

    public void StartGame()
    {
        if (!gameStarted)
        {
            startGameUI.SetActive(false);
            startVirtualCamera.Priority = startGameVirtualCameraPriority;
            starterAssetsInputs.SetCursorState(true);

            laughAudioSource.Play();

            vignetteValue = 0.3f;
            vignette.intensity.value = vignetteValue;

            StartCoroutine(DelayControls());
        }
    }

    IEnumerator DelayControls()
    {
        yield return new WaitForSeconds(cameraTransitionTime);
        introText.SetActive(true);
        gameStarted = true;
        yield return new WaitForSeconds(introTime);
        introText.SetActive(false);
        crosshair.SetActive(true);
        activeGameUI.SetActive(true);
        chainAudioSource.volume = 0.1f;
    }
    
}
