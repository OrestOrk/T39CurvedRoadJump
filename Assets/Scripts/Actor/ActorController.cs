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

    private void StartJumps(int jumpsAmount)
    {
        _actorMove.StartJumps(jumpsAmount);
    }

    private void OnDestroy()
    {
        _wheelController.OnWheelResult -= StartJumps;
    }
}
