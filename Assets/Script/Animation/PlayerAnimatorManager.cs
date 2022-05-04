using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{

    public static PlayerAnimatorManager instance;

    [SerializeField]private Animator animator;

    private void Start()
    {
        instance = this;
    }

    public void AnimatorStateChange(int stateNo)
    {
        animator.SetInteger("StateNo", stateNo);
    }
}
