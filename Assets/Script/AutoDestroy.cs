using System.Collections;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float delay = 1;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
