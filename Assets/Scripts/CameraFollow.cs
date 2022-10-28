using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera Camera;
    public GameObject follower;
    private void Update()
    {
        Camera.transform.position = new Vector3(follower.transform.position.x, follower.transform.position.y, Camera.transform.position.z);
    }
}
