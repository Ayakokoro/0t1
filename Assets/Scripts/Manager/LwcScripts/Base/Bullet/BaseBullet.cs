using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// ◊”µØ÷÷¿‡
/// </summary>
public enum BulletKind
{
    KINETICENERGY,
    CHEMICAL,
    DIRECTEDENERGY
}
public abstract class BaseBullet : MonoBehaviour
{
    protected BulletPool bulletPool;
    protected GameObject target;
    protected GameObject from;
    protected Rigidbody rigidBody;
    protected int atk;
    protected int speed;
    protected bool isChaser;
    public BulletKind kind;
    protected void Start()
    {
        bulletPool = GameObject.Find("BulletPool").GetComponent<BulletPool>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (isChaser)
        {
            if (collision.gameObject == target)
            {
                collision.gameObject.GetComponent<BaseEntity>().Damage(kind, atk);
                Debug.Log(gameObject);
                Debug.Log(bulletPool);
                bulletPool.RecycleBullet(gameObject);
            }
        } 
        else
        {
            if (!collision.gameObject.CompareTag(from.gameObject.tag))
            {
                collision.gameObject.GetComponent<BaseEntity>().Damage(kind, atk);
                bulletPool.RecycleBullet(gameObject);
            }
        }
    }

    public void SetBullet(GameObject _target, GameObject _from, BulletKind _bulletKind, int _bulletAtk, int _bulletSpeed, bool _bulletIsChaser)
    {
        target = _target;
        from = _from;
        kind = _bulletKind;
        atk = _bulletAtk;
        speed = _bulletSpeed;
        isChaser = _bulletIsChaser;
    }
}
