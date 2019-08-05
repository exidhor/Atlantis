using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityAI;

namespace UnityAIEditor
{
    [CustomEditor(typeof(PatrolPath)), CanEditMultipleObjects]
    public class PatrolPathInspector : Editor
    {
        readonly float offsetY = 0f;

        //Tool LastTool = Tool.None;

        protected void OnEnable()
        {
            SceneView.onSceneGUIDelegate += OnCustomSceneGUI;
        }

        protected void OnDisable()
        {
            SceneView.onSceneGUIDelegate -= OnCustomSceneGUI;
        }

        protected void OnCustomSceneGUI(SceneView sceneView)
        {
            PatrolPath patrol = target as PatrolPath;

            Handles.color = Color.green;

            for (int i = 0; i < patrol.points.Count; i++)
            {
                if(i < patrol.points.Count - 1)
                {
                    Handles.DrawLine(patrol.points[i], patrol.points[i + 1]);
                }
                else
                {
                    if(patrol.cycle)
                    {
                        Handles.DrawLine(patrol.points[i], patrol.points[0]);
                    }
                }

                Vector3 newCenter = Handles.PositionHandle(patrol.points[i],
                                                           Quaternion.identity);

                patrol.points[i] = new Vector3(newCenter.x, 0f, newCenter.z);
            }

            //Vector3 center = Tools.WorldConversion.ToVector3(circle.center);

            //Handles.DrawWireDisc(center,
            //                     Vector3.up,
            //                     circle.radius);

            //Handles.color = Color.white;

            //if (!circle.edit)
            //{
            //    UnityEditor.Tools.current = LastTool;
            //    return;
            //}

            //UnityEditor.Tools.current = Tool.None;

            //EditorGUI.BeginChangeCheck();
            //float radius = Handles.RadiusHandle(Quaternion.identity,
            //                                    center,
            //                                    circle.radius,
            //                                    true);
            //if (EditorGUI.EndChangeCheck())
            //{
            //    Undo.RecordObject(circle, "Change Radius Circle Collider");
            //    circle.SetRadius(radius);
            //}

            //EditorGUI.BeginChangeCheck();
            //Vector3 newCenter = Handles.PositionHandle(center + new Vector3(0f, offsetY, 0f),
            //                                           Quaternion.identity);
            //if (EditorGUI.EndChangeCheck())
            //{
            //    Undo.RecordObject(circle, "Change Offset Circle Collider");
            //    Vector2 offset = Tools.WorldConversion.ToVector2(newCenter
            //                                                     - circle.transform.position
            //                                                     - new Vector3(0f, offsetY, 0f));
            //    circle.SetOffset(offset);
            //}
        }

    }
}