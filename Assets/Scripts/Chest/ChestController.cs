using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : BaseRoadItem
{
    private ChestView _chestView;
    private void Start()
    {
        base.Start();
        _chestView = GetComponent<ChestView>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Activate();
        }
    }

    public override void Activate()
    {
        _actorController.ChestTrigger();
        
        _chestView.OpenChest();
        
        //addcoins
        
        DelayManager.DelayAction(AddCoins,2f);
    }

    private void AddCoins()
    {
        int coins = GetRandomCoins();
        
        _chestView.DisplayCoins(coins);
    }
    
    private int GetRandomCoins()
    {
        return UnityEngine.Random.Range(1, 11) * 500;
    }
}
