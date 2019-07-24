using UnityEditor;
using UnityEngine;

namespace ToolsEditor
{
    [CustomEditor(typeof(Tools.QTCircle)), CanEditMultipleObjects]
    public class QTCircleInspector : Editor
    {
        readonly float offsetY = 0f;

        Tool LastTool = Tool.None;

        void OnEnable()
        {
            LastTool = UnityEditor.Tools.current;
        }

        void OnDisable()
        {
            UnityEditor.Tools.current = LastTool;
        }


        private void OnSceneGUI()
        {
            Tools.QTCircle circle = target as Tools.QTCircle;

            //Transform handleTransform = circle.transform;
            //Quaternion handleRotation = handleTransform.rotation;
            //Vector3 p0 = handleTransform.TransformPoint(circle.p0);
            //Vector3 p1 = handleTransform.TransformPoint(circle.p1);

            //Handles.color = Color.white;
            //Handles.DrawLine(p0, p1);
            //Handles. DoPositionHandle(p0, handleRotation);
            //Handles.DoPositionHandle(p1, handleRotation);

            Handles.color = Color.green;

            Vector3 center = Tools.WorldConversion.ToVector3(circle.center);

            Handles.DrawWireDisc(center,
                                 Vector3.up, 
                                 circle.radius);

            Handles.color = Color.white;

            if(!circle.edit)
            {
                UnityEditor.Tools.current = LastTool;
                return;
            }

            UnityEditor.Tools.current = Tool.None;

            EditorGUI.BeginChangeCheck();
            float radius = Handles.RadiusHandle(Quaternion.identity,
                                                center,
                                                circle.radius,
                                                true);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(circle, "Change Radius Circle Collider");
                circle.SetRadius(radius);
            }

            EditorGUI.BeginChangeCheck();
            Vector3 newCenter = Handles.PositionHandle(center + new Vector3(0f, offsetY, 0f), 
                                                       Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(circle, "Change Offset Circle Collider");
                Vector2 offset = Tools.WorldConversion.ToVector2(newCenter 
                                                                 - circle.transform.position
                                                                 - new Vector3(0f, offsetY, 0f));
                circle.SetOffset(offset);
            }
        }
    }
}