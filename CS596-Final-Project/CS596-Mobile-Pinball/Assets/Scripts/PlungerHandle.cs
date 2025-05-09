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
    public float colliderDisableTime = 0.5f;

    [Header("Input Settings")]
    public float doubleClickTime = 0.3f;

    private Vector3 startPos;
    private float currentPull = 0f;
    private int clickCount = 0;
    private float lastClickTime = 0f;
    private bool isHoldingAfterDoubleClick = false;
    private bool hasFiredCurrentBall = false;

    private Vector2 swipeStart;
    private bool isSwiping = false;

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
            hasFiredCurrentBall = false;
        }

        HandleInput();
        HandleSwipeInput();
        UpdatePull();
        CheckRelease();
    }

    void HandleInput()
    {
        if (ballsLeft <= 0) return;

        if (Input.GetMouseButtonDown(0))
        {
            clickCount++;
            if (clickCount == 1)
            {
                lastClickTime = Time.time;
            }
            else if (clickCount == 2 && Time.time - lastClickTime <= doubleClickTime)
            {
                isHoldingAfterDoubleClick = true;
                clickCount = 0;
            }
        }

        if (clickCount == 1 && Time.time - lastClickTime > doubleClickTime)
        {
            clickCount = 0;
        }
    }

    void HandleSwipeInput()
    {
        if (ballsLeft <= 0) return;

        // Mobile touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 pos = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                swipeStart = pos;
                isSwiping = true;
            }
            else if (touch.phase == TouchPhase.Moved && isSwiping)
            {
                float drag = (swipeStart.y - pos.y) / Screen.height;
                currentPull = Mathf.Clamp(drag * maxPull * 2f, 0f, maxPull);
                transform.position = startPos - Vector3.forward * currentPull;
            }
            else if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && isSwiping)
            {
                if (currentPull > 0.1f) Fire();
                isSwiping = false;
                currentPull = 0f;
            }
        }
        // Mouse drag (editor support)
        else if (Input.GetMouseButtonDown(0))
        {
            swipeStart = Input.mousePosition;
            isSwiping = true;
        }
        else if (Input.GetMouseButton(0) && isSwiping)
        {
            float drag = (swipeStart.y - Input.mousePosition.y) / Screen.height;
            currentPull = Mathf.Clamp(drag * maxPull * 2f, 0f, maxPull);
            transform.position = startPos - Vector3.forward * currentPull;
        }
        else if (Input.GetMouseButtonUp(0) && isSwiping)
        {
            if (currentPull > 0.1f) Fire();
            isSwiping = false;
            currentPull = 0f;
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

        ballsLeft--;
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
            ball = balls[balls.Length - 1];
    }

    void UpdateBallsUI()
    {
        if (ballsLeftText != null)
            ballsLeftText.text = "BALLS " + ballsLeft;
    }
}

