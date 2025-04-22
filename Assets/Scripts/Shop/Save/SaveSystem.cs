using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveSystem
{
    private static string _saveFilePath;

    static SaveSystem()
    {
        // Створюємо шлях до файлу збереження для всіх платформ
        _saveFilePath = Path.Combine(Application.persistentDataPath, "shop_data.save");
    }

    // Збереження даних у файл
    public static void SaveData(List<ShopProductData> products)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        // Перевірка, чи існує директорія для збереження
        Directory.CreateDirectory(Application.persistentDataPath); 
        
        FileStream file = File.Create(_saveFilePath);
        formatter.Serialize(file, products);
        file.Close();
    }

    // Завантаження даних з файлу
    public static List<ShopProductData> LoadData()
    {
        if (File.Exists(_saveFilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(_saveFilePath, FileMode.Open);

            List<ShopProductData> products = (List<ShopProductData>)formatter.Deserialize(file);
            file.Close();

            return products;
        }
        else
        {
            return new List<ShopProductData>(); // Повертаємо порожній список, якщо файл не знайдено
        }
    }
}