using UnityEngine;
using UnityEngine.UI;

public class ScoreZone : MonoBehaviour
{
    public int scoreValue = 100;       // Points awarded
    public Text scoreText;             // Reference to UI Text
    public AudioClip hitSound;         // Sound to play on hit
    [Range(0f, 1f)] public float hitVolume = 0.5f; // Custom volume for sound

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
        if (other.CompareTag("Ball"))
        {
            currentScore += scoreValue;
            ScoreManager.Instance.AddScore(scoreValue);
            Debug.Log("BALL HIT");

            if (scoreText != null)
                UpdateScoreUI();

            PlaySound();
        }
    }

    void PlaySound()
    {
        if (hitSound == null) return;

        if (audioSource != null)
            audioSource.PlayOneShot(hitSound, hitVolume);
        else
            AudioSource.PlayClipAtPoint(hitSound, transform.position, hitVolume);
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + currentScore;
    }
}
