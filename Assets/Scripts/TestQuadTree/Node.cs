using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Node
{
    public Node[] childNode = new Node[4];
    public Bounds bound;
    private int depth;
    private Node belongNode;
    private List<GameObject> containers;
    public Node(Bounds bound, int depth, Node belong = null)
    {
        this.bound = bound;
        this.depth = depth;
        this.belongNode = belong;
    }

    public void GenChildren(int max_depth)
    {
        if (depth > max_depth) return;
        int index = 0;
        for (int x = -1; x <= 1; x += 2)
        {
            for (int y = -1; y <= 1; y += 2)
            {
                Vector3 centerOffset = new Vector3(bound.size.x / 4 * x, bound.size.y / 4 * y, 0);
                Vector3 cSize = new Vector3(bound.size.x / 2, bound.size.y / 2, bound.size.z);
                Bounds cBound = new Bounds(bound.center + centerOffset, cSize);
                childNode[index++] = new Node(cBound, depth + 1, this);
            }
        }
    }

    public void DrawBound()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(bound.center, bound.size);

        if (childNode != null)
        {
            for (int i = 0; i < childNode.Length; ++i)
            {
                if (childNode[i] != null) childNode[i].DrawBound();
            }
        }
    }
}
