using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
   [SerializeField] private WheelController _wheelController;
   [SerializeField] private ActorController _actorController;
   
   private void Awake()
   {
      ServiceLocator.RegisterService(_wheelController);
      ServiceLocator.RegisterService(_actorController);
      
      DelayManager.Initialize(this);
   }

}
