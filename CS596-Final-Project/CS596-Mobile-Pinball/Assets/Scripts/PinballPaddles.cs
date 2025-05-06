using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PaddleController : MonoBehaviour
{
    [Header("Paddles")]
    public HingeJoint leftPaddle;
    public HingeJoint rightPaddle;

    [Header("Paddle Settings")]
    public float paddleForce = 1000f;
    public float paddleAngle = 45f;
    public float damper = 1f;

    [Header("UI Buttons")]
    public Button leftButton;
    public Button rightButton;

    private JointSpring leftSpring;
    private JointSpring rightSpring;

    void Start()
    {
        if (!leftPaddle || !rightPaddle || !leftButton || !rightButton)
        {
            Debug.LogError("Missing paddle or button references in Inspector!");
            return;
        }

        // Set up LEFT paddle spring
        leftSpring = leftPaddle.spring;
        leftSpring.spring = paddleForce;
        leftSpring.damper = damper;
        leftSpring.targetPosition = 0f;
        leftPaddle.spring = leftSpring;
        leftPaddle.useSpring = true;

        // Set up RIGHT paddle spring
        rightSpring = rightPaddle.spring;
        rightSpring.spring = paddleForce;
        rightSpring.damper = damper;
        rightSpring.targetPosition = 0f;
        rightPaddle.spring = rightSpring;
        rightPaddle.useSpring = true;

        // Optional: set rotation limits to prevent over-rotation
        JointLimits limits = new JointLimits { min = -60f, max = 60f };
        leftPaddle.limits = limits;
        rightPaddle.limits = limits;
        leftPaddle.useLimits = true;
        rightPaddle.useLimits = true;

        // Add UI triggers
        AddEventTrigger(leftButton, EventTriggerType.PointerDown, LeftPaddleDown);
        AddEventTrigger(leftButton, EventTriggerType.PointerUp, LeftPaddleUp);
        AddEventTrigger(rightButton, EventTriggerType.PointerDown, RightPaddleDown);
        AddEventTrigger(rightButton, EventTriggerType.PointerUp, RightPaddleUp);
    }

    private void AddEventTrigger(Button button, EventTriggerType type, UnityEngine.Events.UnityAction action)
    {
        EventTrigger trigger = button.GetComponent<EventTrigger>();
        if (!trigger)
            trigger = button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener((_) => action());
        trigger.triggers.Add(entry);
    }

    public void LeftPaddleDown()
    {
        leftSpring.targetPosition = -paddleAngle;
        leftPaddle.spring = leftSpring;
    }

    public void LeftPaddleUp()
    {
        leftSpring.targetPosition = 0f;
        leftPaddle.spring = leftSpring;
    }

    public void RightPaddleDown()
    {
        rightSpring.targetPosition = paddleAngle;
        rightPaddle.spring = rightSpring;
    }

    public void RightPaddleUp()
    {
        rightSpring.targetPosition = 0f;
        rightPaddle.spring = rightSpring;
    }
}
