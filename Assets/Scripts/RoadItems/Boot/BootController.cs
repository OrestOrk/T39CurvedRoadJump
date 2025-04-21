using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootController : BaseRoadItem
{
    private BootView _bootView;

    private void Start()
    {
        base.Start();
        _bootView = GetComponent<BootView>();
    }

    public override void Activate()
    {
        _bootView.ActivationEffect();
        
        _actorController.BootJumpTrigger();
    }
}
