using UnityEngine;

[System.Serializable]
public class ShopProductData
{
    public string productName;
    public int productPrice;
    public int productID;
    public bool isPurchased;

    public void Open()
    {
        isPurchased = true;
    }
}