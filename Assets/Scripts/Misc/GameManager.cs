using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] TMP_Text enemiesLeftText;
    [SerializeField] GameObject youWinText;

    int enemiesKilled = 0;

    const string ENEMIES_LEFT_STRING = "Enemies Killed: ";

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
}
