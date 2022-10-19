using UnityEngine;

namespace Core.Common
{
    public class RotationBehaviour : MonoBehaviour, IRotationBehaviour
    {
        [SerializeField] private Transform _rotationTransform = null;
        
        [SerializeField] private float _updateSpeed = 0;

        private bool _locked = false;

        public Transform GetRotationBody() => _rotationTransform;

        public void Rotate(Vector3 lookDir, bool smoothUpdate = true)
        {
            if (_locked)
            {
                return;
            }
            
            if (lookDir.magnitude == 0)
            {
                return;
            }
            
            Quaternion targetRot = Quaternion.LookRotation(lookDir, Vector3.up);
            
            if (!smoothUpdate)
            {
                _rotationTransform.localRotation = targetRot;
                
                return;
            }
            
            _rotationTransform.localRotation = Quaternion.Lerp(_rotationTransform.localRotation, targetRot, _updateSpeed * Time.deltaTime);
        }

        public void LockRotation(bool value)
        {
            _locked = value;
        }
    }
}