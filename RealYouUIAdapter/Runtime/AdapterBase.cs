using System;
using UnityEngine;
using UniRx.Async;
namespace RealYou.Unity.UIAdapter
{

    [Flags]
    public enum ScreenRatio
    {
        None = 0,
        FourThree = 1,
        SixteenNine = 2,
        ThreeTwo = 3,
        IphoneX=4
    }

    public struct ScreenRatioConst
    {
        public const int Min = 1;

        public const int Max = 4;
    }
    
    public abstract class AdapterBase : MonoBehaviour
    {
        private readonly float[] _ratios = new float[]{0,1.3333f,1.7777f,1.5f,2.165f};
        
        protected static ScreenRatio _screenRatio = ScreenRatio.None;

        private static bool _isWaitingUnload = false;
        
        private void Awake()
        {
            if (_screenRatio == ScreenRatio.None)
            {
                Init();
            }

            if (_screenRatio != ScreenRatio.None)
            {
                OnScreenRatioChange(_screenRatio);
            }
        }

        public static void SetScreenRatio(ScreenRatio ratio)
        {
            _screenRatio = ratio;
        }

        private void Init()
        {
            //计算当前的屏幕比例
            int height = Screen.height;
            int width = Screen.width;

            float ratio = width * 1.0f / height;
            if (ratio < 1)
                ratio = 1.0f / ratio;
            
            float min = int.MaxValue;
            
            for (int i = 0; i < _ratios.Length; i++)
            {
                float temp = Math.Abs(_ratios[i] - ratio);
                if (temp < min)
                {
                    min = temp;
                    _screenRatio = (ScreenRatio) i;

                }
            }
            if (_screenRatio == ScreenRatio.IphoneX)
            {
                if (!IsIphoneX())
                {
                    _screenRatio = ScreenRatio.SixteenNine;
                }
            }
        }

        public static bool IsIphoneX(){
		
            string deviceType = SystemInfo.deviceModel;
            int DEVICE_WIDTH = Screen.width;
            int DEVICE_HEIGHT = Screen.height;
#if UNITY_EDITOR
		return (DEVICE_WIDTH ==2436&&DEVICE_HEIGHT ==1125)||(DEVICE_WIDTH==1792&&DEVICE_HEIGHT==828)||(DEVICE_WIDTH==2688&&DEVICE_HEIGHT==1242)||
            (DEVICE_WIDTH == 1125 && DEVICE_HEIGHT == 2436) || (DEVICE_WIDTH == 828 && DEVICE_HEIGHT == 1792) || (DEVICE_WIDTH == 1242 && DEVICE_HEIGHT ==2688 );
#else
            return (deviceType == "iPhone10,3" || deviceType == "iPhone10,6")
                   || deviceType == "iPhone11,8" //XR
                   || deviceType == "iPhone11,2"//XS
                   || deviceType == "iPhone11,4"//XS Max
                   || deviceType == "iPhone11,6"//XS Max
                   || deviceType == "iPhone12,1"//iPhone 11
                   || deviceType == "iPhone12,3"//iPhone 11 Pro
                   || deviceType == "iPhone12,5"//iPhone 11 Pro Max
                   || deviceType == "iPhone13,1" // iPhone 12 Mini
                   || deviceType == "iPhone13,2" // iPhone 12
                   || deviceType == "iPhone13,3" // iPhone 12 Pro
                   || deviceType == "iPhone13,4"; // iPhone 12 Pro Max
#endif

        }
        protected abstract void OnScreenRatioChange(ScreenRatio ratio);
        
         

        protected async void UnloadRes()
        {
            if(_isWaitingUnload)
                return;
            
            _isWaitingUnload = true;
            UniTask task = UniTask.Delay(TimeSpan.FromSeconds(1));
            await task;
            Resources.UnloadUnusedAssets();
            _isWaitingUnload = false;
        }
    }

}
