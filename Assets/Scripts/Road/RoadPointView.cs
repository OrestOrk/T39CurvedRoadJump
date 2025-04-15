using System;
using UnityEngine;
using DG.Tweening;

public class RoadPointView : MonoBehaviour
{
    [SerializeField] private GameObject _pointModel;
    
    private float _animationDuration = 0.3f;
    private Vector3 _startScale;
    private Vector3 _animatedScale = new Vector3(0.7f, 0.7f, 0.7f);

    private void Start()
    {
        _startScale = _pointModel.transform.localScale;
    }

    public void AnimateScale()
    {
        _pointModel.transform.DOScale(_animatedScale,_animationDuration).SetEase(Ease.OutBounce)
            .OnComplete(() => _pointModel.transform.localScale = _startScale);
        Debug.Log("PointAnimate");
    }
}
