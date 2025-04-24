using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopView : MonoBehaviour
{
   
    [Header("Text")]
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _priceText;
    [Space(10)]
    
    [Header("Buttons")]
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _selectButton;
    [Space(10)]
    
    [Header("FX")]
    [SerializeField] private ParticleSystem _purchaseEffect;
    [Space(10)]
    
    [Header("Other")]
    [SerializeField] private GameObject _selectionIndecator;
    [SerializeField] private Transform _productContainer;

    private float _scrollStep = 5f;
    
    public void ScrollTo(int index)
    {
        
        float targetZ = index * _scrollStep;
        _productContainer.DOLocalMoveZ(targetZ, 0.5f).SetEase(Ease.OutCubic);
    }

    public void ShowProductInfo(ShopProductData product)
    {
        _nameText.text = product.productName;
        _priceText.text = product.productPrice.ToString();

        _buyButton.gameObject.SetActive(!product.isPurchased);
        _selectButton.gameObject.SetActive(product.isPurchased);
        _priceText.gameObject.SetActive(!product.isPurchased);
    }

    public void ControllSelectionIndecator(bool state)
    {
        _selectionIndecator.SetActive(state);
    }

    public void PlayPurchaseEffect()
    {
        _purchaseEffect.Play();
    }
}