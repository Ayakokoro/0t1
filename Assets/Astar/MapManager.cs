using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    //��ʱֻ����һ����ͼ,�Ժ��ͼ������list�洢
    public MapData mapData;

    public bool[,] isObstacles;
    public GameObject obstaleViewer;//���ڵ�ͼ��Ѱ��Ԥ�Ƽ�
    private ObstacleViewer[] obstacleViewers;//���еĹ۲��
    public float viewerSize;//�۲��ߵĴ�С
    private int viewerCount;//�۲�������
    private IEnumerator doView;
    private GameObject viewerParent;//���ڹ۲����ƶ�
    public int currentRow;

    private void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<MapManager>();
        }
        //������ͼ��С���ϰ��жϿ�
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
    //���Ķ�Ӧ���е�
    public void UpdateObstacleInfo(int line)
    {
        if(currentRow < viewerCount && isObstacles[line, currentRow] == false)
        {
            print(line.ToString() + "," + currentRow.ToString());
            
            isObstacles[line, currentRow] = true;
        }
    }
    //���µ�ͼ�ϰ���Ϣ������ͼ�ı��ʱ��Ҫ��ȡһ��(�߻��Ǳߺ����õ�)
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

    //�����������Ӧ�ĸ�������ĺ���
    public Vector2Int UseWolrdPosCalcGridPos(Vector3 worldPos)
    {
        return new Vector2Int((int)worldPos.x - 1 - mapData.originX, (int)worldPos.y - 1 - mapData.originY);
    }
    //���ݸ����������Ӧ������
    public Vector3 UseGridCalcPosWolrdPos(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x + mapData.originX + 0.5f, gridPos.y + mapData.originY + 0.5f, 1);
    }

}
