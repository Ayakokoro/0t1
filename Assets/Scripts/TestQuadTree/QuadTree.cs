using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuadTree : MonoBehaviour
{
    public GameObject rect;
    public Bounds objBound;

    public Bounds quadBound;
    public Node root;
    private Node tree;

    public int max_depth;
    private void Start()
    {
        root = new Node(quadBound, 0);
        objBound = rect.GetComponent<Collider>().bounds;
        GenQuadTree(root);
    }

    private void GenQuadTree(Node node)
    {
        if (node.bound.Intersects(objBound))
        {
            node.GenChildren(max_depth);
            for (int i=0;i<4;i++)
            {
                if (node.childNode[i] == null) continue;
                if (node.childNode[i].bound.Intersects(objBound))
                    GenQuadTree(node.childNode[i]);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (root !=null)
        {
            root.DrawBound();
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(quadBound.center, quadBound.size);
        }
    }
}
