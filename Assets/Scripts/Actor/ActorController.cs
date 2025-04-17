using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    [SerializeField] private ActorMove _actorMove;
    
    private WheelController _wheelController;

    private void Start()
    {
        _wheelController = ServiceLocator.GetService<WheelController>();
        
        _wheelController.OnWheelResult += StartJumps;
    }

    #region ItemsCallbacks
    public void TrapTrigger()
    {
        _actorMove.PauseJumps();
    }

    public void ChestTrigger()
    {
        _actorMove.PauseJumps();
        
        DelayManager.DelayAction(_actorMove.ResumeJumps,2f);
    }

    #endregion
    
    private void StartJumps(int jumpsAmount)
    {
        _actorMove.StartJumps(jumpsAmount);
    }
    
    private void OnDestroy()
    {
        _wheelController.OnWheelResult -= StartJumps;
    }
    
}
