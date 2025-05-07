using UnityEngine;

public class PlungerHandle : MonoBehaviour
{
    public GameObject ball;      // your pinball
    public float maxPull = 0.5f; // how far it can travel
    public float pullSpeed = 1f;  // units/sec it retreats
    public float maxForce = 4750f;     // impulse at full pull

    // start position
    private Vector3 startLocalPos;
    private float   currentPull;
    private bool    isCharging;

    void Start()
    {
        startLocalPos = transform.localPosition;
        currentPull   = 0f;
        isCharging    = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isCharging  = true;
            currentPull = 0f;
        }

        if (isCharging && Input.GetKey(KeyCode.Space))
        {
            // accumulate pull
            currentPull += pullSpeed * Time.deltaTime;
            currentPull  = Mathf.Min(currentPull, maxPull);
            transform.localPosition = startLocalPos - Vector3.forward * currentPull;
        }

        if (isCharging && Input.GetKeyUp(KeyCode.Space))
            Fire();
    }

    private void Fire()
    {
        // scale pull → force
        float force = (currentPull / maxPull) * maxForce;

        if (ball != null)
        {
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
                // shoot in the plunger's forward direction
                rb.AddForce(transform.forward * force, ForceMode.Impulse);
        }

        // reset
        transform.localPosition = startLocalPos;
        currentPull             = 0f;
        isCharging              = false;
    }
}
