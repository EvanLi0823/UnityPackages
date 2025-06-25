using System;
using System.Collections.Generic;
using UnityEngine;

namespace RealYou.Unity.UIAdapter
{
    /// <summary>
    /// 更具不同的分辨率显示不同的内容.
    /// </summary>
    [AddComponentMenu("UIAdapter/Selector"),DisallowMultipleComponent()]
    public class DisplaySelector:AdapterBase
    {
        [Serializable]
        internal class DisplayInfo
        {
            public ScreenRatio ScreenRatio;

            public GameObject GameObject;
        }
        
        [HideInInspector]
        [SerializeField] 
        private DisplayInfo[] _gameobjects;

        private GameObject _currentGameObject;


        public GameObject GetGameObject()
        {
            if (_gameobjects == null || _screenRatio == ScreenRatio.None || _gameobjects.Length < 1)
            {
                return null;
            }

            if (_currentGameObject != null)
            {
                return _currentGameObject;
            }

            DisplayInfo info = _gameobjects[(int)_screenRatio];
            if (info != null)
            {
                _currentGameObject = info.GameObject;
            }

            if (_currentGameObject == null)
            {
                _currentGameObject = gameObject;
            }

            return _currentGameObject;
        }
        
        protected override void OnScreenRatioChange(ScreenRatio ratio)
        {
            if (_gameobjects == null || _gameobjects.Length < 1)
            {
                return;
            }

            GameObject current = GetGameObject();

            DisplayInfo info;
            for (int i = 0; i < _gameobjects.Length; i++)
            {
                info = _gameobjects[i];
                if (info == null)
                {
                    continue;
                }

                if (ratio == info.ScreenRatio)
                {
                    if (info.GameObject != null && !info.GameObject.activeSelf)
                    {
                        info.GameObject.SetActive(true);
                    }
                }
                else
                {
                    if(current != info.GameObject)
                    {
                        Destroy(info.GameObject);
                    }
                    info.GameObject = null;
                }
            }
            
            UnloadRes();
        }
    }
}