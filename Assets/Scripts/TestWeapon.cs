using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TestWeapon : Weapon
{
    private float lastTime = 0f;
    public override void DoAttack(Vector3 dir)
    {
        if (isAllowedShoot())
        {
            Shooting(dir);
            lastTime = Time.time;
        }
    }
    private float[] GenArr = new float[] { -15,-30,0,15,30 };

    protected override void Shooting(Vector3 dir)
    {
        BulletPool bulletPool = GameObject.FindObjectOfType<BulletPool>();
        //GameObject bullet = bulletPool.GetBullets(CasingPos.position, Quaternion.FromToRotation(Vector3.right, dir));

        for (int i = 0; i < 5; i++) 
        {
            Vector3 exactDir = Quaternion.Euler(0, 0, GenArr[i]) * dir;
            GameObject bullet = bulletPool.GetBullets(CasingPos.position, Quaternion.FromToRotation(Vector3.right, exactDir));
            Bullet bullet1 = bullet.GetComponent<Bullet>();
            bullet1.TTL = 3f;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(exactDir * bulletSpeed);
        }
        //Bullet bullet1 = bullet.GetComponent<Bullet>();
        //bullet1.TTL = 3f;

        //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //rb.AddForce(dir * bulletSpeed);
    }

    protected override bool isAllowedShoot()
    {
        return Time.time - lastTime >= attackDelay;
    }


}
