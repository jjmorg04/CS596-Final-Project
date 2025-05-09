using UnityEngine;

public class PinballBumper : MonoBehaviour
{
    public float bumperForce = 20f;
    public float maxBallSpeed = 30f;
    public AudioClip hitSound;

    private AudioSource audioSource;

    void Start()
    {
        // Optional: attach one to this object or let it remain null and fall back
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.7f; // volume from 0 (mute) to 1 (full)

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            ScoreManager.Instance.AddScore(100);
            Rigidbody ballRigidbody = collision.rigidbody;

            // ✅ Play sound using PlayOneShot if possible, fallback if needed
            if (hitSound != null)
            {
                if (audioSource != null)
                    audioSource.PlayOneShot(hitSound);
                else
                    AudioSource.PlayClipAtPoint(hitSound, transform.position);
            }

            if (ballRigidbody != null)
            {
                Vector3 direction = (collision.transform.position - transform.position).normalized;
                ballRigidbody.AddForce(direction * bumperForce, ForceMode.Impulse);

                if (ballRigidbody.linearVelocity.magnitude > maxBallSpeed)
                {
                    ballRigidbody.linearVelocity = ballRigidbody.linearVelocity.normalized * maxBallSpeed;
                }
            }
        }
    }
}
