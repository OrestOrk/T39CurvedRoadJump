using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootController : BaseRoadItem
{
    [SerializeField] private BootView _bootView;

    private void Start()
    {
        _bootView = GetComponent<BootView>();
    }

    public override void Activate()
    {
        _bootView.ActivationEffect();
        
        _actorController.BootJumpTrigger();
    }
}
