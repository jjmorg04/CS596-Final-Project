using UnityEngine;
using TMPro;
using System.Collections;

public class PlungerHandle : MonoBehaviour
{
    [Header("Ball Control")]
    public GameObject ball;
    public int ballsLeft = 3;
    public TMP_Text ballsLeftText;

    [Header("Launch Settings")]
    public float maxPull = 1.0f;
    public float pullPerSecond = 0.5f;
    public float maxForce = 500f;

    [Header("Input Settings")]
    public float doubleClickTime = 0.3f;
    public float colliderDisableTime = 0.5f;

    private Vector3 startPos;
    private float currentPull = 0f;

    private int clickCount = 0;
    private float lastClickTime = 0f;
    private bool isHoldingAfterDoubleClick = false;
    private bool hasFiredCurrentBall = false;

    void Start()
    {
        startPos = transform.position;

        if (ball == null)
            FindLatestBall();

        UpdateBallsUI();
    }

    void Update()
    {
        if (ball == null)
        {
            FindLatestBall();
            hasFiredCurrentBall = false; // allow launch on next detection
        }

        HandleInput();
        UpdatePull();
        CheckRelease();
    }

    void HandleInput()
    {
        if (ballsLeft <= 0) return; // No balls? Disable input

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
                    isHoldingAfterDoubleClick = true;
                }
                clickCount = 0;
            }
        }

        if (clickCount == 1 && Time.time - lastClickTime > doubleClickTime)
        {
            clickCount = 0;
        }
    }

    void UpdatePull()
    {
        if (isHoldingAfterDoubleClick && Input.GetMouseButton(0))
        {
            currentPull += pullPerSecond * Time.deltaTime;
            currentPull = Mathf.Min(currentPull, maxPull);
            transform.position = startPos - Vector3.forward * currentPull;
        }
    }

    void CheckRelease()
    {
        if (isHoldingAfterDoubleClick && Input.GetMouseButtonUp(0))
        {
            Fire();
            isHoldingAfterDoubleClick = false;
        }
    }

    void Fire()
    {
        if (hasFiredCurrentBall || ball == null) return;

        Collider handleCollider = GetComponent<Collider>();
        if (handleCollider != null)
            handleCollider.enabled = false;

        float force = (currentPull / maxPull) * maxForce;

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce(Vector3.forward * force, ForceMode.Impulse);

        transform.position = startPos;
        currentPull = 0f;
        hasFiredCurrentBall = true;

        ballsLeft--; // ✅ deduct ball
        UpdateBallsUI();

        StartCoroutine(ReEnableCollider());
    }

    IEnumerator ReEnableCollider()
    {
        yield return new WaitForSeconds(colliderDisableTime);
        Collider handleCollider = GetComponent<Collider>();
        if (handleCollider != null)
            handleCollider.enabled = true;
    }

    void FindLatestBall()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        if (balls.Length > 0)
        {
            ball = balls[balls.Length - 1];
        }
    }

    void UpdateBallsUI()
    {
        if (ballsLeftText != null)
            ballsLeftText.text = "BALLS " + ballsLeft;
    }
}
