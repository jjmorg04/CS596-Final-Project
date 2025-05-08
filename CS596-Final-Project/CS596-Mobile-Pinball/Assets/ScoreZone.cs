using UnityEngine;
using UnityEngine.UI;

public class ScoreZone : MonoBehaviour
{
    public int scoreValue = 100; // Points awarded
    public Text scoreText;       // Reference to the UI Text element
    public AudioClip hitSound;   // Sound to play on hit

    private static int currentScore = 0;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (scoreText != null)
            UpdateScoreUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) // Ensure the object is the pinball
        {

            currentScore += scoreValue;

            Debug.Log("BALL HIT");

            ScoreManager.Instance.AddScore(100);

            if (scoreText != null)
                UpdateScoreUI();

            if (audioSource != null && hitSound != null)
                audioSource.PlayOneShot(hitSound);
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + currentScore;
    }
}