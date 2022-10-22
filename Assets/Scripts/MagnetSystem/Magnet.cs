using System;
using UnityEngine;
using System.Collections;
using Core.CameraSystem;
using Core.ServiceSystem;
using Cysharp.Threading.Tasks;
using Util;

namespace MagnetSystem
{
    public class Magnet : MonoBehaviour
    {
        [SerializeField] private float _attachingDuration = 0.25f;

        [SerializeField] private float _movementSpeed = 1;
        
        [SerializeField] private float _smoothTime = 0.25f;
        
        private IMagneticObject _magneticObject;
    
        private Vector3 _velocity;
        private Vector3 _targetPosition;

        private float _distanceToCamera;
        
        private IEnumerator _positionProgress;

        private CameraManager _cameraManager;
        
        public async UniTask AttachMagneticObject(IMagneticObject magneticObject)
        {
            DeattachMagneticObject();

            await AttachingToMagneticObject();

            _magneticObject = magneticObject;
            
            AttachedToMagneticObject();
        }

        public void DeattachMagneticObject()
        {
            _magneticObject = null;
        }

        private void Awake()
        {
            _cameraManager = ServiceProvider.Get<CameraManager>();
        }

        private void Update()
        {
            if (_magneticObject == null)
            {
                return;
            }

            #region Input Related

            if (Input.GetKey(KeyCode.J))
            {
                UpdateTargetPositionDeltaSpeed(-transform.right);
            }

            if (Input.GetKey(KeyCode.L))
            {
                UpdateTargetPositionDeltaSpeed(transform.right);
            }
            
            if (Input.GetKey(KeyCode.I))
            {
                UpdateTargetPositionDeltaSpeed(transform.up);
            }

            if (Input.GetKey(KeyCode.K))
            {
                UpdateTargetPositionDeltaSpeed(-transform.up);
            }

            #endregion

            UpdateMagneticObjectPosition();
        }

        private void UpdateMagneticObjectPosition()
        {
            Vector3 origin = _cameraManager.MainCamera.ViewportToWorldPoint(Constants.AimPosition);

            Ray ray = _cameraManager.MainCamera.ViewportPointToRay(Constants.AimPosition);
            
            Vector3 targetPos = origin + _distanceToCamera * ray.direction.normalized;

            Vector3 deltaPos = targetPos - _magneticObject.Transform.position;
            
            UpdateTargetPositionDeltaSpeed(deltaPos);
        }
        
        private void UpdateTargetPositionDeltaSpeed(Vector3 delta)
        {
            _targetPosition += delta * (_movementSpeed * Time.deltaTime);
        }

        private UniTask AttachingToMagneticObject()
        {
            return UniTask.Delay(TimeSpan.FromSeconds(_attachingDuration));
        }

        private void AttachedToMagneticObject()
        {
            Vector3 objectPosition = _magneticObject.Transform.position;
            _distanceToCamera = (objectPosition - _cameraManager.MainCamera.ViewportToWorldPoint(Constants.AimPosition)).magnitude;
            _targetPosition = objectPosition;

            _positionProgress = UpdatePositionProgress();

            StartCoroutine(_positionProgress); 
        }

        private IEnumerator UpdatePositionProgress()
        {
            while (true)
            {
                Vector3 currentPosition = _magneticObject.Transform.position;

                _magneticObject.Transform.position =
                    Vector3.SmoothDamp(currentPosition, _targetPosition, ref _velocity, _smoothTime);
                
                yield return null;
            }
        }
    }
}