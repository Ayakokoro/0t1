using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : BaseBullet
{
    private void Start()
    {
        base.Start();
        isChaser = true;
        rigidBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (isChaser) ChaserUpdate();
        else { } // TODO Not Chaser
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
            bulletPool.RecycleBullet(gameObject);
        }
    }
}
