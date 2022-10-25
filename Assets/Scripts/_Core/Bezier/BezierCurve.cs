using UnityEngine;
using System.Collections.Generic;

namespace Core.Bezier
{
    [ExecuteAlways]
    public class BezierCurve : MonoBehaviour
    {
        [SerializeField] private int _resolution = 5;

        private Vector3[] _points = null;
        public IReadOnlyList<Vector3> Points => _points;

        private Vector3[] _lineSegments = null;
        public IReadOnlyList<Vector3> LineSegments => _lineSegments;

#if UNITY_EDITOR
        public void InitBezierPoints()
        {
            int pointCount = 4;

            _points = new Vector3[pointCount];

            Transform _t = transform;
            
            for (int i = 0; i < pointCount; i++)
            {
                _points[i] = _t.position + (i + 1) * _t.forward;
            }
        }
        
        public void UpdatePointPosition(int pointIndex, Vector3 position)
        {
            _points[pointIndex] = position;
        }
#endif

        public void CalculateLine(Vector3[] points)
        {
            int n = points.Length;
            
            if (_points == null)
            {
                _points = new Vector3[n];
            }

            for (int i = 0; i < n; i++)
            {
                _points[i] = points[i];
            }
            
            UpdateLineSegments();
        }

        private void UpdateLineSegments()
        {
            if (_points == null)
            {
                return;
            }
            
            _lineSegments = new Vector3[_resolution + 1];

            float timeInterval = 1f / _resolution;
            
            for (int i = 0; i < _resolution + 1; i++)
            {
                _lineSegments[i] = CalculatePoint(i * timeInterval);
            }
        }

        private Vector3 CalculatePoint(float t)
        {
            return
                Mathf.Pow(1 - t, 3) * _points[0] +
                Mathf.Pow(1 - t, 2) * t * 3 * _points[1] +
                Mathf.Pow(t, 2) * (1 - t) * 3 * _points[2] +
                Mathf.Pow(t, 3) * _points[3];
        }
    }
}