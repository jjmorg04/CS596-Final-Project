using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LeftFlipper"))
        {
            ScoreManager.Instance.AddPoint();
        }
    }
}
