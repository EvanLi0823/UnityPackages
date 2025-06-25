using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace RealYou.Unity.UIAdapter
{
    public class CanvasScalerAdapt:AdapterBase
    {
        private static readonly float SCREEN_NORMAL_WIDTH = 1920f;
        private static readonly float SCREEN_NORMAL_HEIGHT = 1080f;
        private static readonly float IPHONEX_WIDTH = 2436f;

        [Serializable]
        internal class SettingInfo
        {
            public CanvasScalerAdapterSetting m_Setting;
        }
        
        private ScreenRatio _Radio;
        
        [HideInInspector]
        [SerializeField] 
         SettingInfo Settings;
        protected override void OnScreenRatioChange(ScreenRatio ratio)
        {
            _Radio = ratio;
        }
        public void SetCanvasScaler( bool isPortait, Camera camera)
        {
            float screenScale = 1f;
//            if (IphoneXAdapter.IsIphoneX()) {
//                screenScale = SkySreenUtils.DEVICE_WIDTH / screenSizeXIphoneX;
//            }
//            else if (CommonMobileDeviceAdapter.IsWideScreen()) {
//                screenScale = SkySreenUtils.DEVICE_HEIGHT / SCREEN_NORMAL_HEIGHT;
//            }
//            else if (CommonMobileDeviceAdapter.IsSquareScreen()) {
//                screenScale = SkySreenUtils.DEVICE_WIDTH / SCREEN_NORMAL_WIDTH;
//            } 
//            else
//            {
//                screenScale = SkySreenUtils.DEVICE_WIDTH / SCREEN_NORMAL_WIDTH;
//            }

            if (Settings == null)
            {
                Debug.LogError("需要配置setting");
                return;
            }
            
            CanvasScaler[] canvasScaler = camera.GetComponentsInChildren<CanvasScaler>()
                .Where(scaler => scaler.GetComponent<Canvas>().isRootCanvas).ToArray();

            for (int i = 0; i < canvasScaler.Length; i++)
            {
                CanvasScalerAdapterScope scope = Settings.m_Setting.GetScope(canvasScaler[i].name);
                if (scope != null)
                {
                    CanvasScalerAdapterResolution resolution = null;
                    if (isPortait)
                        resolution = scope.Verticle;
                    else
                    {
                        resolution = scope.Horizontal;
                    }

                    if (_Radio == ScreenRatio.IphoneX)
                    {
                        canvasScaler[i].uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
                        canvasScaler[i].scaleFactor = GetWidth(isPortait) / IPHONEX_WIDTH * resolution.IPhoneX;
                        //canvasScaler[i].scaleFactor = 1* resolution.IPhoneX;
                    }
                    else if (_Radio == ScreenRatio.FourThree)
                    {
                        canvasScaler[i].uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
                        canvasScaler[i].scaleFactor = GetWidth(isPortait) /SCREEN_NORMAL_WIDTH  * resolution.IPad;
                        //canvasScaler[i].scaleFactor = 1 * resolution.IPad;
                    }
//                    resolution;
                }
            }
        }

        private int GetWidth(bool isPortait)
        {
            if (isPortait)
            {
                return Screen.height;
            }
            else
            {
                return Screen.width;
            }
        }
    }
}