using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName ="Map/MapData")]
public class MapData : ScriptableObject
{
    [Header("��ͼ��Ϣ")]
    public int gridWidth;
    public int gridHeight;

    [Header("���½�ԭ��")]
    public int originX;
    public int originY;
}
