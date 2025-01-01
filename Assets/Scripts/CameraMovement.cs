using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Camera cam;
    private Vector3 dragOrigin;
    public Slider zoomSlider;
    private float initialPinchDistance;
    private float initialCameraSize;
    public bool isZooming = false;
    public bool isPanning = false;

    void Start()
    {
        zoomSlider.onValueChanged.AddListener(OnZoomSliderValueChanged);
    }

    // Update is called once per frame
    void Update()
    {
        PanCamera();
        ZoomCamera();
    }

    private void PanCamera()
    {
        if (Input.touchCount == 1 && isPanning)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                dragOrigin = cam.ScreenToWorldPoint(touch.position);
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(touch.position);
                cam.transform.position += difference;
            }
        }
    }

    private void OnZoomSliderValueChanged(float value)
    {
        cam.orthographicSize = Mathf.Lerp(2f, 10f, value);
    }

    private void ZoomCamera()
    {
        if (Input.touchCount == 2 && isZooming)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                initialPinchDistance = Vector2.Distance(touch1.position, touch2.position);
                initialCameraSize = cam.orthographicSize;
            }
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                float currentPinchDistance = Vector2.Distance(touch1.position, touch2.position);
                float pinchRatio = initialPinchDistance / currentPinchDistance;
                cam.orthographicSize = Mathf.Clamp(initialCameraSize * pinchRatio, 2f, 10f); // Adjust the min and max values as needed
            }
        }
    }
}
