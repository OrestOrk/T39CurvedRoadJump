using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
   [SerializeField] private WheelController _wheelController;
   [SerializeField] private ActorController _actorController;
   [SerializeField] private CoinsBalanceController _coinsBalanceController;
   [SerializeField] private TransitionScreenController _transitionScreenController;
   [SerializeField] private CharacterSelectionController _characterSelectionController;
   [SerializeField] private GameController _gameController;
   [SerializeField] private FailScreenController _failScreenController;
   
   [Space(10)]
   [Header("ScriptableObject")]
   [SerializeField] private ShopProductDatabase _shopProductDatabase;

   
   private void Awake()
   {
      ServiceLocator.RegisterService(_wheelController);
      ServiceLocator.RegisterService(_actorController);
      ServiceLocator.RegisterService(_coinsBalanceController);
      ServiceLocator.RegisterService(_transitionScreenController);
      ServiceLocator.RegisterService(_characterSelectionController);
      ServiceLocator.RegisterService(_gameController);
      ServiceLocator.RegisterService(_failScreenController);
      
      ServiceLocator.RegisterService(_shopProductDatabase);
      
      DelayManager.Initialize(this);
   }

}
