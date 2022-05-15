using UnityEngine;
using UnityEngine.Events;

public class TriggerEnter2D : MonoBehaviour
{
    public UnityEvent OnTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTrigger.Invoke();
    }
}
