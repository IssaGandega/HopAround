using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] PS;
    
    
    private void PlayParticle(int Number)
    {
        PS[Number].Play(true);
    }
    
}
