using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public static ScoringSystem Instance;

    public Text scoreText;
    public Text highScoreText;

    private int score = 0;
    private int highScore = 1000;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        ResetScores(); // Reset both score and high score at the start
        UpdateUI();
        EnemyType1.OnEnemyDied += AddScore; // Subscribe to enemy death event
        EnemyType2.OnEnemyDied += AddScore;
        EnemyType3.OnEnemyDied += AddScore;
        EnemyType4.OnEnemyDied += AddScore;
    }

    void AddScore(int points)
    {
        score += points;
        if (score > highScore)
        {
            highScore = score;
        }
        UpdateUI();
    }

    public void ResetScores()
    {
        score = 0;
        highScore = 1000; // Reset high score each playthrough
        PlayerPrefs.SetInt("HighScore", 0); // Clear saved high score
        PlayerPrefs.Save();
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score.ToString("D4"); // Always show 4 digits (0000+)
        highScoreText.text = "Hi-Score: " + highScore.ToString("D4");
    }

}
