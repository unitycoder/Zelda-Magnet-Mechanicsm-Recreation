using Cinemachine;
using UnityEngine;

namespace Core.CameraSystem
{
    public class VirtualCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCameraBase _vcam = null;
        public string CameraName => _vcam.Name;

        public void SetPriority(int priority)
        {
            _vcam.Priority = priority;
        }
    }
}