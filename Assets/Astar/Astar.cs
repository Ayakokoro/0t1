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

    private List<Node> openNodeList;    //��ǰѡ��Node��Χ��8����,���ű�
    private HashSet<Node> closedNodeList;   //���б�ѡ�еĵ㣬�رձ�

    private void Start()
    {
        gridNodes = GetComponent<FSM>().parameter.nodes;
    }
    public GridNodes BiuldPath(Vector2Int startPos, Vector2Int endPos)
    {
        GenerateGridNodes(startPos, endPos);

        //�������·��
        return FindShortestPath();
    }

    /// <summary>
    /// ��������ڵ���Ϣ����ʼ�������б�
    /// </summary>
    /// <param name="startPos">���</param>
    /// <param name="endPos">�յ�</param>
    /// <returns></returns>
    private void GenerateGridNodes(Vector2Int startPos, Vector2Int endPos)
    {
        MapManager.instance.GetGridDimensions(out Vector2Int gridDimensions);
        //������Ƭ��ͼ��Χ���������ƶ��ڵ㷶Χ����
        gridNodes = new GridNodes(gridDimensions.x, gridDimensions.y);
        gridWidth = gridDimensions.x;
        gridHeight = gridDimensions.y;

        openNodeList = new List<Node>();

        closedNodeList = new HashSet<Node>();

        //gridNodes�ķ�Χ�Ǵ�0,0��ʼ������Ҫ��ȥԭ������õ�ʵ��λ��
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
    /// ������Χ8���㣬�����ɶ�Ӧ����ֵ
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
                        //���Ӹ��ڵ�
                        validNeighbourNode.parentNode = currentNode;
                        openNodeList.Add(validNeighbourNode);
                    }
                }
            }
        }
    }


    /// <summary>
    /// �ҵ���Ч��Node,���ϰ�������ѡ��
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
    /// �����������ֵ
    /// </summary>
    /// <param name="nodeA"></param>
    /// <param name="nodeB"></param>
    /// <returns>14�ı���+10�ı���</returns>
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
    /// �ҵ������б���F��С����һ�
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