using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
public class NewMonoBehaviourScript : MonoBehaviour
{
    public static ScoreManager instance;
    public TMP_Text Scoreboard;
    private int score = 0;

    void Awake()
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
    }
    public void AddPoint(int points)

    // Update is called once per frame
    void Update()
    {
        score += points;
        Scoreboard.text = "Score: " + score;
        
    }
}