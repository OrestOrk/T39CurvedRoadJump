using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopView : MonoBehaviour
{
    [SerializeField] private Transform _productContainer;
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _priceText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _selectButton;
    [SerializeField] private GameObject _selectionIndecator;

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
}