using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TMP_Text Scoreboard;
    private int score = 0;

    void Awake()
    {
        Instance = this;
    }

    public void AddScore(int value)
    {
        score += value;
        Scoreboard.text = "SCORE " + score.ToString("D4");
    }
    public int GetScore()
    {
        return score;
    }
}

