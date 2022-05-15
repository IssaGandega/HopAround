using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTitleMvm : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] private float speed;
    private float xAxisAccel;

    private void FixedUpdate()
    {
        xAxisAccel = Mathf.Clamp(Input.acceleration.x, -0.5f, 0.5f);
        rb.angularVelocity = xAxisAccel * speed;
    }
}
