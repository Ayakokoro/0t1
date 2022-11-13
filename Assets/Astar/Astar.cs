using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    private GridNodes gridNodes;

    private Node startNode;
    private Node targetNode;

    private int gridWidth;
    private int gridHeight;

    private List<Node> openNodeList;    //当前选中Node周围的8个点,开放表
    private HashSet<Node> closedNodeList;   //所有被选中的点，关闭表

    private void Start()
    {
        gridNodes = GetComponent<FSM>().parameter.nodes;
    }
    public GridNodes BiuldPath(Vector2Int startPos, Vector2Int endPos)
    {
        GenerateGridNodes(startPos, endPos);

        //查找最短路径
        return FindShortestPath();
    }

    /// <summary>
    /// 构建网格节点信息，初始化两个列表
    /// </summary>
    /// <param name="startPos">起点</param>
    /// <param name="endPos">终点</param>
    /// <returns></returns>
    private void GenerateGridNodes(Vector2Int startPos, Vector2Int endPos)
    {
        MapManager.instance.GetGridDimensions(out Vector2Int gridDimensions);
        //根据瓦片地图范围构建网格移动节点范围数组
        gridNodes = new GridNodes(gridDimensions.x, gridDimensions.y);
        gridWidth = gridDimensions.x;
        gridHeight = gridDimensions.y;

        openNodeList = new List<Node>();

        closedNodeList = new HashSet<Node>();

        //gridNodes的范围是从0,0开始所以需要减去原点坐标得到实际位置
        startNode = gridNodes.GetGridNode(startPos.x, startPos.y);
        targetNode = gridNodes.GetGridNode(endPos.x, endPos.y);

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (MapManager.instance.isObstacles[x, y])
                {
                    gridNodes.SetObstacle(x, y);
                }
            }
        }
    }

    public GridNodes FindShortestPath()
    {
        openNodeList.Add(startNode);
        Node currentNode;
        while(openNodeList.Count > 0)
        {
            currentNode = FindOpenFLeastNode();
            openNodeList.Remove(currentNode);
            closedNodeList.Add(currentNode);
            EvaluateNeighbourNodes(currentNode);
            if(closedNodeList.Contains(targetNode))
            {
                return gridNodes;
            }
        }
        return null;
    }

    /// <summary>
    /// 评估周围8个点，并生成对应消耗值
    /// </summary>
    /// <param name="currentNode"></param>
    private void EvaluateNeighbourNodes(Node currentNode)
    {
        Vector2Int currentNodePos = currentNode.gridPosition;
        Node validNeighbourNode;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                validNeighbourNode = GetValidNeighbourNode(currentNodePos.x + x, currentNodePos.y + y);

                if (validNeighbourNode != null)
                {
                    if (!openNodeList.Contains(validNeighbourNode))
                    {
                        validNeighbourNode.gCost = currentNode.gCost + GetDistance(currentNode, validNeighbourNode);
                        validNeighbourNode.hCost = GetDistance(validNeighbourNode, targetNode);
                        //链接父节点
                        validNeighbourNode.parentNode = currentNode;
                        openNodeList.Add(validNeighbourNode);
                    }
                }
            }
        }
    }


    /// <summary>
    /// 找到有效的Node,非障碍，非已选择
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private Node GetValidNeighbourNode(int x, int y)
    {
        if (x >= gridWidth || y >= gridHeight || x < 0 || y < 0)
            return null;

        Node neighbourNode = gridNodes.GetGridNode(x, y);

        if (neighbourNode.isObstacle || closedNodeList.Contains(neighbourNode))
            return null;
        else
            return neighbourNode;
    }


    /// <summary>
    /// 返回两点距离值
    /// </summary>
    /// <param name="nodeA"></param>
    /// <param name="nodeB"></param>
    /// <returns>14的倍数+10的倍数</returns>
    private int GetDistance(Node nodeA, Node nodeB)
    {
        int xDistance = Mathf.Abs(nodeA.gridPosition.x - nodeB.gridPosition.x);
        int yDistance = Mathf.Abs(nodeA.gridPosition.y - nodeB.gridPosition.y);

        if (xDistance > yDistance)
        {
            return 14 * yDistance + 10 * (xDistance - yDistance);
        }
        return 14 * xDistance + 10 * (yDistance - xDistance);
    }
    /// <summary>
    /// 找到开放列表中F最小的那一项，
    /// </summary>
    /// <returns></returns>
    private Node FindOpenFLeastNode()
    {
        Node node = openNodeList[0];
        for(int i = 1; i< openNodeList.Count; i++)
        {
            if(openNodeList[i].FCost < node.FCost)
            {
                node = openNodeList[i];
            }
        }
        return node;
    }
}