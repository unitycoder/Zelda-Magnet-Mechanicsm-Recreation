using UnityEngine;
using System.Collections;

namespace MagnetSystem
{
    public class MagneticObject : MonoBehaviour, IMagneticObject
    {
        [SerializeField] private float _movementSpeed = 1;

        [SerializeField] private float _smoothTime = 0.5f;
        
        [SerializeField] private Rigidbody _rigidbody = null;
        
        public Transform Transform => transform;

        private Vector3 _velocity;
        private Vector3 _targetPosition = Vector3.zero;
        
        public void MoveMagneticObject(Vector3 deltaPos)
        {
            _targetPosition += deltaPos * (_movementSpeed * Time.deltaTime);
        }

        public void AttachingToMagnet(Magnet magnet)
        {
            _velocity = Vector3.zero;
        }

        public void AttachedToMagnet(Magnet magnet)
        {
            _rigidbody.useGravity = false;
            
            _targetPosition = transform.position;

            StartMovementRoutine();
        }

        public void DeattachedFromMagnet()
        {
            _rigidbody.useGravity = true;
            
            _rigidbody.AddForce(_velocity, ForceMode.VelocityChange);
            
            StopMovementRoutine();
        }

        private IEnumerator _movementRoutine;
        
        private void StartMovementRoutine()
        {
            StopMovementRoutine();

            _movementRoutine = MovementProgress();

            StartCoroutine(_movementRoutine);
        }

        private void StopMovementRoutine()
        {
            if (_movementRoutine != null)
            {
                StopCoroutine(_movementRoutine);
            }
        }
        
        private IEnumerator MovementProgress()
        {
            while (true)
            {
                Vector3 currentPosition = transform.position;

                transform.position = Vector3.SmoothDamp(currentPosition, _targetPosition, ref _velocity, _smoothTime);
                
                yield return null;
            }
        }
    }
}