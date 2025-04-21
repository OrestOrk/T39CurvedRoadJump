using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public event Action OnJumpsSeriesComplette;
    
    private ActorMove _actorMove;
    private ActorView _actorView;
    
    private WheelController _wheelController;

    private void Start()
    {
        _wheelController = ServiceLocator.GetService<WheelController>();
        
        _actorMove = GetComponent<ActorMove>();
        _actorView = GetComponent<ActorView>();
        
        _wheelController.OnWheelResult += StartJumps;

        _actorMove.OnTouchdown += _actorView.TouchDownEffect;
        _actorMove.OnJumpsSeriesEnd += SendJumpsComplette;
    }

    #region ItemsCallbacks
    public void TrapTrigger()
    {
        _actorMove.PauseJumps();
    }

    public void BombTrigger()
    {
        _actorMove.PauseJumps();
    }

    public void BootJumpTrigger()
    {
        _actorMove.BonusJump();
        _actorView.PlayBonusJumpEffect();
    }

    public void ChestTrigger()
    {
        _actorMove.PauseJumps();
        
        DelayManager.DelayAction(_actorMove.ResumeJumps,3f);
    }

    #endregion
    
    private void StartJumps(int jumpsAmount)
    {
        _actorMove.StartJumps(jumpsAmount);
    }

    private void SendJumpsComplette()
    {
        OnJumpsSeriesComplette?.Invoke();
    }
    
    private void OnDestroy()
    {
        _wheelController.OnWheelResult -= StartJumps;
        _actorMove.OnTouchdown -= _actorView.TouchDownEffect;
        _actorMove.OnJumpsSeriesEnd -= SendJumpsComplette;
    }
    
}
