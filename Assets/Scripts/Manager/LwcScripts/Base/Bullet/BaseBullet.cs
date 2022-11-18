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
public class BaseBullet : MonoBehaviour
{
    protected GameObject target;
    protected GameObject from;
    protected Rigidbody rigidBody;
    protected int atk;
    protected int speed;
    protected bool isChaser;
    protected BulletKind kind;
    protected void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (isChaser) ChaserUpdate();
        else { NotChaserUpdate(); } // TODO Not Chaser
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (isChaser)
        {
            if (collision.gameObject == target)
            {
                collision.gameObject.GetComponent<BaseEntity>().Damage(kind, atk);
                Debug.Log(gameObject);
                BulletPool.Instance.RecycleBullet(gameObject, kind);
            }
        } 
        else
        {
            if (!collision.gameObject.CompareTag(from.gameObject.tag))
            {
                collision.gameObject.GetComponent<BaseEntity>().Damage(kind, atk);
                BulletPool.Instance.RecycleBullet(gameObject, kind);
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
    public void SetBullet(GameObject _from, BulletKind _bulletKind, int _bulletAtk, int _bulletSpeed, bool _bulletIsChaser)
    {
        from = _from;
        kind = _bulletKind;
        atk = _bulletAtk;
        speed = _bulletSpeed;
        isChaser = _bulletIsChaser;
    }
    public void SetBullet(BulletKind _bulletKind)
    {
        kind = _bulletKind;
    }
    private void NotChaserUpdate()
    {

    }
    private void ChaserUpdate()
    {
        if (target != null)
        {
            transform.forward = target.transform.position - transform.position;
            rigidBody.velocity = transform.forward * speed;
        }
        else
        {
            BulletPool.Instance.RecycleBullet(gameObject, kind);
        }
    }
}
