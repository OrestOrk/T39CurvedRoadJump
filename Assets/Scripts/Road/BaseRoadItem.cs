using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRoadItem : MonoBehaviour
{
    protected ActorController _actorController;

    protected void Start()
    {
        _actorController = ServiceLocator.GetService<ActorController>();

        if (_actorController == null)
        {
            Debug.Log("Error: ActorController is null");
        }
    }

    public abstract void Activate();
}
