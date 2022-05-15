using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void SetAnim()
    {
        animator.SetBool("Go",!animator.GetBool("Go"));
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
