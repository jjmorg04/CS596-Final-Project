using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TMP_Text Scoreboard;
    private int score = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void AddPoint(int points)
    {
        score += points;
        Scoreboard.text = "Score: " + score;
    }
}
