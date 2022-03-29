using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private Vector2 dir;
    [SerializeField] private float force;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Rigidbody2D>())
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(dir*force,ForceMode2D.Impulse);
        }
    }
}
