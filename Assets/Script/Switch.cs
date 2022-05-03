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
    private bool On;
    

    public void Tongued()
    {
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
}
