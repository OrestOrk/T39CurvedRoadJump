using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WheelView : MonoBehaviour
{
    [SerializeField] private RectTransform _wheelTransform;
    [SerializeField] private Button _spinButton;
    
    private float _animationDuration = 0.5f;
    
    private Vector3 _startPosition;

    private void Awake()
    {
        if (_wheelTransform != null)
        {
            _startPosition = _wheelTransform.anchoredPosition;
        }
    }

    public void ShowWheel()
    {
        if (_wheelTransform == null) return;
        gameObject.SetActive(true);
        _spinButton.interactable = true;
        
        _wheelTransform.DOAnchorPosY(_startPosition.y, _animationDuration).SetEase(Ease.OutBounce);
    }

    public void HideWheel()
    {
        if (_wheelTransform == null) return;
        _wheelTransform.DOAnchorPosY(_wheelTransform.anchoredPosition.y - 1000f, _animationDuration)
            .OnComplete(() => gameObject.SetActive(false));
    }

    public void ActivateSpinButton()
    {
        _spinButton.interactable = true;
    }

    public void DeactivateSpinButton()
    {
        _spinButton.interactable = false;
    }
}