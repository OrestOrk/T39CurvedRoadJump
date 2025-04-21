using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CoinsBalanceView : MonoBehaviour
{
    [SerializeField] private Text _coinText;

    private int _currentCoins;

    private Tweener _countTween;
    private Tweener _punchTween;

    public void DisplayCoins(int coins)
    {
        if (_countTween != null && _countTween.IsActive()) 
            _countTween.Kill();

        if (_punchTween != null && _punchTween.IsActive()) 
            _punchTween.Kill();

        _countTween = DOTween.To(() => _currentCoins, x => {
            _currentCoins = x;
            _coinText.text = _currentCoins.ToString();
        }, coins, 0.5f).SetEase(Ease.OutQuad);

        _punchTween = _coinText.rectTransform.DOPunchScale(Vector3.one * 0.2f, 0.3f, 5, 0.5f);
    }
}