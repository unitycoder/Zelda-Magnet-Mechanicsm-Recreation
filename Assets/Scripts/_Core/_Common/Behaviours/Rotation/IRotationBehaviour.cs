using UnityEngine;

namespace Core.Common
{
    public interface IRotationBehaviour
    {
        Transform GetRotationBody();
        
        void Rotate(Vector3 eulerAngles, bool smoothUpdate = true);
        
        void LockRotation(bool value);
    }
}