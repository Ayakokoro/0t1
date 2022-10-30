using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    protected bool isChaser;
    protected Rigidbody2D rigidBody;
    public GameObject target;
    public GameObject from;
    public int atk;
    public int speed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isChaser)
        {
            if (collision.gameObject == target)
            {
                collision.gameObject.GetComponent<Health>().Damage(atk);
            }
        } 
        else
        {
            if (!collision.gameObject.CompareTag(from.gameObject.tag))
            {
                collision.gameObject.GetComponent<Health>().Damage(atk);
            }
        }
    }
}
