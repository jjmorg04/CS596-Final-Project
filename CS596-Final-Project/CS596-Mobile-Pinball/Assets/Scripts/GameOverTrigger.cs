using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private TMP_Text finalScoreText;

    [Header("Settings")]
    [SerializeField] private string ballTag = "Ball";

    private ScoreManager scoreManager;
    private bool gameEnded = false;

    void Awake()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
            Debug.LogWarning("No ScoreManager found in scene!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameEnded) return;

        if (other.CompareTag(ballTag))
        {
            gameEnded = true;
            EndGame(other.gameObject);
        }
    }

    private void EndGame(GameObject ball)
    {
        // stop the ball’s physics
        if (ball.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        // save high score
        if (scoreManager != null)
        {
            scoreManager.SaveHighScore();
        }

        // show the panel
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // set the texts
        if (gameOverText != null)
            gameOverText.text = "GAME OVER";

        if (finalScoreText != null && scoreManager != null)
        {
            finalScoreText.text = "FINAL SCORE\n" + scoreManager.GetScore().ToString("D4");
                                
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

