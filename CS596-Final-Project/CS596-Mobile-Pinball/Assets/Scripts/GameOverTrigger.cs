using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

// Scripting for end game
public class GameOverTrigger : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text ballsLeftText;

    [Header("Ball Settings")]
    [SerializeField] private string ballTag = "Ball";
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballSpawnPoint;
    [SerializeField] private int totalBalls = 3;

    private ScoreManager scoreManager;
    private int ballsLeft;
    private bool gameEnded = false;

    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
            Debug.LogWarning("No ScoreManager found in scene!");

        ballsLeft = totalBalls;
        UpdateBallsUI();
        SpawnNewBall();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(ballTag)) return;

        Destroy(other.gameObject);
        ballsLeft--;
        UpdateBallsUI();

        if (ballsLeft > 0)
        {
            Invoke(nameof(SpawnNewBall), 1.5f); // Wait before spawning next ball
        }
        else
        {
            if (!gameEnded)
            {
                gameEnded = true;
                EndGame();
            }
        }
    }

    private void SpawnNewBall()
    {
        if (ballPrefab != null && ballSpawnPoint != null)
        {
            GameObject newBall = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);
            Rigidbody rb = newBall.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = true;
                StartCoroutine(EnablePhysics(rb));
            }
        }
    }

    private IEnumerator EnablePhysics(Rigidbody rb)
    {
        yield return new WaitForFixedUpdate(); // Allow spawn to settle
        rb.isKinematic = false;
    }

    private void UpdateBallsUI()
    {
        if (ballsLeftText != null)
        {
            ballsLeftText.text = "BALLS " + ballsLeft;
        }
    }

    private void EndGame()
    {
        if (scoreManager != null)
        {
            scoreManager.SaveHighScore();
        }

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (gameOverText != null)
            gameOverText.text = "GAME OVER";

        if (finalScoreText != null && scoreManager != null)
        {
            finalScoreText.text =
                "FINAL SCORE\n" + scoreManager.GetScore().ToString("D4");
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


