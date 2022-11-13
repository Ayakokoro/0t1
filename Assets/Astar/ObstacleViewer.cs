using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleViewer : MonoBehaviour
{
    public GameObject test;//测试检测效果的预制体
    public int gridID;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            MapManager.instance.UpdateObstacleInfo(gridID);
            Instantiate(test, transform.position, Quaternion.identity);
        }
    }

}
