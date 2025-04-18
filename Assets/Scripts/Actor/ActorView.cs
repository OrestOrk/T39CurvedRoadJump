using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _jumpEndVFX;

    public void TouchDownEffect()
    {
        _jumpEndVFX.Play();
    }
}
