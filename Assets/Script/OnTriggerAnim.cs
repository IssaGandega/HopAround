using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerAnim : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool once;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Player")) && once != true)
        {
            animator.SetBool("Go",true);
            once = true;
        }
    }
}
