using UnityEngine;

public class ShopController : MonoBehaviour
{ 
    private ShopProductDatabase _productDatabase;
    private CoinsBalanceController _coinsBalanceController;
    private CharacterSelectionController _characterSelectionController;
    private ShopView _shopView;

    private int _currentIndex = 0;

    private void Start()
    {
        _shopView = GetComponent<ShopView>();
        _productDatabase = ServiceLocator.GetService<ShopProductDatabase>();
        _coinsBalanceController = ServiceLocator.GetService<CoinsBalanceController>();
        _characterSelectionController = ServiceLocator.GetService<CharacterSelectionController>();
        
        LoadPurchasedProducts();
        
        ShowCurrentProduct();
        
        ControllSelectionIndecator();
    }
     
    //load data
    private void LoadPurchasedProducts()
    {
        var savedProducts = SaveSystem.LoadData(); // Завантажуємо збережені дані з файлу

        foreach (var savedProduct in savedProducts)
        {
            var product = _productDatabase.products.Find(p => p.productID == savedProduct.productID);
            if (product != null)
            {
                product.isPurchased = savedProduct.isPurchased; // Оновлюємо статус покупки для відповідного товару
            }
        }
    }

    public void NextProduct()
    {
        _currentIndex = (_currentIndex + 1) % _productDatabase.products.Count;
        _shopView.ScrollTo(_currentIndex);
        ShowCurrentProduct();
        ControllSelectionIndecator();
    }

    public void PreviousProduct()
    {
        _currentIndex--;
        if (_currentIndex < 0) _currentIndex = _productDatabase.products.Count - 1;
        _shopView.ScrollTo(_currentIndex);
        ShowCurrentProduct();
        ControllSelectionIndecator();
    }

    private void ShowCurrentProduct()
    {
        var currentProduct = _productDatabase.products[_currentIndex];
        _shopView.ShowProductInfo(currentProduct);
    }

    // Метод для покупки товару
    public void PurchaseCurrentProduct()
    {
        var currentProduct = _productDatabase.products[_currentIndex];
        if (!currentProduct.isPurchased) // Якщо товар ще не куплений
        {
            if (_coinsBalanceController.SpendCoins(currentProduct.productPrice))
            {
                currentProduct.Open(); // Купуємо товар
                SavePurchasedProduct(currentProduct); // Зберігаємо статус покупки
                ShowCurrentProduct(); // Оновлюємо інформацію на екрані
                
                _shopView.PlayPurchaseEffect();
            }
        }
    }

    public void SelectCharacter()
    {
        int ID = _productDatabase.products[_currentIndex].productID;
        _characterSelectionController.PlayerSelected(ID);
        
        ControllSelectionIndecator();
    }

    private void ControllSelectionIndecator()
    {
        if (_currentIndex + 1 == _characterSelectionController.selectedCharacterID)
        {
            _shopView.ControllSelectionIndecator(true);
        }
        else
        {
            _shopView.ControllSelectionIndecator(false);
        }
    }
    
    private void SavePurchasedProduct(ShopProductData product)
    {
        var savedProducts = SaveSystem.LoadData(); 
        var existingProduct = savedProducts.Find(p => p.productID == product.productID);

        if (existingProduct != null)
        {
            existingProduct.isPurchased = product.isPurchased; // Оновлюємо статус покупки
        }
        else
        {
            savedProducts.Add(product); 
        }

        SaveSystem.SaveData(savedProducts); 
    }
}
