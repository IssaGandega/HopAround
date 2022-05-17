using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] private Vector2 dir;
    [SerializeField] private float force;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip springSound;
    
    /*private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("A");
        if (other.gameObject.GetComponent<Rigidbody2D>())
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(dir*force,ForceMode2D.Impulse);
            SoundManager.instance.PlaySound(springSound);
            animator.Play("003_Interaction");
        }
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Rigidbody2D>())
        {
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            other.GetComponent<Rigidbody2D>().AddForce(dir*force,ForceMode2D.Impulse);
            SoundManager.instance.PlaySound(springSound);
            animator.Play("003_Interaction");
        }
    }

    /*
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Rigidbody2D>())
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce((dir*force),ForceMode2D.Impulse);
        }
    }*/
}
