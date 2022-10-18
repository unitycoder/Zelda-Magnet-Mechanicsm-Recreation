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
    
        private Vector3 _targetPosition;
        private Vector3 _velocity;

        private IEnumerator _positionProgress;
        
        public async void AttachMagneticObject(IMagneticObject magneticObject)
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

            if (Input.GetKey(KeyCode.J))
            {
                UpdateTargetPosition(Vector3.left);
            }

            if (Input.GetKey(KeyCode.L))
            {
                UpdateTargetPosition(Vector3.right);
            }
            
            if (Input.GetKey(KeyCode.I))
            {
                UpdateTargetPosition(Vector3.up);
            }

            if (Input.GetKey(KeyCode.K))
            {
                UpdateTargetPosition(Vector3.down);
            }
        }

        private void UpdateTargetPosition(Vector3 delta)
        {
            _targetPosition += delta * (_movementSpeed * Time.deltaTime);
        }

        private UniTask AttachingToMagneticObject()
        {
            _targetPosition = _magneticObject.Transform.position;
            
            return UniTask.Delay((int) (_attachingDuration * 1000));
        }

        private void AttachedToMagneticObject()
        {
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