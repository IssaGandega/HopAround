using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherSpring : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Launcher launcherBrain;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            launcherBrain.player = other.gameObject;
            launcherBrain.player.GetComponent<Rigidbody2D>().AddForce(Vector2.up*launcherBrain.force);
            animator.SetInteger("State", 3);
            launcherBrain.StartCoroutine(launcherBrain.CD());
        }
    }
}
