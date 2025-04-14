using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
   [SerializeField] private WheelController _wheelController;
   private void Awake()
   {
      ServiceLocator.RegisterService(_wheelController);
      
      DelayManager.Initialize(this);
   }

}
