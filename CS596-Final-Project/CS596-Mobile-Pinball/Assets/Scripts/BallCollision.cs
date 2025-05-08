using UnityEngine;

public class BallCollision : MonoBehaviour
public class bvall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (collision.gameObject.CompareTag("LeftFlipper"))
        {
            ScoreManager.Instance.AddPoint();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}