using UnityEngine;

namespace Core.Common
{
    public class RotationBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _rotationTransform = null;

        [SerializeField] private bool _smoothUpdate = false;

        [SerializeField] private float _updateSpeed = 0;
        
        public void Rotate(Vector3 lookDir)
        {
            if (lookDir.magnitude == 0)
            {
                return;
            }
            
            Quaternion targetRot = Quaternion.LookRotation(lookDir);
            
            if (!_smoothUpdate)
            {
                _rotationTransform.localRotation = targetRot;
                
                return;
            }
            
            _rotationTransform.localRotation = Quaternion.Lerp(_rotationTransform.localRotation, targetRot, _updateSpeed * Time.deltaTime);
        }
    }
}