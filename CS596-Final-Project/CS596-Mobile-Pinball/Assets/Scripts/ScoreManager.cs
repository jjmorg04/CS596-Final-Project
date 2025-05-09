using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TMP_Text Scoreboard;
    public TMP_Text HighScoreHUD;

    private int score = 0;
    private const string HighScoreKey = "HighScore";

    void Awake()
    {
        Instance = this;

        // Display high score at game start
        if (HighScoreHUD != null)
        {
            int highScore = GetHighScore();
            HighScoreHUD.text = "PB " + highScore.ToString("D4");
        }
    }

    // Add score value and display
    public void AddScore(int value)
    {
        score += value;
        Scoreboard.text = "SCORE " + score.ToString("D4");
    }

    public int GetScore()
    {
        return score;
    }

    // Saves new high score if score is higher
    public void SaveHighScore()
    {
        int currentHigh = PlayerPrefs.GetInt(HighScoreKey, 0);
        if (score > currentHigh)
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
            PlayerPrefs.Save();

            if (HighScoreHUD != null)
                HighScoreHUD.text = "PB " + score.ToString("D4");
        }
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HighScoreKey, 0);
    }

// Reset score option for demo play
[ContextMenu("Reset High Score")]
public void ResetHighScore()
{
    PlayerPrefs.DeleteKey("HighScore");
    Debug.Log("High score reset.");
}

}

