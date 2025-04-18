using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapView : MonoBehaviour
{
    [SerializeField] private Animator _rotateAniamtor;
    
    [SerializeField] private Animation _actionAnimation;

    public void TrapAnimation()
    {
        _actionAnimation.Play();
        _rotateAniamtor.enabled = false;
        
    }
}
