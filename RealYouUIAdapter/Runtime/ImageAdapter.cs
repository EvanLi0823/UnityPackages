using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RealYou.Unity.UIAdapter
{
    [AddComponentMenu("UIAdapter/Image"),DisallowMultipleComponent()]
    public class ImageAdapter : AdapterBase
    {
        [Serializable]
        internal class ImageInfo
        {
            public ScreenRatio ScreenRatio;
        
            public Sprite Sprite;
        }
        
        [HideInInspector ]
        [SerializeField]
        private List<ImageInfo> _imageInfos;

        
        private Sprite _currentSprite;

        public Sprite GetCurrentSprite()
        {
            if (_screenRatio == ScreenRatio.None || _imageInfos == null)
                return null;

            if (_currentSprite != null)
            {
                return _currentSprite;
            }

            ImageInfo imageInfo;
            for (int i = 0; i < _imageInfos.Count; i++)
            {
                imageInfo = _imageInfos[i];
                if(imageInfo == null)
                    continue;
                
                if (imageInfo.ScreenRatio == _screenRatio)
                {

                    _currentSprite = imageInfo.Sprite;
                }
            }

            if (_currentSprite == null)
            {
                Image image = gameObject.GetComponent<Image>();
                if (image)
                {
                    _currentSprite = image.sprite;
                }
            }

            return _currentSprite;

        }
        protected override void OnScreenRatioChange(ScreenRatio ratio)
        {
            if(_imageInfos == null)
                return;

            ImageInfo imageInfo;
            for (int i = 0; i < _imageInfos.Count; i++)
            {
                imageInfo = _imageInfos[i];
                if(imageInfo == null)
                    continue;
                
                if (imageInfo.ScreenRatio == ratio && imageInfo.Sprite != null)
                {
                    
                    Image image = gameObject.GetComponent<Image>();
                    if (image)
                    {
                        image.sprite = imageInfo.Sprite;
                    }
                }
                else
                {
                    imageInfo.Sprite = null;
                }
            }
            UnloadRes();
        }
    }
}
