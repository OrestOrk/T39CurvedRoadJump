using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _skullVFX;
    private Animator _animator;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayActivateSpikeAnimation()
    {
        _animator.SetTrigger("ShowSpike");
    }

    public void PlaySkullVFX()
    {
        _skullVFX.Play();
    }
}
