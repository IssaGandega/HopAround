using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Boutton : MonoBehaviour
{
    [SerializeField] private UnityEvent eventsOnPressed;
    [SerializeField] private UnityEvent eventsOnReleased;
    [SerializeField] private bool hastTime;
    [SerializeField] private float time;
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
        
            if (hastTime)
            {
                SoundManager.instance.PlaySound(buttonSoundTime);
                StartCoroutine(Wait());
            }
            else
            {
                once = true;
            }
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(time);
        eventsOnReleased.Invoke();
        animator.SetBool("Go", false);
    }
}
