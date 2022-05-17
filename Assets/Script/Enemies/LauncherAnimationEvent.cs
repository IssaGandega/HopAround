using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherAnimationEvent : MonoBehaviour
{
    [SerializeField] private Launcher launcherBrain;

    public void ReturnToRoutine()
    {
        launcherBrain.animator.SetInteger("State",0);
    }

    public void ChangeState()
    {
        launcherBrain.ChangeState();
    }
}
