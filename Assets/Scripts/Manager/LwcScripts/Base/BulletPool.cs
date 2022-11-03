using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoSingleton<BulletPool>
{
    private Stack<GameObject>[] bulletsPool;
    private GameObject now;
    private void Start()
    {
        bulletsPool = new Stack<GameObject>[System.Enum.GetNames((new BulletKind()).GetType()).Length];
        for (int i = 0; i < bulletsPool.Length; i++) bulletsPool[i] = new Stack<GameObject>();
    }
    #region 子弹
    /// <summary>
    /// 子弹发出者对应子弹
    /// </summary>
    [Header("子弹")]
    public GameObject turretBullet;
    #endregion
    /// <summary>
    /// 新建一个子弹进入对象池
    /// </summary>
    /// <param name="kind">子弹种类</param>
    private void NewBullet(BulletKind kind)
    {
        now = Instantiate(turretBullet);
        now.GetComponent<BaseBullet>().kind = kind;
        //now.GetComponent<BaseBullet>().Binding(Instance);
        now.transform.parent = transform;
        now.SetActive(false);
        bulletsPool[(int)kind].Push(now);
    }
    /// <summary>
    /// 从对象池中获取一个子弹
    /// </summary>
    /// <param name="kind">子弹种类</param>
    /// <param name="position">发射坐标</param>
    /// <param name="quaternion">发射角度</param>
    /// <returns></returns>
    public GameObject GetBullet(BulletKind kind, Vector3 position, Quaternion quaternion)
    {
        Debug.Log((int)kind);
        Debug.Log(bulletsPool);
        if (bulletsPool[(int)kind].Count == 0) NewBullet(kind);
        GameObject now = bulletsPool[(int)kind].Pop();
        now.transform.position = position;
        now.transform.rotation = quaternion;
        now.SetActive(true);
        return now;
    }
    /// <summary>
    /// 回收子弹
    /// </summary>
    /// <param name="bullet">子弹实例</param>
    public void RecycleBullet(GameObject bullet)
    {
        Debug.Log((int)now.GetComponent<BaseBullet>().kind);
        Debug.Log(bulletsPool[(int)now.GetComponent<BaseBullet>().kind]);
        bulletsPool[(int)now.GetComponent<BaseBullet>().kind].Push(bullet);
        bullet.SetActive(false);
    }
}
