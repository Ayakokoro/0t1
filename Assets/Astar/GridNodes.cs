using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNodes
{
    private int width;
    private int height;
    private Node[,] gridNode;

    /// <summary>
    /// ���캯����ʼ���ڵ㷶Χ����
    /// </summary>
    /// <param name="width">��ͼ���</param>
    /// <param name="height">��ͼ�߶�</param>
    public GridNodes(int width, int height)
    {
        this.width = width;
        this.height = height;

        gridNode = new Node[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridNode[x, y] = new Node(new Vector2Int(x, y));
            }
        }
    }


    public Node GetGridNode(int xPos, int yPos)
    {
        if (xPos < width && yPos < height)
        {
            return gridNode[xPos, yPos];
        }
        Debug.Log("��������Χ");
        return null;
    }
    
    public void SetObstacle(int xPos, int yPos)
    {
        if (xPos < width && yPos < height)
        {
            gridNode[xPos, yPos].isObstacle = true;
        }
    }
}