using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperAnimatorEvent : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    public Jumper jumper;

    public void ForceMoment()
    {
        player.GetComponent<Rigidbody2D>().AddForce(Vector2.up*jumper.force);
    }
    
    public void StopPlayerControl()
    {
        jumper.playerIsEjected = false;
    }

    public void ReturnToRoutine()
    {
        jumper.animator.SetInteger("State",0);
    }
}
