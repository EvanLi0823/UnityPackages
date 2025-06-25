using UnityEditor;
using UnityEngine;

namespace RealYou.Unity.UIAdapter
{
    [CustomEditor(typeof(ImageAdapter))]
    public class ImageAdapterEditor:Editor
    {
        private const string ScreenRatioSeriaName = "ScreenRatio";
        
        private const string SpriteSeriaName = "Sprite";

        private ScreenRatio _ratio;

        private SerializedProperty _imageInfos;

        private void OnEnable()
        {
            _imageInfos = serializedObject.FindProperty("_imageInfos");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            serializedObject.UpdateIfRequiredOrScript();

            
            for (int i = 0; i < _imageInfos.arraySize; i++)
            {
                if (!Draw(_imageInfos.GetArrayElementAtIndex(i)))
                {
                    _imageInfos.DeleteArrayElementAtIndex(i);
                    break;
                }
            }
            
            EditorGUILayout.BeginHorizontal();
            
            _ratio = (ScreenRatio)EditorGUILayout.EnumPopup("屏幕比例",_ratio);
            if (GUILayout.Button("添加新配置"))
            {
                if (_ratio != ScreenRatio.None)
                {
                    if (!Add(_imageInfos,_ratio))
                    {
                        EditorUtility.DisplayDialog("Error", "已经有相同的配置了！", "ok");
                    }
                }
            }
            
            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }

        private static bool Draw(SerializedProperty imageInfo)
        {
            ScreenRatio ratio = (ScreenRatio)imageInfo.FindPropertyRelative(ScreenRatioSeriaName).enumValueIndex;
            SerializedProperty sprite = imageInfo.FindPropertyRelative(SpriteSeriaName);
            
            sprite.objectReferenceValue = EditorGUILayout.ObjectField(ratio.ToString(), sprite.objectReferenceValue, typeof(Sprite), false) as Sprite;
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("删除",GUILayout.MaxWidth(100)))
            {
                return false;
            }
            EditorGUILayout.EndHorizontal();

            return true;
        }

        private static bool Add(SerializedProperty images,ScreenRatio ratio)
        {
            int ratioInt = (int) ratio;
            for (int i = 0; i < images.arraySize; i++)
            {
                if (images.GetArrayElementAtIndex(i).FindPropertyRelative(ScreenRatioSeriaName).enumValueIndex == ratioInt)
                {
                    return false;
                }
            }
            
            images.InsertArrayElementAtIndex(images.arraySize);
            
            SerializedProperty newInfo = images.GetArrayElementAtIndex(images.arraySize - 1);
            newInfo.FindPropertyRelative(ScreenRatioSeriaName).enumValueIndex = ratioInt;
            newInfo.FindPropertyRelative(SpriteSeriaName).objectReferenceValue = null;
            

            return true;

        }
    }
}