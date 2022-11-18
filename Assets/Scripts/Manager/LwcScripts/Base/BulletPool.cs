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
    [SerializeField] private GameObject Bullet;
    #endregion
    /// <summary>
    /// �½�һ���ӵ���������
    /// </summary>
    /// <param name="kind">�ӵ�����</param>
    private void NewBullet(BulletKind kind)
    {
        now = Instantiate(Bullet);
        now.GetComponent<BaseBullet>().SetBullet(kind);
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
    public void RecycleBullet(GameObject bullet, BulletKind kind)
    {
        Debug.Log((int)kind);
        Debug.Log(bulletsPool[(int)kind]);
        bulletsPool[(int)kind].Push(bullet);
        bullet.SetActive(false);
    }
}
