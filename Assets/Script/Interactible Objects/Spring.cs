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
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Rigidbody2D>())
        {
            SoundManager.instance.PlaySound(springSound);
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(dir*force,ForceMode2D.Impulse);
            animator.SetBool("Go",true);
            StartCoroutine(CD());
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Rigidbody2D>())
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce((dir*force)/5,ForceMode2D.Impulse);
        }
    }


    private IEnumerator CD()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Go", false);
    }
}
