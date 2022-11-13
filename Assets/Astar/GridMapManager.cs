using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMapManager : MonoBehaviour
{
    public static GridMapManager instance;
    //暂时只考虑一个地图,以后地图多了用list存储
    public MapData mapData;
    public bool[,] isObstacles;
    private List<Tilemap> ObstacleTilemaps;
    private void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<GridMapManager>();
        }
        //创建地图大小的障碍判断库
        isObstacles = new bool[mapData.gridWidth, mapData.gridHeight];
        ObstacleTilemaps = new List<Tilemap>();

        for (int i = 0; i < this.transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.CompareTag("Obstacles"))
            {
                ObstacleTilemaps.Add(transform.GetChild(i).GetComponent<Tilemap>());
            }
        }
        UpdateObstacleInfo();
    }
    
    public void GetGridDimensions(out Vector2Int gridDimensions, out Vector2Int gridOrigin)
    {
        gridDimensions = Vector2Int.zero;
        gridOrigin = Vector2Int.zero;

        gridDimensions.x = mapData.gridWidth;
        gridDimensions.y = mapData.gridHeight;

        gridOrigin.x = mapData.originX;
        gridOrigin.y = mapData.originY;
    }
    //更新地图障碍信息，当地图改变的时候要调取一下
    private void UpdateObstacleInfo()
    {
        for(int x = 0; x < mapData.gridWidth; x++)
        {
            for(int y = 0; y < mapData.gridHeight; y++)
            {
                for(int t = 0; t < ObstacleTilemaps.Count; t++)
                {
                    if (ObstacleTilemaps[t].HasTile(new Vector3Int(mapData.originX + x, mapData.originY + y, 0)))
                    {
                        isObstacles[x, y] = true;
                        //print((mapData.originX + x).ToString() + " " +(mapData.originY + y).ToString());
                        break;
                    }
                }
            }
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
