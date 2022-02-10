using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private Tongue tongue;
    private float xAxisAccel;

    private void OnEnable()
    {
        tongue = gameObject.GetComponent<Tongue>();
    }

    private void FixedUpdate()
    {
        xAxisAccel = Mathf.Clamp(Input.acceleration.x, -0.5f, 0.5f);
        if (xAxisAccel < -0.2f && xAxisAccel > 0.2f)
        {
            rb.velocity = Vector3.zero;
        }

        if (!tongue.isGrabing)
        {
            rb.velocity = new Vector3(xAxisAccel * speed, rb.velocity.y, 0);
        }


        if (Input.touchCount > 0)
        {
            if ((isGrounded) && (tongue.isGrabing))
            {
                rb.AddForce(Vector3.up*jumpForce);
            }
        }
        
        isGrounded = Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius);
    }
}
