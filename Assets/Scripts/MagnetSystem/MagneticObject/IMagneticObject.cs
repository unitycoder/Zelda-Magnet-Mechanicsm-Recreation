using UnityEngine;

namespace MagnetSystem
{
    public interface IMagneticObject
    {
        Transform Transform { get; }

        void MoveMagneticObject(Vector3 deltaPos);
        
        void AttachingToMagnet(Magnet magnet);
        
        void AttachedToMagnet(Magnet magnet);

        void DetachedFromMagnet();

        Vector3 GetObjectVelocity();
    }
}