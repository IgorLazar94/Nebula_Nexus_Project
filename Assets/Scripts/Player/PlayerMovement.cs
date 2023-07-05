using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private float speed;
        private Rigidbody rb;
        private bool isReadyToMove = true;

        public static System.Action<float> onPlayerStopped;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            speed = GameSettings.Instance.GetPlayerSpeed();
        }

        private void OnEnable()
        {
            onPlayerStopped += EnableStopPlayer;

            UI.JoyStickInput.isHasInputDirection += PlayerMove;
            UI.JoyStickInput.isNotHasInputDirection += ResetSpeed;
        }

        private void OnDisable()
        {
            onPlayerStopped -= EnableStopPlayer;

            UI.JoyStickInput.isHasInputDirection -= PlayerMove;
            UI.JoyStickInput.isNotHasInputDirection -= ResetSpeed;
        }

        private void PlayerMove(Vector3 _inputDirection)
        {
            if (!isReadyToMove) return;

            Vector3 playerDirection = new Vector3(_inputDirection.x, 0f, _inputDirection.y);
            rb.velocity = playerDirection * speed;
            PlayerLookForward(playerDirection);
        }

        private void PlayerLookForward(Vector3 _playerDirection)
        {
            Vector3 lookAtPosition = transform.position + _playerDirection;
            transform.LookAt(lookAtPosition);
        }

        private void ResetSpeed()
        {
            rb.velocity = Vector3.zero;
        }

        private void EnableStopPlayer(float time)
        {
            //StartCoroutine(StopPlayer(time));
        }

        private IEnumerator StopPlayer(float _time)
        {
            isReadyToMove = false;
            ResetSpeed();
            yield return new WaitForSeconds(_time);
            isReadyToMove = true;
        }
    }
}

