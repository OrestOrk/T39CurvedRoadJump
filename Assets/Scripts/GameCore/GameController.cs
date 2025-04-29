using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public event Action OnStartPlaying;
    public event Action OnGameOver;
    
    public event Action OnResetGame;
    
    private ActorController _actorController;
    private WheelController _wheelController;

    private void Start()
    {
        _actorController = ServiceLocator.GetService<ActorController>();
        _wheelController = ServiceLocator.GetService<WheelController>();
        
        _actorController.OnActorDeath += SendGameOver;
        _wheelController.OnGameOverSector += SendGameOver;
    }

    public void StartPlaying()
    {
        OnStartPlaying?.Invoke();
    }

    private void HandleResetGame()
    {
        OnResetGame?.Invoke();
    }

    private void SendGameOver()
    {
        OnGameOver?.Invoke();
        
        DelayManager.DelayAction(HandleResetGame,3f);
    }

    private void OnDestroy()
    {
        _actorController.OnActorDeath -= SendGameOver;
        _wheelController.OnGameOverSector -= SendGameOver;
    }
}
