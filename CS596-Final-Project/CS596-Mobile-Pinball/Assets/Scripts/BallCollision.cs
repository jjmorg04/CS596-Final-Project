using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
    if (collision.gameObject.CompareTag("Flipper"))
        {
            ScoreManager.instance.AddPoint(1); //Adds 1 point
        }
    }
}
