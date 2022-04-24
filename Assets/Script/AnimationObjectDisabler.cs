using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationObjectDisabler : MonoBehaviour
{
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
