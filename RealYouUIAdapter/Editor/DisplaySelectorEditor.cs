using UnityEditor;
using UnityEngine;

namespace RealYou.Unity.UIAdapter
{
    [CustomEditor(typeof(DisplaySelector))]
    public class DisplaySelectorEditor:Editor
    {
        private SerializedProperty _gameobjects;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (_gameobjects != null && _gameobjects.arraySize > 0)
            {
                
                serializedObject.Update();

                SerializedProperty displayInfo;
                SerializedProperty go;
                for (int i = ScreenRatioConst.Min; i <= ScreenRatioConst.Max; i++)
                {
                    displayInfo = _gameobjects.GetArrayElementAtIndex(i);
                    go = displayInfo.FindPropertyRelative("GameObject");
                    EditorGUILayout.PropertyField(go,new GUIContent(((ScreenRatio)i).ToString()));
                }

                serializedObject.ApplyModifiedProperties();
            }

        }

        private void OnEnable()
        {
            serializedObject.Update();
            
            _gameobjects = serializedObject.FindProperty("_gameobjects");
            SerializedProperty displayInfo;
            if (_gameobjects.arraySize == 0)
            {
                for (int i = 0; i < ScreenRatioConst.Max + 1; i++)
                {
                    _gameobjects.InsertArrayElementAtIndex(0);
                }
                
                for (int i = ScreenRatioConst.Min; i <= ScreenRatioConst.Max; i++)
                {
                    displayInfo =  _gameobjects.GetArrayElementAtIndex(i);
                    displayInfo.FindPropertyRelative("ScreenRatio").enumValueIndex = i;
                }
            }

            serializedObject.ApplyModifiedProperties();

        }
    }
}