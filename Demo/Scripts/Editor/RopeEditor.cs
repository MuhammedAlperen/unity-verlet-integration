using RopeSystem.Demo.Scripts.Runtime;
using UnityEditor;
using UnityEngine;

namespace RopeSystem.Demo.Scripts.Editor
{
    [CustomEditor(typeof(Rope))]
    public class RopeEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            var rope = (Rope) target;
            var serializedRope = new SerializedObject(rope);
            var pointListProperty = serializedRope.FindProperty("m_pointList");
            var stickListProperty = serializedRope.FindProperty("m_stickList");

            for (var i = 0; i < pointListProperty.arraySize; i++)
            {
                var pointProperty = pointListProperty.GetArrayElementAtIndex(i);
                var positionProperty = pointProperty.FindPropertyRelative("m_position");
                var isPinnedProperty = pointProperty.FindPropertyRelative("m_isPinned");
                
                Handles.color = isPinnedProperty.boolValue ? Color.red : Color.green;
                var position = positionProperty.vector3Value;
                Handles.Button(position, Quaternion.identity, .1f, 0, Handles.SphereHandleCap);

                if (isPinnedProperty.boolValue)
                {
                    EditorGUI.BeginChangeCheck();
                    var newPosition = Handles.DoPositionHandle(position, Quaternion.identity);
                    if (EditorGUI.EndChangeCheck()) positionProperty.vector3Value = newPosition;
                }
            }

            serializedRope.ApplyModifiedProperties();

            Handles.color = Color.white;
            for(var i = 0; i < stickListProperty.arraySize; i++)
            {
                var indexedRopeProperty = stickListProperty.GetArrayElementAtIndex(i);
                var startIndex = indexedRopeProperty.FindPropertyRelative("m_startPointIndex").intValue;
                var endIndex = indexedRopeProperty.FindPropertyRelative("m_endPointIndex").intValue;
                var stiffness = indexedRopeProperty.FindPropertyRelative("m_stiffness").floatValue;

                var startPointProperty = pointListProperty.GetArrayElementAtIndex(startIndex);
                var endPointProperty = pointListProperty.GetArrayElementAtIndex(endIndex);

                var startPosition = startPointProperty.FindPropertyRelative("m_position").vector3Value;
                var endPosition = endPointProperty.FindPropertyRelative("m_position").vector3Value;
                Handles.DrawLine(startPosition, endPosition, 8 * stiffness);
            }
        }
    }
}