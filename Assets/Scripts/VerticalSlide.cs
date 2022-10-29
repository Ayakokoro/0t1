using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VerticalSlide : MonoBehaviour
{
    public RectTransform background;
    public RectTransform stickPos;
    public float offset;
    public float maxScale;
    public float minScale;
    private float nowScale;
    public Camera cam;
    private bool isEnabled;
    private float dir;

    private void Start()
    {
        nowScale = cam.orthographicSize;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isEnabled = RectTransformUtility.RectangleContainsScreenPoint(stickPos, Input.mousePosition);
        }

        if (Input.GetMouseButton(0) && isEnabled)
        {
            dir = Mathf.Clamp((Input.mousePosition.x - background.position.x), -offset, offset);
            float distance = Mathf.Abs(dir);
            stickPos.position = new Vector3(background.position.x + dir, stickPos.position.y, stickPos.position.z);

            if (dir < 0)
            {
                cam.orthographicSize = Mathf.Lerp(nowScale, maxScale, distance/offset);
            }
            else
            {
                cam.orthographicSize = Mathf.Lerp(nowScale, minScale, distance/offset);
            }
        }
    }
}
