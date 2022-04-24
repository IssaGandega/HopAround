using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    [SerializeField] private UnityEvent events;
    [SerializeField] private Material mat;
    [SerializeField] private Color colorOn;
    [SerializeField] private Color colorOff;

    private void OnEnable()
    {
        mat.color = colorOff;
    }

    public void TongueTouched()
    {
        events.Invoke();
        if (mat.color == colorOn)
        {
            mat.color = colorOff;
        }
        else
        {
            mat.color = colorOn;
        }
    }

}
