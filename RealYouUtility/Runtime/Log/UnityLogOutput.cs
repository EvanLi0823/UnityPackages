using System;
using UnityEngine;

namespace RealYou.Utility.Log
{
    internal class UnityLogOutput : ILogOutput
    {

        public void Write(LogLevel level, string channel, string message)
        {
            switch (level)
            {
                case LogLevel.Log:
                    Debug.LogFormat("{0}-{1}", channel, message);
                    break;
                case LogLevel.Warning:
                    Debug.LogWarningFormat("{0}-{1}", channel, message);
                    break;
                case LogLevel.Error:
                    Debug.LogErrorFormat("{0}-{1}", channel, message);
                    break;
                case LogLevel.Exception:
                    #if UNITY_2019_1_OR_NEWER
                    Debug.LogFormat(LogType.Exception, LogOption.None,null,"[Exception] {0}-{1}", channel, message);
                    #else
                    Debug.LogErrorFormat("[Exception] {0}-{1}", channel, message);
                    #endif
                    break;
                    
            }
        }
    }
}