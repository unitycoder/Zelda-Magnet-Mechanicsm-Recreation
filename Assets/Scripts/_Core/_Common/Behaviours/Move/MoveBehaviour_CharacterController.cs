using UnityEngine;

namespace Core.Common
{
    public class MoveBehaviour_CharacterController : MonoBehaviour, IMovementBehaviour
    {
        [SerializeField] private CharacterController _characterController = null;

        [SerializeField] private float _speed = 10;

        [SerializeField] private bool _applyGravity = false;
        
        public void Move(Vector3 direction)
        {
            _characterController.Move(direction * (_speed * Time.deltaTime));
        }

        private void FixedUpdate()
        {
            if (_applyGravity)
            {
                _characterController.Move(Physics.gravity * Time.fixedDeltaTime);
            }
        }
    }
}