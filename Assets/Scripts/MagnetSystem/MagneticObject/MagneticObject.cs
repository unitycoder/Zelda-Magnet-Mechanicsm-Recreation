using System;
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
        
        public Action OnAttaching { get; set; }
        public Action OnAttached { get; set; }
        public Action OnDetached { get; set; }
        
        public void MoveMagneticObject(Vector3 deltaPos)
        {
            if (deltaPos.magnitude < Mathf.Epsilon)
            {
                return;
            }
            
            _targetPosition += deltaPos * (_movementSpeed * Time.deltaTime);
        }

        public void AttachingToMagnet(Magnet magnet)
        {
            _velocity = Vector3.zero;
            
            OnAttaching?.Invoke();
        }

        public void AttachedToMagnet(Magnet magnet)
        {
            _rigidbody.useGravity = false;
            
            _targetPosition = transform.position;

            StartMovementRoutine();
            
            OnAttached?.Invoke();            
        }

        public void DetachedFromMagnet()
        {
            _rigidbody.useGravity = true;
            
            _rigidbody.AddForce(_velocity, ForceMode.VelocityChange);
            
            StopMovementRoutine();
            
            OnDetached?.Invoke();
        }

        public Vector3 GetObjectVelocity()
        {
            return _velocity;
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