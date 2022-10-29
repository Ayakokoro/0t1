using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bullet;
    private Stack<GameObject> bulletPool = new Stack<GameObject>();


    private void GenBullets()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.parent = transform;
            newBullet.SetActive(false);
            bulletPool.Push(newBullet);
        }
    }

    public GameObject GetBullets(Vector3 pos,Quaternion rot)
    {
        if (bulletPool.Count == 0) GenBullets();
        GameObject newBullet = bulletPool.Pop() as GameObject;
        newBullet.transform.position = pos;
        newBullet.transform.rotation = rot;
        newBullet.SetActive(true);
        return newBullet;
    }

    public void Recycle(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Push(bullet);
    }

    private void Start()
    {
        GenBullets();
    }
}
