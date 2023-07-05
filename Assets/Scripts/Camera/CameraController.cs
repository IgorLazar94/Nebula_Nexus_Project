using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class CameraController : MonoBehaviour
    {
        public Transform target;
        private Vector3 offset;

        private void Start()
        {
            offset = transform.position;
        }

        private void LateUpdate()
        {
            if (target != null)
            {
                Vector3 targetPosition = target.position + offset;
                transform.position = targetPosition;
            }
        }
    }

