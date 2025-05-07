using UnityEngine;
using System.Collections;

public class PlungerHandle : MonoBehaviour
{
    public GameObject ball;               // Assign your pinball GameObject in the Inspector
    public float maxPull = 1.0f;          // Maximum pullback distance
    public float pullPerPress = 0.1f;     // Pullback added per space bar press
    public float autoFireDelay = 2.0f;    // Seconds before auto-fire
    public float maxForce = 500f;         // Maximum force applied to the ball

    private Vector3 startPos;
    private float currentPull = 0f;
    private bool timerActive = false;
    private Coroutine fireCoroutine;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Space bar pressed
        {
            AddPower();

            if (!timerActive)
            {
                fireCoroutine = StartCoroutine(AutoFire());
                timerActive = true;
            }
        }
    }

    void AddPower()
    {
        currentPull = Mathf.Min(currentPull + pullPerPress, maxPull);
        transform.position = startPos - Vector3.forward * currentPull; // Pull handle visually
    }

    IEnumerator AutoFire()
    {
        yield return new WaitForSeconds(autoFireDelay);
        Fire();
    }

    void Fire()
    {
        float force = (currentPull / maxPull) * maxForce;
        if (ball != null)
        {
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddForce(Vector3.forward * force);
        }
        // Reset handle
        transform.position = startPos;
        currentPull = 0f;
        timerActive = false;
    }
}
