using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSmallController : BaseRoadItem
{
    private CoinsSmallView _coinsSmallView;
    
    private CoinsBalanceController _coinsBalanceController;
    private const int COINS_REWARD = 100;
    private void Start()
    {
        base.Start();
        
        _coinsSmallView = GetComponent<CoinsSmallView>();
        _coinsBalanceController = ServiceLocator.GetService<CoinsBalanceController>();
    }
    
    public override void Activate()
    {
        _coinsBalanceController.AddCoins(COINS_REWARD);
        _coinsSmallView.ActivateEffect();
        
        AudioController.instance.PlayBonusClip();
    }
}
