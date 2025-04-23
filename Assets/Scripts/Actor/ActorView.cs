using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _jumpEndVFX;
    [SerializeField] private ParticleSystem _bigJumpVFX;
    [SerializeField] private ParticleSystem _deathVFX;

    public void TouchDownEffect()
    {
        _jumpEndVFX.Play();
    }

    public void PlayBonusJumpEffect()
    {
        _jumpEndVFX.Play();
        
        DelayManager.DelayAction(StopBonusJumpEffect,1f);
    }

    public void PlayDeathEffect()
    {
        _deathVFX.Play();
    }

    private void StopBonusJumpEffect()
    {
        _jumpEndVFX.Stop();
    }
}
