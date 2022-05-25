using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Boutton : MonoBehaviour
{
    [SerializeField] private UnityEvent eventsOnPressed;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioClip buttonSoundTime;
    private bool once;
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (!once)
            {
                SoundManager.instance.PlaySound(buttonSound);
                eventsOnPressed.Invoke();
                animator.SetBool("Go",true);

            }
            
            else
            {
                once = true;
            }
        }
    }
}
