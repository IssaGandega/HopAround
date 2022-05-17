using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour, ITonguable
{
    [SerializeField] private UnityEvent eventsOn;
    [SerializeField] private UnityEvent eventsOff;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip switchSound;
    private bool On;
    

    public void TongueTouched()
    {
        SoundManager.instance.PlaySound(switchSound);
        if (On)
        {
            eventsOff.Invoke();
            animator.SetBool("Go", false);
            On = false;
        }
        else
        {
            eventsOn.Invoke();
            animator.SetBool("Go", true);
            On = true;
        }
    }
    

    public void Tongued(Tongue tongue)
    {
        tongue.line.enabled = true;
        tongue.pointIsAnInteractable = true;
    }
    
    
}
