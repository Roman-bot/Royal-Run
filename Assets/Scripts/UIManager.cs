using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text scoreText;

    [SerializeField]
    TMP_Text timerText;

    [SerializeField]
    GameObject gameOverPanel;

    [SerializeField]
    PlayerController playerController;

    bool isGameOver = false; // Flag to check if the game is over
    public bool gameOver { get { return isGameOver; } }


    int currentScore;
    public float timer = 2f;

    public void UpdateScore(int score)
    {
        currentScore += score; // Increment the current score by the given score
        scoreText.text = "Score: " + currentScore.ToString();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver) return; // If the game is over, skip the rest of the Update method  
        timer -= Time.deltaTime; // Decrease the timer by the time passed since the last frame  
        timerText.text = "Time: " + timer.ToString("F1"); // Update the timer text with a format specifier for one decimal place  
        if (timer <= 0)
        {
            // GameOver();  
        }
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true); // Activate the game over panel
        Time.timeScale = 0.1f; // Stop the game by setting time scale to 0
        playerController.enabled = false; // Disable player controls
        isGameOver = true; // Set the game over flag to true
    }
}
