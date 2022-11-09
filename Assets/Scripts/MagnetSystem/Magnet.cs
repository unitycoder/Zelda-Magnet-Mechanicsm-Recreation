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

        [SerializeField] private MagnetLineController _lineController = null;

        [SerializeField] private ParticleSystem _attachedVFX = null;

        private float _distanceToCamera;

        private IMagneticObject _magneticObject;
        
        private IEnumerator _positionProgress;

        private CameraManager _cameraManager;
        
        public async UniTask AttachMagneticObject(IMagneticObject magneticObject)
        {
            DeattachMagneticObject();

            await AttachingToMagneticObject(magneticObject);

            _magneticObject = magneticObject;

            AttachedToMagneticObject();
        }

        public void DeattachMagneticObject()
        {
            if (_magneticObject != null)
            {
                _magneticObject.DetachedFromMagnet();
            }

            _lineController.DisableLineController();
            
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
                UpdateMagneticObjectPosition(-transform.right);
            }

            if (Input.GetKey(KeyCode.L))
            {
                UpdateMagneticObjectPosition(transform.right);
            }
            
            if (Input.GetKey(KeyCode.I))
            {
                UpdateMagneticObjectPosition(transform.up);
            }

            if (Input.GetKey(KeyCode.K))
            {
                UpdateMagneticObjectPosition(-transform.up);
            }

            #endregion

            UpdateMagneticObjectPosition();

            _lineController.UpdateLine();
        }

        private void UpdateMagneticObjectPosition()
        {
            Vector3 origin = _cameraManager.MainCamera.ViewportToWorldPoint(Constants.AimPosition);

            Ray ray = _cameraManager.MainCamera.ViewportPointToRay(Constants.AimPosition);
            
            Vector3 targetPos = origin + _distanceToCamera * ray.direction.normalized;

            Vector3 deltaPos = targetPos - _magneticObject.Transform.position;
            
            UpdateMagneticObjectPosition(deltaPos);
        }
        
        private void UpdateMagneticObjectPosition(Vector3 delta)
        {
            _magneticObject.MoveMagneticObject(delta);
        }

        private UniTask AttachingToMagneticObject(IMagneticObject magneticObject)
        {
            magneticObject.AttachingToMagnet(this);
            
            _lineController.EnableLineController(transform, magneticObject);
            
            return UniTask.Delay(TimeSpan.FromSeconds(_attachingDuration));
        }

        private void AttachedToMagneticObject()
        {
            Vector3 objectPosition = _magneticObject.Transform.position;
            
            _attachedVFX.transform.position = objectPosition;
            _attachedVFX.Play();
            
            _distanceToCamera = (objectPosition - _cameraManager.MainCamera.ViewportToWorldPoint(Constants.AimPosition)).magnitude;
            
            _magneticObject.AttachedToMagnet(this);
        }
    }
}