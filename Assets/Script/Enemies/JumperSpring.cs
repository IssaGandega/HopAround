using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperSpring : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Jumper jumperMain;
    [SerializeField] private JumperAnimatorEvent jumperAnimatorEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jumperAnimatorEvent.player = other.gameObject;
            jumperMain.player = other.gameObject;
            animator.SetInteger("State", 3);
        }
    }
}
