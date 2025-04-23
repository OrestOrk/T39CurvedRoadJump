using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAnimation : MonoBehaviour
{
    [SerializeField] private Animator[] _actorAnimators;
    [SerializeField] private ActorMove _actorMove;
    
    private ActorController _actorController;
    
    private CharacterSelectionController _characterSelectionController;
    private void Start()
    {
        _actorController = GetComponent<ActorController>();
        _characterSelectionController = ServiceLocator.GetService<CharacterSelectionController>();
        
        _actorMove.OnJumpStart += PlayJumpAnimation;
        _actorController.OnActorDeath += PlayDeathAnimation;
        _actorController.OnResetActor += PlayIdleAnimation;
    }
    private void PlayJumpAnimation()
    {
        _actorAnimators[GetActiveChar()].SetTrigger("Jump");
    }

    private void PlayDeathAnimation()
    {
        _actorAnimators[GetActiveChar()].SetTrigger("Lose");
    }

    private void PlayIdleAnimation()
    {
        _actorAnimators[GetActiveChar()].SetTrigger("Idle");
    }

    private int GetActiveChar()
    {
        return _characterSelectionController.selectedCharacterID - 1;
    }
    private void OnDestroy()
    {
        _actorMove.OnJumpStart -= PlayJumpAnimation;
        _actorController.OnActorDeath -= PlayDeathAnimation;
        _actorController.OnResetActor -= PlayIdleAnimation;
    }
}
