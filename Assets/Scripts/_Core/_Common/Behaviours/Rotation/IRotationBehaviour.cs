using UnityEngine;

namespace Core.Common
{
    public interface IRotationBehaviour
    {
        Transform GetRotationBody();
        
        void Rotate(Vector3 lookDir, bool smoothUpdate = true);
        
        void LockRotation(bool value);
    }
}