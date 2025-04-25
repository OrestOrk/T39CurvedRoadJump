using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Serialization;

public class CoinsBalanceView : MonoBehaviour
{
    [SerializeField] private Text _coinText;
    [SerializeField] private Text _coinShopText;
    [SerializeField] private Text _coinDailyBonusText; // новий текст

    private int _currentCoins;

    private Tweener _countTween;
    private Tweener _punchTween;
    private Tweener _countShopTween;
    private Tweener _punchShopTween;
    private Tweener _countPopupTween;
    private Tweener _punchPopupTween;

    public void DisplayCoins(int coins)
    {
        // Скидаємо всі активні твіни
        _countTween?.Kill();
        _punchTween?.Kill();
        _countShopTween?.Kill();
        _punchShopTween?.Kill();
        _countPopupTween?.Kill();
        _punchPopupTween?.Kill();

        _currentCoins = coins;

        // Оновлення _coinText
        if (_coinText.gameObject.activeInHierarchy)
        {
            _countTween = DOTween.To(() => int.Parse(_coinText.text), x => {
                _coinText.text = x.ToString();
            }, coins, 0.5f).SetEase(Ease.OutQuad);

            _punchTween = _coinText.rectTransform.DOPunchScale(Vector3.one * 0.2f, 0.3f, 5, 0.5f);
        }
        else
        {
            _coinText.text = coins.ToString();
        }

        // Оновлення _coinShopText
        if (_coinShopText.gameObject.activeInHierarchy)
        {
            _countShopTween = DOTween.To(() => int.Parse(_coinShopText.text), x => {
                _coinShopText.text = x.ToString();
            }, coins, 0.5f).SetEase(Ease.OutQuad);

            _punchShopTween = _coinShopText.rectTransform.DOPunchScale(Vector3.one * 0.2f, 0.3f, 5, 0.5f);
        }
        else
        {
            _coinShopText.text = coins.ToString();
        }

        // Оновлення _coinPopupText
        if (_coinDailyBonusText.gameObject.activeInHierarchy)
        {
            _countPopupTween = DOTween.To(() => int.Parse(_coinDailyBonusText.text), x => {
                _coinDailyBonusText.text = x.ToString();
            }, coins, 0.5f).SetEase(Ease.OutQuad);

            _punchPopupTween = _coinDailyBonusText.rectTransform.DOPunchScale(Vector3.one * 0.2f, 0.3f, 5, 0.5f);
        }
        else
        {
            _coinDailyBonusText.text = coins.ToString();
        }
    }
}
