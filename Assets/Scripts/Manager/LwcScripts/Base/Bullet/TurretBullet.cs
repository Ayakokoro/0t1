using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : BaseBullet
{
    private void Start()
    {
        isChaser = true;
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        transform.forward = target.transform.position - transform.position;
        rigidBody.velocity = transform.forward * speed * Time.deltaTime;
    }
}
