using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAnimation : MonoBehaviour
{
    [SerializeField] private Animator _actorAnimator;
    [SerializeField] private ActorMove _actorMove;

    private void Start()
    {
        _actorMove.OnJumpStart += PlayJumpAnimation;
    }
    private void PlayJumpAnimation()
    {
        _actorAnimator.SetTrigger("Jump");
    }

    private void OnDestroy()
    {
        _actorMove.OnJumpStart -= PlayJumpAnimation;
    }
}
