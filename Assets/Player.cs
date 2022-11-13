using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    public float speed;  

    new private Rigidbody2D rigidbody;

    private Animator animator;

    private float inputX, inputY;

    private float stopX, stopY;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        Vector2 input = transform.right * inputX + transform.up * inputY;
        rigidbody.velocity = input.normalized * speed;

        if(input == Vector2.zero)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
            stopX = inputX;
            stopY = inputY;
        }
        animator.SetFloat("InputX", stopX);
        animator.SetFloat("InputY", stopY);
    }

}
