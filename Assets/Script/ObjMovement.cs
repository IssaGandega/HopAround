using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    private float xAxisAccel;

    private void FixedUpdate()
    {
        xAxisAccel = Mathf.Clamp(Input.acceleration.x, -0.5f, 0.5f);
        if (xAxisAccel < -0.2f && xAxisAccel > 0.2f)
        {
            rb.velocity = Vector3.zero;
        }
        rb.velocity = new Vector3(xAxisAccel * speed, rb.velocity.y, 0);

    }
}
