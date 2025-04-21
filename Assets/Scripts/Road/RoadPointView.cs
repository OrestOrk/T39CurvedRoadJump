using System;
using UnityEngine;
using DG.Tweening;

public class RoadPointView : MonoBehaviour
{
    [SerializeField] private GameObject _pointModel;
    [SerializeField] private Transform _itemContainer;
    
    [Header("Optional. TRAPS/BONUCES and others")]
    [SerializeField] private BaseRoadItem _roadItem = null;
    
    private float _animationDuration = 0.3f;
    private Vector3 _startScale;
    private Vector3 _animatedScale = new Vector3(0.7f, 0.7f, 0.7f);

    private void Start()
    {
        _startScale = _pointModel.transform.localScale;
        SetupItemTransform();
    }

    public void SetItem(BaseRoadItem roadItem)
    {
        _roadItem = roadItem;
        SetupItemTransform();
    }
    public void AnimateScale()
    {
        _pointModel.transform.DOScale(_animatedScale,_animationDuration).SetEase(Ease.OutBounce)
            .OnComplete(() => _pointModel.transform.localScale = _startScale);
        Debug.Log("PointAnimate");
    }

    public void TryActivateItem()
    {
        if (_roadItem != null)
        {
            _roadItem.Activate();
        }
    }

    private void SetupItemTransform()
    {
        if (_roadItem != null)
        {
            _roadItem.transform.SetParent(_itemContainer);
            _roadItem.transform.localPosition = Vector3.zero;
            _roadItem.transform.localRotation = Quaternion.identity;
            _roadItem.transform.localScale = Vector3.one;
        }
    }
    
}
