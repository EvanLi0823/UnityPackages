#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Threading;
using UniRx.Async.Internal;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UniRx.Async
{
    public partial struct UniTask
    {
        public static UniTask Delay(TimeSpan delayTimeSpan, Object context, CancellationToken cancellationToken = default(CancellationToken), bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update)
        {
            if (delayTimeSpan < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException("Delay does not allow minus delayFrameCount. delayTimeSpan:" + delayTimeSpan);
            }
            
            if(context == null)
                throw new ArgumentException("context is null");
            
            return ignoreTimeScale
                ? new DelayIgnoreTimeScaleTerminatePromise(delayTimeSpan, context, delayTiming, cancellationToken).Task
                : new DelayTerminatePromise(delayTimeSpan, context, delayTiming, cancellationToken).Task;
        }
        
        public static UniTask<int> DelayFrame(int delayFrameCount, Object context, CancellationToken cancellationToken = default(CancellationToken), PlayerLoopTiming delayTiming = PlayerLoopTiming.Update)
        {
            if (delayFrameCount < 0)
            {
                throw new ArgumentOutOfRangeException("Delay does not allow minus delayFrameCount. delayFrameCount:" + delayFrameCount);
            }
            
            if(context == null)
                throw new ArgumentException("context is null");

            var source = new DelayFrameTerminatePromise(delayFrameCount,context, delayTiming, cancellationToken);
            return source.Task;
        }
        
        class DelayFrameTerminatePromise : PlayerLoopReusablePromiseBase<int>
        {
            readonly int delayFrameCount;
            int currentFrameCount;
            
            private readonly Object _context;

            public DelayFrameTerminatePromise(int delayFrameCount, Object context, PlayerLoopTiming timing, CancellationToken cancellationToken)
                : base(timing, cancellationToken, 2)
            {
                this.delayFrameCount = delayFrameCount;
                this.currentFrameCount = 0;
                _context = context;
            }

            protected override void OnRunningStart()
            {
                currentFrameCount = 0;
            }

            public override bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested
#if !UNITY_EDITOR || SAFE_MODE
                    || _context == null
#endif
                )
                {
                    Complete();
                    TrySetCanceled();
                    return false;
                }

                if (currentFrameCount == delayFrameCount)
                {
                    Complete();
                    TrySetResult(currentFrameCount);
                    return false;
                }

                currentFrameCount++;
                return true;
            }
            
            //只设置标记，并不continue.
            public override bool TrySetCanceled()
            {   
                if (status != AwaiterStatus.Pending)
                {
                    return false;
                }

                status = AwaiterStatus.Canceled;
                return true;
            }
        }

        class DelayTerminatePromise : PlayerLoopReusablePromiseBase
        {
            private readonly float delayFrameTimeSpan;
            private float elapsed;
            private readonly Object _context;


            public DelayTerminatePromise(TimeSpan delayFrameTimeSpan, Object context, PlayerLoopTiming timing,
                CancellationToken cancellationToken)
                : base(timing, cancellationToken, 2)
            {
                this.delayFrameTimeSpan = (float) delayFrameTimeSpan.TotalSeconds;
                _context = context;
            }

            protected override void OnRunningStart()
            {
                elapsed = 0.0f;
            }

            public override bool MoveNext()
            {
                //如果在Editor模式下不对context进行判断，目的是将exception曝露给开发者.
                if (cancellationToken.IsCancellationRequested
#if !UNITY_EDITOR || SAFE_MODE
                    || _context == null
#endif
                    )
                {
                    Complete();
                    TrySetCanceled();
                    return false;
                }

                elapsed += Time.deltaTime;
                if (elapsed >= delayFrameTimeSpan)
                {
                    Complete();
                    TrySetResult();
                    return false;
                }

                return true;
            }

            //只设置标记，并不continue.
            public override bool TrySetCanceled()
            {   
                if (status != AwaiterStatus.Pending)
                {
                    return false;
                }

                status = AwaiterStatus.Canceled;
                return true;
            }
        }

        class DelayIgnoreTimeScaleTerminatePromise : PlayerLoopReusablePromiseBase
        {
            private readonly float delayFrameTimeSpan;
            private float elapsed;
            private readonly Object _context;

            public DelayIgnoreTimeScaleTerminatePromise(TimeSpan delayFrameTimeSpan, Object context, PlayerLoopTiming timing, CancellationToken cancellationToken)
                : base(timing, cancellationToken, 2)
            {
                this.delayFrameTimeSpan = (float)delayFrameTimeSpan.TotalSeconds;
                _context = context;
            }

            protected override void OnRunningStart()
            {
                elapsed = 0.0f;
            }

            public override bool MoveNext()
            {
                //如果在Editor模式下不对context进行判断，目的是将exception曝露给开发者.
                if (cancellationToken.IsCancellationRequested 
#if !UNITY_EDITOR || SAFE_MODE
                    || _context == null
#endif
                    )
                {
                    Complete();
                    TrySetCanceled();
                    return false;
                }

                elapsed += Time.unscaledDeltaTime;

                if (elapsed >= delayFrameTimeSpan)
                {
                    Complete();
                    TrySetResult();
                    return false;
                }

                return true;
            }

            //只设置标记，并不continue.
            public override bool TrySetCanceled()
            {
                if (status != AwaiterStatus.Pending)
                {
                    return false;
                }

                status = AwaiterStatus.Canceled;
                return true;
            }
        }
    }
}

#endif