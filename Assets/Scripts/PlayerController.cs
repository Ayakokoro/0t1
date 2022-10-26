using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DIRECTION
{
    Clockwise,
    Counterclockwise,
}

public class PlayerController : MonoBehaviour
{
    public float cwForce;
    public float ccwForce;
    public float straightForce;
    public Vector3 forwardGlobal;

    private Rigidbody2D rb;
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

    public void Movement(DIRECTION dir)
    {
        if (dir == DIRECTION.Clockwise)
        {
            rb.AddTorque(-cwForce);
        }
        else
        {
            rb.AddTorque(ccwForce);
        }
        rb.AddForce(forwardGlobal * straightForce);
    }

    public void Attack()
    {

    }
}
