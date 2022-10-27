using Core.Bezier;
using DG.Tweening;
using UnityEngine;

namespace MagnetSystem
{
    public class MagnetLineController : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer = null;

        [SerializeField] private BezierCurve _bezierCurve = null;
        
        [SerializeField] private float _speedCoefficient = 2;
        
        [SerializeField] [Range(0, 1)] private float _point1NormPos = 0;
        
        [SerializeField] [Range(0, 1)] private float _point2NormPos = 0;

        [SerializeField] [Range(0, 1)] private float _point3NormPos = 0;
        
        private Vector3[] _magneticLine = new Vector3[4];
        
        private Transform _magnetTransform;
        private IMagneticObject _magneticObject;
        
        public void EnableLineController(Transform magnetTransform, IMagneticObject magneticObject)
        {
            _lineRenderer.enabled = true;
            
            _magnetTransform = magnetTransform;
            _magneticObject = magneticObject;

            AnimateMagneticLineToObject();
        }

        public void DisableLineController()
        {
            _lineRenderer.enabled = false;

            _magnetTransform = null;
            _magneticObject = null;
        }

        public void UpdateLine()
        {
            CalculateMagneticLine(_magneticObject.Transform.position, _magneticObject.GetObjectVelocity());
        }

        private void AnimateMagneticLineToObject()
        {
            Vector3 dir = _magneticObject.Transform.position - _magnetTransform.position;
            
            DOVirtual.Float(0, 1f, 0.25f, 
                (value) =>
            {
                CalculateMagneticLine(_magnetTransform.position + value * dir, Vector3.zero);
            });
        }

        private void CalculateMagneticLine(Vector3 endPosition, Vector3 velocity)
        {
            _magneticLine[0] = _magnetTransform.position;
            _magneticLine[3] = _magneticLine[0] + (endPosition - _magneticLine[0]) * _point3NormPos;

            Vector3 dir = _magneticLine[3] - _magneticLine[0];

            _magneticLine[1] = _magneticLine[0] + dir * _point1NormPos;

            _magneticLine[2] = _magneticLine[0] + dir * _point2NormPos;
            _magneticLine[2] += _speedCoefficient * velocity;
            
            _bezierCurve.CalculateLine(_magneticLine);

            int segmentCount = _bezierCurve.LineSegments.Count;
            
            _lineRenderer.positionCount = segmentCount;
            
            for (int i = 0; i < segmentCount; i++)
            {
                _lineRenderer.SetPosition(i, _bezierCurve.LineSegments[i]);
            }
        }
    }
}