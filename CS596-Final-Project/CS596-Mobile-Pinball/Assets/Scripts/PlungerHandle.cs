using UnityEngine;
using System.Collections;

public class PlungerHandle : MonoBehaviour
{
    public GameObject ball;               // ball
    public float maxPull = 1.0f;          // how far handle travels
    public float pullPerSecond = 0.5f;    // pullback speed while holding mouse button
    public float maxForce = 500f;         // max force on ball

    public float doubleClickTime = 0.3f;  // max time between clicks for double click
    public float colliderDisableTime = 0.5f;

    private Vector3 startPos;
    private float currentPull = 0f;

    private int clickCount = 0;
    private float lastClickTime = 0f;
    private bool isHoldingAfterDoubleClick = false;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) 
        {
            clickCount++;
            if (clickCount == 1)
            {
                lastClickTime = Time.time;
            }
            else if (clickCount == 2)
            {
                if (Time.time - lastClickTime <= doubleClickTime)
                {
                    // detects double click
                    isHoldingAfterDoubleClick = true;
                }
                clickCount = 0; // reset click count after double-click
            }
        }
        if (clickCount == 1 && Time.time - lastClickTime > doubleClickTime)
        {
            clickCount = 0;
        }

        // increase pull while holding mouse
        if (isHoldingAfterDoubleClick && Input.GetMouseButton(0))
        {
            currentPull += pullPerSecond * Time.deltaTime;
            currentPull = Mathf.Min(currentPull, maxPull);
            transform.position = startPos - Vector3.forward * currentPull;
        }

        // fire on mouse release
        if (isHoldingAfterDoubleClick && Input.GetMouseButtonUp(0))
        {
            Fire();
            isHoldingAfterDoubleClick = false;
        }
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

        StartCoroutine(ReEnableCollider());
    }

    IEnumerator ReEnableCollider()
    {
        yield return new WaitForSeconds(colliderDisableTime);
        Collider handleCollider = GetComponent<Collider>();
        if (handleCollider != null)
        {
            handleCollider.enabled = true;
        }
    }
}
