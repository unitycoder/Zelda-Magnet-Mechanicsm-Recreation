using UnityEngine;

namespace Common
{
    public class MoveBehaviour_CharacterController : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController = null;

        [SerializeField] private float _speed = 10;
        
        public void Move(Vector3 direction)
        {
            _characterController.Move(direction * (_speed * Time.deltaTime));
        }
    }
}