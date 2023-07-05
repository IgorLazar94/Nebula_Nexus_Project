using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationFXController : MonoBehaviour
    {
        private Animator animator;

        private void OnEnable()
        {
            UI.JoyStickInput.isHasInputDirection += SetPlayerMove;
            UI.JoyStickInput.isNotHasInputDirection += SetPlayerStop;
        }

        private void OnDisable()
        {
            UI.JoyStickInput.isHasInputDirection -= SetPlayerMove;
            UI.JoyStickInput.isNotHasInputDirection -= SetPlayerStop;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void SetPlayerMove(Vector3 direction)
        {
            // set particles in direction
            animator.SetBool(AnimationParameters.isMove, true);
        }

        private void SetPlayerStop()
        {
            animator.SetBool(AnimationParameters.isMove, false);
        }
    }
}

