using UnityEditor;
using UnityEngine;

namespace RealYou.Unity.UIAdapter
{
    [CustomEditor(typeof(PositionAdapter))]
    public class PositionAdapterEditor : Editor
    {
        private const string ScreenRatioSeriaName = "ScreenRatio";

        private const string AnchorMinSeriaName = "AnchorMin";
        private const string AnchorMaxSeriaName = "AnchorMax";
        private const string AnchorPosSeriaName = "AnchoredPosition";
        private const string SizeDeltaSeriaName = "SizeDelta";
        private const string PivotSeriaName = "Pivot";
        private const string LocalScaleSeriaName = "LocalScale";
        private const string LocalRotationSeriaName = "LocalRotation";
        
        private ScreenRatio _currentRatio;

        private PositionAdapter _target;

        private SerializedProperty _currentRatioIndex;

        private SerializedProperty _positions;

        private void OnEnable()
        {
            _currentRatioIndex = serializedObject.FindProperty("_currentRatioIndex");
            _positions = serializedObject.FindProperty("_positions");
            if (_currentRatioIndex.intValue == -1)
            {
                _currentRatio = ScreenRatio.None;
            }
            else
            {
                SerializedProperty pos = _positions.GetArrayElementAtIndex(_currentRatioIndex.intValue);

                _currentRatio = (ScreenRatio) pos.FindPropertyRelative(ScreenRatioSeriaName).enumValueIndex;
            }

            _target = target as PositionAdapter;
            UnityEditorInternal.ComponentUtility.MoveComponentUp(_target);

        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ScreenRatio ratio = (ScreenRatio) EditorGUILayout.EnumPopup("屏幕尺寸", _currentRatio);
            if (ratio != _currentRatio && ratio != ScreenRatio.None)
            {
                _currentRatio = ratio;
//                _target.SetRatio(ratio);

                serializedObject.UpdateIfRequiredOrScript();
                ChangeRatio((int)ratio);
                serializedObject.ApplyModifiedProperties();
            }

        }

        [MenuItem("Component/UIAdapter/Position(Old)")]
        private static void AddPositionAdapter()
        {
            PositionAdapter adapter = Selection.activeGameObject.GetComponent<PositionAdapter>();
            if (adapter != null)
            {
                adapter.hideFlags = HideFlags.None;
                EditorUtility.DisplayDialog("Error", "已经添加过PositionAdapter,请不要重复添加.", "ok");
            }
            else
            {
                Selection.activeGameObject.AddComponent<PositionAdapter>();
            }
        }

        private void ChangeRatio(int radio)
        {
            SerializedProperty pos;
            
            //保存现有的.
            if (_currentRatioIndex.intValue != -1)
            {
                pos = _positions.GetArrayElementAtIndex(_currentRatioIndex.intValue);
                CopyPostion(pos);
                
            }

            _currentRatioIndex.intValue = -1;
            
            //恢复以前的.

            for (int i = 0; i < _positions.arraySize; i++)
            {
                pos = _positions.GetArrayElementAtIndex(i);
                if (pos.FindPropertyRelative(ScreenRatioSeriaName).enumValueIndex == radio)
                {
                    _currentRatioIndex.intValue = i;
                    break;
                }
            }

            if (_currentRatioIndex.intValue != -1)
            {
                pos = _positions.GetArrayElementAtIndex(_currentRatioIndex.intValue);
                ResetPostion(pos);
            }
            else
            {
                //未找到，需要添加新的.
                _currentRatioIndex.intValue = _positions.arraySize;
                _positions.InsertArrayElementAtIndex(_currentRatioIndex.intValue);
                
                pos = _positions.GetArrayElementAtIndex(_currentRatioIndex.intValue);
                CopyPostion(pos);
                pos.FindPropertyRelative(ScreenRatioSeriaName).enumValueIndex = (int) radio;
            }
        }

        private void CopyPostion(SerializedProperty pos)
        {
            RectTransform rectTransform = (target as Component).GetComponent<RectTransform>();

            pos.FindPropertyRelative(AnchorMinSeriaName).vector2Value = rectTransform.anchorMin;
            pos.FindPropertyRelative(AnchorMaxSeriaName).vector2Value = rectTransform.anchorMax;

            pos.FindPropertyRelative(AnchorPosSeriaName).vector2Value = rectTransform.anchoredPosition;
            pos.FindPropertyRelative(SizeDeltaSeriaName).vector2Value = rectTransform.sizeDelta;
            pos.FindPropertyRelative(PivotSeriaName).vector2Value = rectTransform.pivot;

            pos.FindPropertyRelative(LocalScaleSeriaName).vector3Value = rectTransform.localScale;
            pos.FindPropertyRelative(LocalRotationSeriaName).quaternionValue = rectTransform.localRotation;
        }
        
        private void ResetPostion(SerializedProperty pos)
        {
            RectTransform rectTransform = (target as Component).GetComponent<RectTransform>();

            rectTransform.anchorMin = pos.FindPropertyRelative(AnchorMinSeriaName).vector2Value;
            rectTransform.anchorMax = pos.FindPropertyRelative(AnchorMaxSeriaName).vector2Value;

            rectTransform.anchoredPosition = pos.FindPropertyRelative(AnchorPosSeriaName).vector2Value;
            rectTransform.sizeDelta = pos.FindPropertyRelative(SizeDeltaSeriaName).vector2Value;
            rectTransform.pivot = pos.FindPropertyRelative(PivotSeriaName).vector2Value;

            rectTransform.localScale = pos.FindPropertyRelative(LocalScaleSeriaName).vector3Value;
            rectTransform.localRotation = pos.FindPropertyRelative(LocalRotationSeriaName).quaternionValue;
        }
    }
}
