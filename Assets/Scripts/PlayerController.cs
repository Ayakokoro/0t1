using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Rendering;

public enum DIRECTION
{
    Clockwise,
    Counterclockwise,
}

public class PlayerController : MonoBehaviour
{
    public double hp;
    public double delay;
    public float cwForce;
    public float ccwForce;
    public float straightForce;
    public Vector3 forwardGlobal;

    private Rigidbody2D rb;
    private double timer;
    private string nowCollisionName;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        forwardGlobal = rb.transform.right;
    }

    public void Movement(DIRECTION idir, Vector3 dir)
    {
        float cosTheta = Vector3.Dot(dir, forwardGlobal);
        if (idir == DIRECTION.Clockwise)
        {
            if (cosTheta <= Mathf.Sqrt(3) / 2.0) rb.AddTorque(-cwForce);
        }
        else
        {
            if (cosTheta <= Mathf.Sqrt(3) / 2.0) rb.AddTorque(ccwForce);
        }
        if (cosTheta > Mathf.Sqrt(3) / 2.0) rb.AddForce(forwardGlobal * straightForce);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        timer -= Time.deltaTime;
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "PotionFog" && timer <= 0) 
        {
            hp -= 0.1;
        }
        if (timer <= 0) timer = delay;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionName = collision.gameObject.name;
        Debug.Log(collisionName);
        if (nowCollisionName != collisionName) 
        {
            nowCollisionName = collisionName;
            hp -= 1;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        nowCollisionName = "";
    }
}
