using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : BaseRoadItem
{
    private ChestView _chestView;
    private CoinsBalanceController _coinsBalanceController;
    private void Start()
    {
        base.Start();
        
        _chestView = GetComponent<ChestView>();
        
        _coinsBalanceController = ServiceLocator.GetService<CoinsBalanceController>();
    }

    public override void Activate()
    {
        _actorController.ChestTrigger();
        
        _chestView.OpenChest();
        
        AudioController.instance.PlayChestOpen();
        
        DelayManager.DelayAction(AddCoins,2f);
    }

    private void AddCoins()
    {
        int coins = GetRandomCoins();
        
        _coinsBalanceController.AddCoins(coins);
        _chestView.DisplayCoins(coins);
    }
    
    private int GetRandomCoins()
    {
        return UnityEngine.Random.Range(1, 11) * 500;
    }
}
