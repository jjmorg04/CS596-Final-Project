using UnityEngine;

// Scripting for ball & flipper collision for point trigger
public class BallCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Flipper"))
        {
            Debug.Log("Hit a flipper!");
            ScoreManager.Instance.AddScore(100);
        }
    }
}

