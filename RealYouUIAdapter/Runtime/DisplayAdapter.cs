using System;
using System.Collections.Generic;
using UnityEngine;

namespace RealYou.Unity.UIAdapter
{
    [AddComponentMenu("UIAdapter/Display"),DisallowMultipleComponent()]
    public class DisplayAdapter : AdapterBase
    {
        [Serializable]
        internal class DisplayInfo
        {
            public ScreenRatio ScreenRatio;

            public bool IsShow;
        }
        
        [HideInInspector]
        [SerializeField]
        private List<DisplayInfo> _displayInfos;

        //默认是否显示.
        [HideInInspector]
        [SerializeField] 
        private bool _defaultShow;
        
        protected override void OnScreenRatioChange(ScreenRatio ratio)
        {
            if(_displayInfos == null)
                return;

            DisplayInfo temp;
            
            for (int i = 0; i < _displayInfos.Count; i++)
            {
                temp = _displayInfos[i];
                if (temp != null && temp.ScreenRatio == ratio)
                {
                    _defaultShow = temp.IsShow;
                    break;
                }
            }
            
            
            if (_defaultShow)
            {
                if(!gameObject.activeSelf)
                    gameObject.SetActive(true);
            }
            else
            {
                Destroy(gameObject);
                // DestroyImmediate(gameObject,true);
                
                UnloadRes();
            }
        }
    }
}