using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void DoorOpenRotation()
    {
        animator.Play("003_Interaction_ON");
    }

    public void DoorCloseRotation()
    {
        animator.Play("003_Interaction_OFF");
    }
}
