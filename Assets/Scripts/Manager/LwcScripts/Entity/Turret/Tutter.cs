using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutter : MonoBehaviour
{
    BulletPool bulletPool;
    private void Start()
    {
        bulletPool = FindObjectOfType<BulletPool>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(gameObject.tag))
        {
            // TODO 对象池发射子弹
            GameObject now = bulletPool.GetTurretBullet(transform.position, transform.rotation);
            TurretBullet bullet = now.GetComponent<TurretBullet>();
            bullet.from = gameObject;
            bullet.target = collision.gameObject;
        }
    }
}
