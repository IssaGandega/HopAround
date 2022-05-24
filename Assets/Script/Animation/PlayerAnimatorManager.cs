using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{

    public static PlayerAnimatorManager instance;

    [SerializeField]private Animator animator;
    private bool stopReceivingState;

    private void Start()
    {
        instance = this;
    }

    public void AnimatorStateChange(int stateNo,bool stopReceptor)
    {
        if (!stopReceivingState)
            animator.SetInteger("StateNo", stateNo);

        if (stopReceptor)
            StartCoroutine(CD());
        
    }

    private IEnumerator CD()
    {
        stopReceivingState = true;
        yield return new WaitForSeconds(0.1f);
        stopReceivingState = false;
    }
}
