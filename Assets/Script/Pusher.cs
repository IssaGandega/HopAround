using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour, ITonguable
{

    [SerializeField] private Animator animator;
    [SerializeField] private MovingPlatform mvm;
    [SerializeField] private float force;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            mvm.ChangeState();
            animator.SetInteger("State" ,1);
            col.GetComponent<Rigidbody2D>().AddForce((transform.position-col.transform.position).normalized*force,ForceMode2D.Impulse);
            StartCoroutine(CD());
        }
    }

    public void Tongued()
    {
        animator.SetInteger("State" ,2);
        mvm.ChangeState();
        StartCoroutine(CD());
    }


    private IEnumerator CD()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetInteger("State" ,0);
        mvm.ChangeState();


    }
}
