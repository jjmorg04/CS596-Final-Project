using UnityEngine;
using System.Collections;

public class PlungerHandle : MonoBehaviour
{
    public GameObject ball;               // ball
    public float maxPull = 1.0f;          // how far handle travels
    public float pullPerPress = 0.1f;     // pullback per space
    public float autoFireDelay = 2.0f;    // seconds until auto-fire
    public float maxForce = 500f;         // max force on ball

    private Vector3 startPos;
    private float currentPull = 0f;
    private bool timerActive = false;
    private Coroutine fireCoroutine;

    public float colliderDisableTime = 0.5f;




    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // space press
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
        transform.position = startPos - Vector3.forward * currentPull; // moves handle back
    }

    IEnumerator AutoFire()
    {
        yield return new WaitForSeconds(autoFireDelay);
        Fire();
    }

    void Fire()
    {
        Collider handleCollider = GetComponent<Collider>();

        if (handleCollider != null)
        {
            handleCollider.enabled = false;
        }


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
        StartCoroutine(ReEnableCollider());
    }



    IEnumerator ReEnableCollider()
    {
        yield return new WaitForSeconds(colliderDisableTime);
        Collider handleCollider = GetComponent<Collider>();
        if(handleCollider != null)
        {
            handleCollider.enabled = true;
        }

    }
}