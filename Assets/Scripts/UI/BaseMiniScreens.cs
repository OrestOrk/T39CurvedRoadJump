using UnityEngine;
using DG.Tweening;

public abstract class BaseMiniScreens : MonoBehaviour
{
    [SerializeField] protected GameObject _screenPanel;
    [SerializeField] protected GameObject _screenContainer;

     protected float _animationDuration = 0.5f;

    public virtual void ShowScreen()
    {
        _screenPanel.SetActive(true);
        _screenContainer.SetActive(true);

        _screenContainer.transform.localScale = Vector3.zero;
        _screenContainer.transform.DOScale(Vector3.one, _animationDuration).SetEase(Ease.OutBack);
    }

    public virtual void HideScreen()
    {
        _screenContainer.transform.DOScale(Vector3.zero, _animationDuration).SetEase(Ease.OutBack).OnComplete(() =>
        {
            _screenPanel.SetActive(false);
            _screenContainer.SetActive(false);
        });
    }
}