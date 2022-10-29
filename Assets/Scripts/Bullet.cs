using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float TTL;
    [SerializeField] private GameObject from;

    private void Update()
    {
        TTL -= Time.deltaTime;
        if (TTL <= 0)
        {
            Unregister();
        }
    }
    private void Unregister()
    {
        BulletPool pool = GameObject.FindObjectOfType<BulletPool>();
        pool.Recycle(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            Unregister();
        }
    }
}
