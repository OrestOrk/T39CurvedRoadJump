using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSmallController : BaseRoadItem
{
    private CoinsSmallView _coinsSmallView;

    private void Start()
    {
        base.Start();
        _coinsSmallView = GetComponent<CoinsSmallView>();
        
    }
    
    public override void Activate()
    {
        //add 100 coins to coinsytem
        _coinsSmallView.ActivateEffect();
    }
}
