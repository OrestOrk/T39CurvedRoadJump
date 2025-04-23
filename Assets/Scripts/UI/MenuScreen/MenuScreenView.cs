using DG.Tweening;
using UnityEngine;

public class MenuScreenView : MonoBehaviour
{
    [SerializeField] private GameObject _menuButtonContainer;
    private float _animationDuration = 0.3f;

    public void ShowMenu()
    {
        _menuButtonContainer.SetActive(true);
        _menuButtonContainer.transform.localScale = Vector3.zero;
        _menuButtonContainer.transform.DOScale(Vector3.one, _animationDuration).SetEase(Ease.OutBack);
    }

    public void HideMenu()
    {
        _menuButtonContainer.transform
            .DOScale(Vector3.zero, _animationDuration)
            .SetEase(Ease.InBack)
            .OnComplete(() => _menuButtonContainer.SetActive(false));
    }
}