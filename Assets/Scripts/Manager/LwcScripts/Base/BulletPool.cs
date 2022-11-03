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
    #region �ӵ�
    /// <summary>
    /// �ӵ������߶�Ӧ�ӵ�
    /// </summary>
    [Header("�ӵ�")]
    public GameObject turretBullet;
    #endregion
    /// <summary>
    /// �½�һ���ӵ���������
    /// </summary>
    /// <param name="kind">�ӵ�����</param>
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
    /// �Ӷ�����л�ȡһ���ӵ�
    /// </summary>
    /// <param name="kind">�ӵ�����</param>
    /// <param name="position">��������</param>
    /// <param name="quaternion">����Ƕ�</param>
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
    /// �����ӵ�
    /// </summary>
    /// <param name="bullet">�ӵ�ʵ��</param>
    public void RecycleBullet(GameObject bullet)
    {
        Debug.Log((int)now.GetComponent<BaseBullet>().kind);
        Debug.Log(bulletsPool[(int)now.GetComponent<BaseBullet>().kind]);
        bulletsPool[(int)now.GetComponent<BaseBullet>().kind].Push(bullet);
        bullet.SetActive(false);
    }
}
