using System;
using UnityEditor;
using UnityEngine;

namespace RealYou.Unity.UIAdapter
{
    [CustomEditor(typeof(CanvasScalerAdapt))]
    public class CanvasScalerEditor:Editor
    {
        private SerializedProperty SettingProperty;
//        private CanvasScalerAdapterSetting setting;
         void OnEnable()
        {
            SettingProperty = serializedObject.FindProperty("Settings");
//            setting = SettingProperty.objectReferenceValue as CanvasScalerAdapterSetting;
            CanvasScalerAdapt _target = target as CanvasScalerAdapt;
//            UnityEditorInternal.ComponentUtility.MoveComponentUp(_target);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayoutOption[] options = new GUILayoutOption[0];
            SettingProperty.FindPropertyRelative("m_Setting").objectReferenceValue=(CanvasScalerAdapterSetting)EditorGUILayout.ObjectField("setting",SettingProperty.FindPropertyRelative("m_Setting").objectReferenceValue,typeof(CanvasScalerAdapterSetting),false);
            serializedObject.ApplyModifiedProperties();
//            if (setting1 != setting)
//            {
////                pos.FindPropertyRelative(ScreenRatioSeriaName)
//                serializedObject.UpdateIfRequiredOrScript();
//                SettingProperty.objectReferenceValue = setting;
//                Debug.Log(1111);
//                serializedObject.ApplyModifiedProperties();
//            }
//            newSprite = EditorGUILayout.ObjectField ("Sprite", currentSprite, typeof(Sprite), true, options) as Sprite;
        }
    }
}