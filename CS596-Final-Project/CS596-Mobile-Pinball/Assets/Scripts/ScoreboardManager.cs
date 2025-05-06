using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    public TMP_Text scoreboard;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddPoint()
    {
        score++;
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        scoreboard.text = "Score: " + score;
    }
}
