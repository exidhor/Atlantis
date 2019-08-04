using UnityEngine;
using UnityEditor;
using ToolsEditor;

[CustomEditor(typeof(Monster)), CanEditMultipleObjects]
public class MonsterInspector : QTCircleInspector
{
}
