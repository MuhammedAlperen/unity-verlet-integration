using RopeSystem.Demo.Scripts.Runtime;
using UnityEditor;
using UnityEngine;

namespace RopeSystem.Demo.Scripts.Editor
{
    [CustomEditor(typeof(VerletDemo), true)]
    public class VerletDemoEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            var rope = (VerletDemo) target;
            var serializedRope = new SerializedObject(rope);
            var pointListProperty = serializedRope.FindProperty("m_particleList");

            for (var i = 0; i < pointListProperty.arraySize; i++)
            {
                var pointProperty = pointListProperty.GetArrayElementAtIndex(i);
                var positionProperty = pointProperty.FindPropertyRelative("m_position");
                var isPinnedProperty = pointProperty.FindPropertyRelative("m_isPinned");
                
                var position = positionProperty.vector3Value;

                if (!isPinnedProperty.boolValue) continue;
                
                EditorGUI.BeginChangeCheck();
                var newPosition = Handles.DoPositionHandle(position, Quaternion.identity);
                if (EditorGUI.EndChangeCheck()) positionProperty.vector3Value = newPosition;
            }

            serializedRope.ApplyModifiedProperties();
        }
    }
}