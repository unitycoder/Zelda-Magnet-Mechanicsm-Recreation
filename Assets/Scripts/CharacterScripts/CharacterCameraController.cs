using UnityEngine;

namespace CharacterSystem
{
    public class CharacterCameraController : MonoBehaviour
    {
        [SerializeField] private Transform _followTransform = null;

        [SerializeField] private Transform _characterTransform = null;
        [SerializeField] private Vector3 _offset = Vector3.up;
        
        [SerializeField] private float _minX = -20;
        [SerializeField] private float _maxX = 50;

        private float _rotationSpeed = 250;

        private Quaternion _nextRotation;
        private Vector2 _prevMousePos = Vector2.zero;

        private void Awake()
        {
            _prevMousePos = Input.mousePosition;
        }

        private void Update()
        {
            UpdateFollowPosition();
            
            Vector2 curMousePos = Input.mousePosition;

            Vector2 delta = curMousePos - _prevMousePos;

            if (delta.magnitude <= Mathf.Epsilon)
            {
                return;
            }

            delta = delta.normalized;

            Vector3 targetEuler = _followTransform.eulerAngles +
                                  new Vector3(delta.y, delta.x, 0) * (_rotationSpeed * Time.deltaTime);
            
            float angle = targetEuler.x % 360;
            
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

            targetEuler.x = angle;
            
            Quaternion newRotation = Quaternion.Euler(targetEuler);

            _followTransform.rotation = newRotation;

            _prevMousePos = curMousePos;
        }

        private void UpdateFollowPosition()
        {
            _followTransform.position = _characterTransform.position + _offset;
        }
    }
}