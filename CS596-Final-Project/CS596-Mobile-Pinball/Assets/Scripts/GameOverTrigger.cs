using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject gameOverPanel;  // the panel object
    [SerializeField] private TMP_Text  gameOverText;   // your “GAME OVER” text (TMP)
    [SerializeField] private TMP_Text  finalScoreText; // the final score text (TMP)

    [Header("Settings")]
    [SerializeField] private string ballTag = "Ball";

    private ScoreManager scoreManager;
    private bool         gameEnded = false;

    void Awake()
    {
        // hide panel at start
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // find your ScoreManager
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

        // show the panel
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // set the texts
        if (gameOverText != null)
            gameOverText.text = "GAME OVER";

        if (finalScoreText != null && scoreManager != null)
            finalScoreText.text = "Final Score\n" + scoreManager.GetScore();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
