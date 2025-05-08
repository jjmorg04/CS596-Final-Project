using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallVelocityLimiter : MonoBehaviour
{
    public float maxSpeed = 20f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }
}