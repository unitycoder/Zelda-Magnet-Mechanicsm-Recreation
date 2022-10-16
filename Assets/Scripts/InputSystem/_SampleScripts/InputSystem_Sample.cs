using UnityEngine;

namespace InputSystem
{
    public class InputSystem_Sample : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody = null;

        [SerializeField] private float _movementSpeed = 5f;
        
        [SerializeField] private float _jumpPower = 10f;
        
        private void Awake()
        {
            RegisterToInputManager();
        }

        private void OnDestroy()
        {
            UnregisterFromInputManager();
        }

        private void RegisterToInputManager()
        {
            InputManager.Instance.TryRegisterToInputListener<InputData_Axis>(EInputEvent.HorizontalAxis,
                OnHorizontalInputPerformed);
            
            InputManager.Instance.TryRegisterToInputListener<InputData_Key>(EInputEvent.KeyUp, OnKeyUp);
        }

        private void UnregisterFromInputManager()
        {
            if (InputManager.Instance == null)
            {
                return;
            }
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Axis>(EInputEvent.HorizontalAxis,
                OnHorizontalInputPerformed);
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>(EInputEvent.KeyUp, OnKeyUp);
        }

        private void OnHorizontalInputPerformed(InputData_Axis axisInputData)
        {
            _rigidbody.position += Vector3.right * (axisInputData.Amount * _movementSpeed * Time.deltaTime);
        }
        
        private void OnKeyUp(InputData_Key keyData)
        {
            if (keyData.KeyCode != KeyCode.Space)
            {
                return;
            }
            
            _rigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.VelocityChange);
        }
    }
}