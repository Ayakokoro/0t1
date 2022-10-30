using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoSingleton<BulletPool>
{
    #region ·ÀÓùËþ×Óµ¯
    [Header("·ÀÓùËþ")]
    public GameObject turretBullet;
    private Stack<GameObject> turretBulletsPool;
    private void NewturretBullet()
    {
        GameObject now = Instantiate(turretBullet);
        now.transform.parent = transform;
        now.SetActive(false);
        turretBulletsPool.Push(now);
    }

    public GameObject GetTurretBullet(Vector3 position, Quaternion quaternion)
    {
        if (turretBulletsPool.Count == 0) NewturretBullet();
        GameObject now = turretBulletsPool.Pop();
        now.transform.position = position;
        now.transform.rotation = quaternion;
        now.SetActive(true);
        return now;
    }
    public void RecycleTurretBullet(GameObject Bullet)
    {
        Bullet.SetActive(false);
        turretBulletsPool.Push(Bullet);
    }
    #endregion
}
