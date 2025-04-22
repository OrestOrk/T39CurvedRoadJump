using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CoinsBalanceView : MonoBehaviour
{
    [SerializeField] private Text _coinText;
    [SerializeField] private Text _coinShopText;

    private int _currentCoins;

    private Tweener _countTween;
    private Tweener _punchTween;
    private Tweener _countShopTween;
    private Tweener _punchShopTween;

    public void DisplayCoins(int coins)
    {
        // Якщо є активні анімації, скидаємо їх
        if (_countTween != null && _countTween.IsActive()) 
            _countTween.Kill();
        if (_punchTween != null && _punchTween.IsActive()) 
            _punchTween.Kill();
        if (_countShopTween != null && _countShopTween.IsActive()) 
            _countShopTween.Kill();
        if (_punchShopTween != null && _punchShopTween.IsActive()) 
            _punchShopTween.Kill();

        // Анімація для _coinText
        _countTween = DOTween.To(() => _currentCoins, x => {
            _currentCoins = x;
            _coinText.text = _currentCoins.ToString();
        }, coins, 0.5f).SetEase(Ease.OutQuad);

        _punchTween = _coinText.rectTransform.DOPunchScale(Vector3.one * 0.2f, 0.3f, 5, 0.5f);

        // Анімація для _coinShopText
        _countShopTween = DOTween.To(() => _currentCoins, x => {
            _currentCoins = x;
            _coinShopText.text = _currentCoins.ToString();
        }, coins, 0.5f).SetEase(Ease.OutQuad);

        _punchShopTween = _coinShopText.rectTransform.DOPunchScale(Vector3.one * 0.2f, 0.3f, 5, 0.5f);
    }
}