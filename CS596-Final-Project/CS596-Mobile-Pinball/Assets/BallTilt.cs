using UnityEngine;

public class BallTiltControl : MonoBehaviour
{
    public Rigidbody ball;
    public float tiltForce = 5f; // tweak to taste

    private GameObject lastTrackedBall;

    void Update()
    {
        // Automatically find the latest ball if null or destroyed
        if (ball == null)
        {
            FindLatestBall();
        }

        if (ball != null)
        {
            Vector3 tilt = Input.acceleration;
            Vector3 force = new Vector3(tilt.x, 0f, tilt.y) * tiltForce;

            if (force.magnitude > 0.02f)
            {
                ball.AddForce(force, ForceMode.Force);
            }
        }
    }

    void FindLatestBall()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        if (balls.Length > 0)
        {
            GameObject latest = balls[balls.Length - 1];
            Rigidbody rb = latest.GetComponent<Rigidbody>();
            if (rb != null)
            {
                ball = rb;
                lastTrackedBall = latest;
            }
        }
    }
}

