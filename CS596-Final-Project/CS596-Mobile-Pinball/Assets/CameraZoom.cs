using UnityEngine;

public class PinchZoomPan : MonoBehaviour
{
    public Camera cam;
    public float zoomSpeed = 0.1f;
    public float panSpeed = 0.005f;
    public float minZoom = 30f;
    public float maxZoom = 90f;

    private bool isOrthographic;
    private Vector2 lastMidpoint;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;

        isOrthographic = cam.orthographic;
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            // ---- Pinch Zoom ----
            Vector2 prevTouch0 = touch0.position - touch0.deltaPosition;
            Vector2 prevTouch1 = touch1.position - touch1.deltaPosition;

            float prevMagnitude = (prevTouch0 - prevTouch1).magnitude;
            float currentMagnitude = (touch0.position - touch1.position).magnitude;

            float deltaMagnitude = prevMagnitude - currentMagnitude;

            if (isOrthographic)
            {
                cam.orthographicSize += deltaMagnitude * zoomSpeed;
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
            }
            else
            {
                cam.fieldOfView += deltaMagnitude * zoomSpeed;
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minZoom, maxZoom);
            }

            // ---- Two-Finger Pan ----
            Vector2 currentMidpoint = (touch0.position + touch1.position) * 0.5f;
            Vector2 deltaMidpoint = currentMidpoint - lastMidpoint;

            // Convert screen delta to world delta
            if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                Vector3 panDirection = -cam.transform.right * deltaMidpoint.x * panSpeed
                                     - cam.transform.up * deltaMidpoint.y * panSpeed;

                cam.transform.position += panDirection;
            }

            lastMidpoint = currentMidpoint;
        }
    }
}

