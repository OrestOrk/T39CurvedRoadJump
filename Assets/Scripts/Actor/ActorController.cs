using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public event Action OnResetActor;
    public event Action OnActorDeath;
    public event Action OnJumpsSeriesComplette;
    
    private ActorMove _actorMove;
    private ActorView _actorView;
    
    private WheelController _wheelController;
    private GameController _gameController;
    
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    
    private void Start()
    {
        _wheelController = ServiceLocator.GetService<WheelController>();
        _gameController = ServiceLocator.GetService<GameController>();
        
        _actorMove = GetComponent<ActorMove>();
        _actorView = GetComponent<ActorView>();
        
        _wheelController.OnWheelResult += StartJumps;
        _wheelController.OnGameOverSector += FailActor;
        _gameController.OnResetGame += ResetActor;

        _actorMove.OnTouchdown += _actorView.TouchDownEffect;
        _actorMove.OnJumpsSeriesEnd += SendJumpsComplette;
        
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    #region ItemsCallbacks
    public void TrapTrigger()
    {
        _actorMove.StopJumps();
        
        FailActor();
    }

    public void BombTrigger()
    {
        _actorMove.StopJumps();
        
        FailActor();
    }

    public void SpikeTrapTrigger()
    {
        _actorMove.StopJumps();
        
        FailActor();
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

    private void FailActor()
    {
        OnActorDeath?.Invoke();
        _actorView.PlayDeathEffect();
        _actorView.PlayBrokenHeartEffect();
    }

    private void ResetActor()//reset transform
    {
        _actorMove.ResetMoveData();
        
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        
        OnResetActor?.Invoke();
    }
    
    private void OnDestroy()
    {
        _wheelController.OnWheelResult -= StartJumps;
        _wheelController.OnGameOverSector -= FailActor;
        _gameController.OnResetGame -= ResetActor;
        
        _actorMove.OnTouchdown -= _actorView.TouchDownEffect;
        _actorMove.OnJumpsSeriesEnd -= SendJumpsComplette;
    }
    
}
