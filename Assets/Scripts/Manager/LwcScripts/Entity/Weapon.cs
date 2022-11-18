using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    private GameObject father;
    [Header("武器参数")]
    private float last;
    [SerializeField] private float CD;
    [SerializeField] private float Radius;
    [Header("子弹参数")]
    [SerializeField] private BulletKind bulletKind;
    [SerializeField] private bool bulletIsChaser;
    [SerializeField] private int bulletAtk;
    [SerializeField] private int bulletSpeed;
    private void Start()
    {
        GetComponent<SphereCollider>().radius = Radius;
        father = transform.parent.gameObject;
        last = CD;
    }
    private void Update()
    {
        last += Time.deltaTime;
    }
    private bool CheckEnvironment(GameObject gameObject)
    {
        return gameObject.CompareTag("Weapon") || gameObject.CompareTag("Bullet");
    }
    private void OnTriggerStay(Collider other)
    {
        if (last >= CD)
        {
            if (!CheckEnvironment(other.gameObject) && !other.gameObject.CompareTag(father.tag))
            {
                Debug.Log(last);
                Debug.Log(other.gameObject);
                GameObject now = BulletPool.Instance.GetBullet(BulletKind.KINETICENERGY, transform.position, transform.rotation);
                now.GetComponent<BaseBullet>().SetBullet(other.gameObject, gameObject, bulletKind, bulletAtk, bulletSpeed, bulletIsChaser);
                last = 0.0f;
            }
        }
    }
}
