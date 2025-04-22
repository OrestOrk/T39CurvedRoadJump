using System;
using UnityEngine;

public class CoinsBalanceController : MonoBehaviour
{
    //public event Action<int> OnCoinsChanged;

    private int _coins;
    private const string CoinsKey = "Coins";
    
    private CoinsBalanceView _coinsBalanceView;
    private void Awake()
    {
        _coinsBalanceView = GetComponent<CoinsBalanceView>();
        
        LoadCoins();
        _coinsBalanceView.DisplayCoins(_coins);
    }
    
    private void LoadCoins()
    {
        _coins = PlayerPrefs.GetInt(CoinsKey, 200000);
        Debug.Log($"Loaded {_coins}");
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt(CoinsKey, _coins);
        PlayerPrefs.Save();
    }

    public void AddCoins(int amount)
    {
        if (amount <= 0) return;

        _coins += amount;
        SaveCoins();
        //OnCoinsChanged?.Invoke(_coins);
        _coinsBalanceView.DisplayCoins(_coins);
    }

    public bool SpendCoins(int amount)
    {
        if (amount <= 0) return false;
        if (_coins < amount) return false;

        _coins -= amount;
        SaveCoins();
        //OnCoinsChanged?.Invoke(_coins);
        _coinsBalanceView.DisplayCoins(_coins);
        return true;
    }

    public bool HasEnoughCoins(int amount)
    {
        return _coins >= amount;
    }

    public int GetBalance()
    {
        return _coins;
    }
}