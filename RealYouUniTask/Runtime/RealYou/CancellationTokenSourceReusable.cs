using System;
using System.Threading;
using UnityEngine;

namespace UniRx.Async
{
    public class CancellationTokenSourceReusable:IDisposable
    {
        public CancellationToken Token
        {
            get
            {
                if(_cancellationTokenSource == null)
                    throw new Exception("_cancellationTokenSource is null");

                return _cancellationTokenSource.Token;
            }
        }

        public bool IsCancellationRequested
        {
            get
            {
                if(_cancellationTokenSource == null)
                    throw new Exception("_cancellationTokenSource is null");

                return _cancellationTokenSource.IsCancellationRequested;
            }
        }
        
        
        private CancellationTokenSource _cancellationTokenSource;

        public CancellationTokenSourceReusable()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Cancel()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        /*
         * 如果现在处于cancel的状态，则重置状态为normal的；否则什么都不做.
         */
        public void Reset()
        {
            if (_cancellationTokenSource == null || _cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource = new CancellationTokenSource();
            }
        }

        public void Dispose()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}