using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _jumpEndVFX;
    [SerializeField] private ParticleSystem _bigJumpVFX;
    [SerializeField] private ParticleSystem _deathVFX;
    [SerializeField] private ParticleSystem _brokenHeartVFX;

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

    public void PlayBrokenHeartEffect()
    {
        _brokenHeartVFX.Play();
    }
    private void StopBonusJumpEffect()
    {
        _jumpEndVFX.Stop();
    }
}
