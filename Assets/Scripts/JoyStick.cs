using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour
{
    public RectTransform backGround;
    public RectTransform stickPos;
    public RectTransform range;
    public float speed;
    public PlayerController mainCharacter;

    protected float radius = 0f;
    protected bool isEnabled = false;
    protected Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        radius = range.rect.width / 2;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isEnabled = RectTransformUtility.RectangleContainsScreenPoint(backGround, Input.mousePosition);
        }

        if (Input.GetMouseButton(0) && isEnabled) 
        {
            dir = (Input.mousePosition - backGround.position);
            float distance = Mathf.Clamp(dir.magnitude, 0f, radius);
            dir = dir.normalized;

            stickPos.localPosition = dir * distance;
        }
        else
        {
            stickPos.localPosition = Vector3.Lerp(stickPos.localPosition, Vector3.zero, speed * Time.deltaTime);
            isEnabled = false;
        }
    }
}
