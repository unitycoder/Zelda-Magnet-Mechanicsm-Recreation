using UnityEngine;

namespace MagnetSystem
{
    public class MagneticObject : MonoBehaviour, IMagneticObject
    {
        public Transform Transform => transform;
    }
}