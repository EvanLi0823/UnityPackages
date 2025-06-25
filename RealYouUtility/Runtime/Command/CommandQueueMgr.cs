using System;
using System.Collections.Generic;
using RealYou.Utility.Log;

namespace RealYou.Utility.Command
{
    public static class CommandQueueMgr
    {
        public static event Action<Exception> UnobservedException;
        
        private static Dictionary<int, CommandQueue> _queueLayers;

        static CommandQueueMgr()
        {
            _queueLayers = new Dictionary<int, CommandQueue>(128);
        }

        internal static CommandQueue GetQueue(int queueLayer)
        {
            CommandQueue queue;
            if (!_queueLayers.TryGetValue(queueLayer,out queue))
            {
                QueueConfig config = QueueConfig.GetConfig(queueLayer);
                queue = new CommandQueue(config.Capacity, config.Jobs,PublishUnobservedException);
                _queueLayers.Add(queueLayer,queue);
            }

            return queue;

        }

        internal static void PublishUnobservedException(Exception ex)
        {
            if (UnobservedException != null)
            {
                UnobservedException.Invoke(ex);
            }
            else
            {
                Logger.GlobalLogger.LogException(ex);
            }
        }
    }
}