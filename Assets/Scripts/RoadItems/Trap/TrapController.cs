using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : BaseRoadItem
{
    private TrapView _trapView;
    
    private void Start()
    {
        base.Start();
        
        _trapView = GetComponent<TrapView>();
    }
    
    public override void Activate()
    {
        _trapView.TrapAnimation();

        _actorController.TrapTrigger();
        AudioController.instance.PlayTrap();
    }
}
