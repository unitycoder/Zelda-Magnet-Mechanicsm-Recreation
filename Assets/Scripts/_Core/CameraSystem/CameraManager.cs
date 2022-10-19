using Cinemachine;
using UnityEngine;
using Core.ServiceSystem;

namespace Core.CameraSystem
{
    public class CameraManager : MonoBehaviour, IService
    {
        [SerializeField] private VirtualCamera[] _cameras = null;

        private CinemachineVirtualCameraBase _vcam;
        
        private const int BACKGROUND_PRIORITY = 5;
        private const int FOREGROUND_PRIORITY = 15;
        
        public void SetTransition(string targetCamera)
        {
            VirtualCamera targetVCam = null;
            
            foreach (var virtualCamera in _cameras)
            {
                if (virtualCamera.CameraName.Equals(targetCamera))
                {
                    targetVCam = virtualCamera;
                }
            }

            if (targetVCam == null)
            {
                return;
            }

            foreach (VirtualCamera virtualCamera in _cameras)
            {
                int priority = virtualCamera == targetVCam ? FOREGROUND_PRIORITY : BACKGROUND_PRIORITY;
                
                virtualCamera.SetPriority(priority);
            }
        }
    }
}