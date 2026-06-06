using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public SpawnManager spawner;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;

    void Start()
    {
        gameOverPanel.SetActive(false);
        UpdateScoreUI(0);
    }

    public void UpdateScoreUI(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = "Congratulations! You Won!";
    }

    //called from button
    public void RestartGame()
    {
        spawner.enabled = true;
        Start();
    }
}