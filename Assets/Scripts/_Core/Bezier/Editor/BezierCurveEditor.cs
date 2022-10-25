using UnityEditor;
using UnityEngine;
using Unity.Mathematics;
using System.Collections.Generic;

namespace Core.Bezier
{
    [CustomEditor(typeof(BezierCurve))]
    public class BezierCurveEditor : Editor
    {
        private Vector3[] _positions = null;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            BezierCurve curve = (BezierCurve) target;
            
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button("Initialize Curve"))
            {
                curve.InitBezierPoints();
            }
            
            GUILayout.FlexibleSpace();

            if (curve.Points == null)
            {
                return;
            }
            
            for (int i = 0; i < curve.Points.Count; i++)
            {
                EditorGUILayout.Vector3Field($"Point {i}", curve.Points[i]);
            }
        }

        private void OnSceneGUI()
        {
            BezierCurve curve = (BezierCurve) target;

            InitPoints(curve, curve.Points);
            
            DrawLineSegments(curve.LineSegments);
        }

        private void InitPoints(BezierCurve curve, IReadOnlyList<Vector3> points)
        {
            if (points == null)
            {
                return;
            }

            _positions = new Vector3[points.Count];

            for (int i = 0; i < points.Count; i++)
            {
                Handles.color = new Color(0, 1, 1, 0.5f);
                
                // _positions[i] = Handles.PositionHandle(points[i], quaternion.identity);

                // EditorGUI.BeginChangeCheck();
                
                _positions[i] = Handles
                    .FreeMoveHandle(
                        points[i],
                        quaternion.identity, 0.25f, new Vector3(0, 0, 0), Handles.SphereHandleCap);


                curve.UpdatePointPosition(i, _positions[i]);
            }
        }

        private void DrawLineSegments(IReadOnlyList<Vector3> lineSegments)
        {
            if (lineSegments == null)
            {
                return;
            }
            
            Handles.color = new Color(1, 0, 0, 1);
            
            for (int i = 0; i < lineSegments.Count - 1; i++)
            {
                Handles.DrawLine(lineSegments[i], lineSegments[i + 1]);
            }
        }
    }
}