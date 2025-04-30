using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : BaseRoadItem
{
    private BombView _bombView;

    private void Start()
    {
        base.Start();
        
        _bombView = GetComponent<BombView>();
    }
    
    public override void Activate()
    {
        _actorController.BombTrigger();
        
        _bombView.ActivationEffect();
        
        AudioController.instance.PlayExplosionBomb();
    }
}
