using UnityEngine;
using System.Collections;
using Cysharp.Threading.Tasks;

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

        private Vector3 _previousMagnetPosition;
        private Quaternion _previousMagnetRotation;
        
        private IEnumerator _positionProgress;
        
        public async UniTask AttachMagneticObject(IMagneticObject magneticObject)
        {
            DeattachMagneticObject();

            _magneticObject = magneticObject;

            await AttachingToMagneticObject();

            AttachedToMagneticObject();
        }

        public void DeattachMagneticObject()
        {
            _magneticObject = null;
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

            /*
            Vector3 deltaAngles = transform.rotation.eulerAngles - _previousMagnetRotation.eulerAngles;
            Vector3 deltaMagPos = _magneticObject.Transform.position - transform.position;
            Vector3 newDelta = Quaternion.Inverse(Quaternion.Euler(deltaAngles)) * deltaMagPos;
            _targetPosition += newDelta;
            */
        }

        private void UpdateTargetPositionDeltaSpeed(Vector3 delta)
        {
            _targetPosition += delta * (_movementSpeed * Time.deltaTime);
        }

        private void ResetMagnetProperties()
        {
            _previousMagnetPosition = transform.position;
            _previousMagnetRotation = transform.rotation;
        }

        private UniTask AttachingToMagneticObject()
        {
            _targetPosition = _magneticObject.Transform.position;

            return UniTask.Delay((int) (_attachingDuration * 1000));
        }

        private void AttachedToMagneticObject()
        {
            ResetMagnetProperties();
            
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