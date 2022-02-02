using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float force;
    [SerializeField] private float jumpForce;
    private float xAxisAccel;

    private void FixedUpdate()
    {
        xAxisAccel = Mathf.Clamp(Input.acceleration.x, -0.5f, 0.5f);
        if (xAxisAccel < -0.2f && xAxisAccel > 0.2f)
        {
            rb.velocity = Vector3.zero;
        }
        rb.velocity = new Vector3(xAxisAccel * force, rb.velocity.y, 0);

        if (Input.touchCount > 0)
        {
            rb.AddForce(Vector3.up*jumpForce);
        }
    }
}
