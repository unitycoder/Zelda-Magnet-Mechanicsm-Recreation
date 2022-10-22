using System;
using Core.Common;
using UnityEngine;

namespace CharacterSystem
{
    public class CharacterCameraController : MonoBehaviour
    {
        [SerializeField] private RotationBehaviour_EulerAngles _rotationBehaviour = null;
        
        [SerializeField] private Transform _followTransform = null;

        [SerializeField] private Transform _characterTransform = null;
        
        [SerializeField] private Vector3 _offset = Vector3.up;
        
        [SerializeField] private float _minX = -20;
        
        [SerializeField] private float _maxX = 50;

        [SerializeField] private float _rotationSpeed = 250;
        
        private bool _rotateCharacter = false; 
        private Vector3 _targetEuler;
        private Quaternion _nextRotation;
        
        public Action<Vector2> OnMouseInputPerformed { get; set; }
        
        public void EnableCharacterRotation(bool enabled)
        {
            _rotateCharacter = enabled;
        }
        
        private void Awake()
        {
            _targetEuler = _followTransform.rotation.eulerAngles;
        }

        private void Update()
        {
            UpdateFollowPosition();

            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            
            Vector2 delta = new Vector2(x, y);

            delta = delta.normalized;

            _targetEuler += new Vector3(delta.y, delta.x, 0);
            _targetEuler.z = 0;
            
            float angle = _targetEuler.x % 360;

            if (angle < 0)
            {
                angle += 360;
            }
            
            if (angle > 180 && angle < 360 + _minX)
            {
                angle = _minX + 360;
            }
            else if (angle <= 180 && angle >= _maxX)
            {
                angle = _maxX;
            }

            _targetEuler.x = angle;
            
            Quaternion newRotation = Quaternion.Euler(_targetEuler);

            _followTransform.rotation = Quaternion.SlerpUnclamped(_followTransform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
            
            if (_rotateCharacter)
            {
                Vector3 characterTarget = _targetEuler;
                characterTarget.x = 0;
                
                _rotationBehaviour.Rotate(characterTarget);
            }
        }

        private void UpdateFollowPosition()
        {
            _followTransform.position = _characterTransform.position + _offset;
        }
    }
}