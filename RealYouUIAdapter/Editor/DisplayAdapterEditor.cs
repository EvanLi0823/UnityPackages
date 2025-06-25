using UnityEditor;

namespace RealYou.Unity.UIAdapter
{
    [CustomEditor(typeof(DisplayAdapter))]
    public class DisplayAdapterEditor:Editor
    {   
        private const string ScreenRatioSeriaName = "ScreenRatio";
        
        private const string IsShowSeriaName = "IsShow";

        private SerializedProperty _defaultShow;

        private SerializedProperty _displayInfos;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.UpdateIfRequiredOrScript();

            bool defaultShowOld = _defaultShow.boolValue;
            bool defaultShowNew = EditorGUILayout.Toggle("默认是否显示", _defaultShow.boolValue);

            _defaultShow.boolValue = defaultShowNew;


            EditorGUILayout.LabelField("override");

            SerializedProperty info;
            ScreenRatio ratio;
            bool showOld;
            for (int i = 0; i < _displayInfos.arraySize; i++)
            {
                info = _displayInfos.GetArrayElementAtIndex(i);
                
                ratio = (ScreenRatio)info.FindPropertyRelative(ScreenRatioSeriaName).enumValueIndex;
                showOld = info.FindPropertyRelative(IsShowSeriaName).boolValue;

                if (showOld == defaultShowOld)
                {
                    showOld = defaultShowNew;
                }
                else
                {
                    showOld = !defaultShowNew;
                }
                info.FindPropertyRelative(IsShowSeriaName).boolValue = EditorGUILayout.Toggle(ratio.ToString(), showOld);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            _defaultShow = serializedObject.FindProperty("_defaultShow");
            _displayInfos = serializedObject.FindProperty("_displayInfos");

            if (_displayInfos.arraySize == 0)
            {
                serializedObject.UpdateIfRequiredOrScript();
                
                for (int i = 0; i < ScreenRatioConst.Max; i++)
                {
                    _displayInfos.InsertArrayElementAtIndex(0);
                }

                for (int i = 0; i < ScreenRatioConst.Max; i++)
                {
                    _displayInfos.GetArrayElementAtIndex(i).FindPropertyRelative(ScreenRatioSeriaName).enumValueIndex =
                        i + 1;
                }
                
                serializedObject.ApplyModifiedProperties();
            }

        }
    }
}