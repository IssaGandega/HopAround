using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherSpring : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Launcher launcherBrain;
    [SerializeField] private AudioClip springSound;
    [SerializeField] private Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
            
            player.targetSpeed = 0;
            player.currentSpeed = 0;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            SoundManager.instance.PlaySound(springSound);
            launcherBrain.player = other.gameObject;
            launcherBrain.player.GetComponent<Rigidbody2D>().AddForce(Vector2.up*launcherBrain.force);
            animator.SetInteger("State", 3);
            launcherBrain.StartCoroutine(launcherBrain.CD());
        }
    }
}
