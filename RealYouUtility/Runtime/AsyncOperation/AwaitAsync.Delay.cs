using System;
using UnityExtend;

namespace RealYou.Utility.AsyncOperation
{
    public static partial class AwaitAsync
    {
        public static IAwaiter Delay(float time)
        {
            return new DelayImp(time);
        }

        private class DelayImp : AwaitAble
        {
            private ulong _uid;
            
            public DelayImp(float time)
            {
                _uid = CustomUpdate.Add(time, InvokeCompletionEvent);
            }

            protected override void OnCancel()
            {
                base.OnCancel();
                
                CustomUpdate.Remove(_uid);
            }
        }
    }
}