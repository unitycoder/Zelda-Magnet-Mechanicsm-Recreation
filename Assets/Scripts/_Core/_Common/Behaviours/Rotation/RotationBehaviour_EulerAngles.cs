using UnityEngine;

namespace Core.Common
{
    public class RotationBehaviour_EulerAngles : MonoBehaviour, IRotationBehaviour
    {
        [SerializeField] private Transform _rotationTransform = null;
        
        [SerializeField] private float _updateSpeed = 0;

        private bool _locked = false;

        public Transform GetRotationBody()
        {
            return _rotationTransform;
        }

        public void LockRotation(bool value)
        {
            _locked = value;
        }
        
        public void Rotate(Vector3 eulerAngles, bool smoothUpdate = false)
        {
            if (_locked)
            {
                return;
            }

            Quaternion targetQuaternion = Quaternion.Euler(eulerAngles);
            
            if (!smoothUpdate)
            {
                _rotationTransform.localRotation = targetQuaternion;
                
                return;
            }
            
            _rotationTransform.localRotation = Quaternion.Lerp(_rotationTransform.localRotation, targetQuaternion, _updateSpeed * Time.deltaTime);
        }
    }
}