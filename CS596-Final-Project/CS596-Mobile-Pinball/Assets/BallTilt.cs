using UnityEngine;

public class BallTiltControl : MonoBehaviour
{
    public Rigidbody ball;
    public float tiltForce = 5f; // tweak to taste

    void Update()
    {
        Vector3 tilt = Input.acceleration;

        // Only use x and y (flattened to your board’s axis)
        Vector3 force = new Vector3(tilt.x, 0f, tilt.y) * tiltForce;

        // Optional: filter out small jitters
        if (force.magnitude > 0.02f)
        {
            ball.AddForce(force, ForceMode.Force);
        }
    }
}
