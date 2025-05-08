using UnityEngine;

public class PinballBumper : MonoBehaviour
{
    public float bumperForce = 20f;         // Reduce force to a more realistic level
    public float maxBallSpeed = 30f;        // Clamp max speed after bounce
    public AudioClip hitSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            ScoreManager.Instance.AddScore(100);
            Rigidbody ballRigidbody = collision.rigidbody;

            if (audioSource != null && hitSound != null)
                audioSource.PlayOneShot(hitSound);

            if (ballRigidbody != null)
            {
                // Calculate outward direction from bumper to ball
                Vector3 direction = (collision.transform.position - transform.position).normalized;

                // Apply force
                ballRigidbody.AddForce(direction * bumperForce, ForceMode.Impulse);

                // Clamp the velocity to maxBallSpeed
                if (ballRigidbody.linearVelocity.magnitude > maxBallSpeed)
                {
                    ballRigidbody.linearVelocity = ballRigidbody.linearVelocity.normalized * maxBallSpeed;
                }
            }
        }
    }
}