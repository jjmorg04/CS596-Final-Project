using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PaddleController : MonoBehaviour
{
    public HingeJoint leftPaddle;
    public HingeJoint rightPaddle;

    public float paddleForce = 1000f;

    private JointSpring leftSpring;
    private JointSpring rightSpring;

    public Button leftButton;
    public Button rightButton;

    void Start()
    {

        leftSpring = leftPaddle.spring;
        rightSpring = rightPaddle.spring;

        leftSpring.spring = paddleForce;
        leftSpring.damper = 1f;

        rightSpring.spring = paddleForce;
        rightSpring.damper = 1f;

        if (!leftPaddle || !rightPaddle || !leftButton || !rightButton)
{
    Debug.LogError("Missing paddle or button references in Inspector!");
}


        // Attach button event triggers
        AddEventTrigger(leftButton, EventTriggerType.PointerDown, LeftPaddleDown);
        AddEventTrigger(leftButton, EventTriggerType.PointerUp, LeftPaddleUp);

        AddEventTrigger(rightButton, EventTriggerType.PointerDown, RightPaddleDown);
        AddEventTrigger(rightButton, EventTriggerType.PointerUp, RightPaddleUp);
    }

    void AddEventTrigger(Button button, EventTriggerType eventType, UnityEngine.Events.UnityAction action)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener((eventData) => action());
        trigger.triggers.Add(entry);
    }

    public void LeftPaddleDown()
    {
        leftSpring.targetPosition = -45f;
        leftPaddle.spring = leftSpring;
    }

    public void LeftPaddleUp()
    {
        leftSpring.targetPosition = 0f;
        leftPaddle.spring = leftSpring;
    }

    public void RightPaddleDown()
    {
        rightSpring.targetPosition = 45f;
        rightPaddle.spring = rightSpring;
    }

    public void RightPaddleUp()
    {
        rightSpring.targetPosition = 0f;
        rightPaddle.spring = rightSpring;
    }
}
