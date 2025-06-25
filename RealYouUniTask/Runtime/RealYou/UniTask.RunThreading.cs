#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;

namespace UniRx.Async
{
    public partial struct UniTask
    {
        public static async void RunInMainThread(Action action)
        {
            if(action == null)
                return;
            
            await SwitchToMainThread();
            action();
        }
        
        public static async void RunInMainThread<T>(Action<T> action, T val)
        {
            if(action == null)
                return;
            
            await SwitchToMainThread();
            action(val);
        }

        public static async void  RunInThreadPool(Action action)
        {
            if(action == null)
                return;
            
            await SwitchToThreadPool();
            action();
        }
        
        public static async void  RunInThreadPool<T>(Action<T> action, T val)
        {
            if(action == null)
                return;
            
            await SwitchToThreadPool();
            action(val);
        }
    }
}

#endif