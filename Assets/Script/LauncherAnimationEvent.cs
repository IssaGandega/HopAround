using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherAnimationEvent : MonoBehaviour
{
    [SerializeField] private Launcher launcherBrain;
   
    /*public void ForceMoment()
    {
        launcherBrain.player.GetComponent<Rigidbody2D>().AddForce(Vector2.up*launcherBrain.force);
        Debug.Log("Force");
    }*/
    

    public void ReturnToRoutine()
    {
        launcherBrain.animator.SetInteger("State",0);
    }

    public void ChangeState()
    {
        launcherBrain.ChangeState();
    }
}
