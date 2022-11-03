using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : BaseEntity
{
    [SerializeField] private BulletPool bulletPool;
    private float last;
    [Header("×Óµ¯²ÎÊý")]
    public float CD;
    public BulletKind bulletKind;
    public bool bulletIsChaser;
    public int bulletAtk;
    public int bulletSpeed;
    private void Start()
    {
        Debug.Log("New Turret");
        bulletPool = GameObject.Find("BulletPool").GetComponent<BulletPool>();
        last = CD;
    }
    private void Update()
    {
        last += Time.deltaTime;
    }
    private void OnTriggerStay(Collider collision)
    {
        Debug.Log(last);
        if (last >= CD)
        {
            if (!collision.gameObject.CompareTag(gameObject.tag))
            {
                Debug.Log(collision.gameObject);
                GameObject now = bulletPool.GetBullet(BulletKind.KINETICENERGY, transform.position, transform.rotation);
                TurretBullet bullet = now.GetComponent<TurretBullet>();
                bullet.SetBullet(collision.gameObject, gameObject, bulletKind, bulletAtk, bulletSpeed, bulletIsChaser);
                last = 0.0f;
            }
        }
    }
}
