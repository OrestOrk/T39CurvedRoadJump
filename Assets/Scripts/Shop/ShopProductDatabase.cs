using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ShopProductDatabase", menuName = "Shop/ShopProductDatabase")]
public class ShopProductDatabase : ScriptableObject
{
    public List<ShopProductData> products;
}