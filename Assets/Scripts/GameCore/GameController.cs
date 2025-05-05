using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public event Action OnStartPlaying;
    public event Action OnGameOver;
    
    public event Action OnResetGame;
    public event Action OnExitToMenu;
    
    private ActorController _actorController;
    private WheelController _wheelController;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
        _actorController = ServiceLocator.GetService<ActorController>();
        _wheelController = ServiceLocator.GetService<WheelController>();
        
        _actorController.OnActorDeath += SendGameOver;
        _wheelController.OnGameOverSector += SendGameOver;
    }

    public void StartPlaying()
    {
        OnStartPlaying?.Invoke();
    }

    public void ExitToMenu()//go to menu from play scene
    {
        OnExitToMenu?.Invoke();
        
        DelayManager.DelayAction(HandleResetGame,1f);
    }

    private void HandleResetGame()
    {
        OnResetGame?.Invoke();
    }

    private void SendGameOver()
    {
        OnGameOver?.Invoke();
        
        DelayManager.DelayAction(HandleResetGame,3f);

        AudioController.instance.PlayGameOver();
    }
    
    private void OnDestroy()
    {
        _actorController.OnActorDeath -= SendGameOver;
        _wheelController.OnGameOverSector -= SendGameOver;
    }
}
