using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinsSmallView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _textVFX;

    public void ActivateEffect()
    {
        _textVFX.Play();
        PickUpAnimation();
    }
    
    private void PickUpAnimation()
    {
        transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => Destroy(gameObject));
    }
}
