using System;
using System.Collections.Generic;
using UnityEngine;

namespace RealYou.Unity.UIAdapter
{   
    [RequireComponent(typeof(RectTransform)),DisallowMultipleComponent(),AddComponentMenu("UIAdapter/Position")]
    public class PositionAdapter : AdapterBase
    {   
        [Serializable]
        internal class PositionInfo
        {
            public ScreenRatio ScreenRatio;
        
            public Vector2 AnchorMin;
            public Vector2 AnchorMax;
            public Vector2 AnchoredPosition;
            public Vector2 SizeDelta;
            public Vector2 Pivot;
            public Vector3 LocalScale;
            public Quaternion LocalRotation;
        }
        
        [HideInInspector]
        [SerializeField]
        private List<PositionInfo> _positions;

        [HideInInspector]
        [SerializeField]
        private int _currentRatioIndex  = -1;
        
        protected override void OnScreenRatioChange(ScreenRatio ratio)
        {
            PositionInfo currentPostion = null;
            if(_currentRatioIndex < 0 || _positions == null || _positions.Count < _currentRatioIndex)
                return;

            currentPostion = _positions[_currentRatioIndex];
            if (currentPostion.ScreenRatio == ratio)
            {
                return;
            }

            for (int i = 0; i < _positions.Count; i++)
            {
                currentPostion = _positions[i];
                if (currentPostion != null && currentPostion.ScreenRatio == ratio)
                {
                    _currentRatioIndex = i;
                    ResetPosition(currentPostion);
                    return;
                }
            }

        }

        private void ResetPosition(PositionInfo pos)
        {
            RectTransform rectTransform = this.GetComponent<RectTransform>();
            
            rectTransform.anchorMin = pos.AnchorMin;
            rectTransform.anchorMax = pos.AnchorMax;
            
            rectTransform.anchoredPosition = pos.AnchoredPosition;
            rectTransform.sizeDelta = pos.SizeDelta;
            rectTransform.pivot = pos.Pivot;

            rectTransform.localScale = pos.LocalScale;
            rectTransform.localRotation = pos.LocalRotation;
        } 
        
    }

}
