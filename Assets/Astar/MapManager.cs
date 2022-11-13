using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    //暂时只考虑一个地图,以后地图多了用list存储
    public MapData mapData;

    public bool[,] isObstacles;
    public GameObject obstaleViewer;//用于地图搜寻的预制件
    private ObstacleViewer[] obstacleViewers;//所有的观测点
    public float viewerSize;//观察者的大小
    private int viewerCount;//观察者数量
    private IEnumerator doView;
    private GameObject viewerParent;//便于观察者移动
    public int currentRow;

    private void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<MapManager>();
        }
        //创建地图大小的障碍判断库
        currentRow = 0;
        viewerParent = transform.GetChild(0).gameObject;
        InitObstacleViewer();
        isObstacles = new bool[viewerCount,viewerCount];
        doView = Viewer();
        StartCoroutine(doView);

    }

    private void InitObstacleViewer()
    {
        viewerCount = (int)(mapData.gridWidth / viewerSize);
        for(int i = 0; i < viewerCount; i++)
        {
            GameObject gameObject = Instantiate(obstaleViewer, new Vector3(mapData.originX + i * viewerSize, mapData.originY, 0), Quaternion.identity);
            gameObject.transform.localScale *= viewerSize;
            gameObject.transform.SetParent(viewerParent.transform);
            gameObject.transform.GetComponent<ObstacleViewer>().gridID = i;
            //obstacleViewers[i] = gameObject.GetComponent<ObstacleViewer>();
        }
    }

    public void GetGridDimensions(out Vector2Int gridDimensions)
    {
        gridDimensions = Vector2Int.zero;

        gridDimensions.x = viewerCount;
        gridDimensions.y = viewerCount;

    }
    //更改对应行列的
    public void UpdateObstacleInfo(int line)
    {
        if(currentRow < viewerCount && isObstacles[line, currentRow] == false)
        {
            print(line.ToString() + "," + currentRow.ToString());
            
            isObstacles[line, currentRow] = true;
        }
    }
    //更新地图障碍信息，当地图改变的时候要调取一下(策划那边好像不用调)
    /*   private void UpdateObstacleInfo()
       {
           for(int i = 0; i < viewerCount; i++)
           {
               viewerParent.transform.Translate(new Vector3(0, viewerSize, 0));
           }
           *//*for (int x = 0; x < mapData.gridWidth; x++)
           {
               for (int y = 0; y < mapData.gridHeight; y++)
               {
                   for (int t = 0; t < ObstacleTilemaps.Count; t++)
                   {
                       if (ObstacleTilemaps[t].HasTile(new Vector3Int(mapData.originX + x, mapData.originY + y, 0)))
                       {
                           isObstacles[x, y] = true;
                           //print((mapData.originX + x).ToString() + " " +(mapData.originY + y).ToString());
                           break;
                       }
                   }
               }
           }*//*

       }*/
    private IEnumerator Viewer()
    {
        while(currentRow < viewerCount)
        {
            yield return new WaitForSeconds(0.03f);
            viewerParent.transform.Translate(new Vector3(0, viewerSize, 0));
            currentRow++;
        }
    }

    //根据坐标算对应的格子坐标的函数
    public Vector2Int UseWolrdPosCalcGridPos(Vector3 worldPos)
    {
        return new Vector2Int((int)worldPos.x - 1 - mapData.originX, (int)worldPos.y - 1 - mapData.originY);
    }
    //根据格子坐标算对应的坐标
    public Vector3 UseGridCalcPosWolrdPos(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x + mapData.originX + 0.5f, gridPos.y + mapData.originY + 0.5f, 1);
    }

}
