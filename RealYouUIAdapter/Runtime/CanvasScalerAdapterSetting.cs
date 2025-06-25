using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Serialization;

namespace RealYou.Unity.UIAdapter
{
    [CreateAssetMenu(menuName = "CreateAdapterSetting")]
    public class CanvasScalerAdapterSetting : ScriptableObject
    {
         [SerializeField] public CanvasScalerAdapterScope[] canvasScalerAdapterModel;

         public CanvasScalerAdapterScope GetScope(string scalerName)
         {
            if(canvasScalerAdapterModel==null)
                 return null;
            CanvasScalerAdapterScope[] rets =canvasScalerAdapterModel.Where(scope => scope.CanvasName.Contains(scalerName)).ToArray();
            if (rets != null && rets.Length > 0)
                return rets[0];
            return null;
         }
    }

    [Serializable]
    public class CanvasScalerAdapterScope
    {
        [SerializeField]
        public String[] CanvasName;
        [SerializeField]
        public CanvasScalerAdapterResolution Horizontal;
        [SerializeField]
        public CanvasScalerAdapterResolution Verticle;
    }
    [Serializable]
    public class CanvasScalerAdapterResolution
    {
        [SerializeField]
        public float IPhoneX=1f;
        [SerializeField]
        public float IPad=1f;
    }
}