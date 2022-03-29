using System;
using UnityEngine;
using UnityEngine.Events;

public class PressurPad : MonoBehaviour
{
    [SerializeField] private UnityEvent events;
    [SerializeField] private Material mat;
    [SerializeField] private Color colorOn;
    [SerializeField] private Color colorOff;

    private void OnEnable()
    {
        mat.color = colorOff;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Pressed();
    }
    
    public void Pressed()
    {
        events.Invoke();
        mat.color = colorOn;
    }

}
