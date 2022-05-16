using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Vector3 openRotation;
    [SerializeField] private Vector3 closeRotation;
    public void DoorOpenRotation()
    {
        transform.DORotate(openRotation, 1f);
    }

    public void DoorCloseRotation()
    {
        transform.DORotate(closeRotation, 1f);
    }
}
