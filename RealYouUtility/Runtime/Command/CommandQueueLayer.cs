using System;
using System.Collections.Generic;

namespace RealYou.Utility.Command
{
    public static class CommandQueueLayer
    {
        public static readonly int Normal = 0;

        public static void Add(int layer, int capacity, int jobs)
        {
            QueueConfig.AddConfig(layer,capacity,jobs);
        }
    }

    internal struct QueueConfig
    {
        public int Capacity;

        public int Jobs;

        public static int DefaultCapacity = 128;
        
        private static Dictionary<int,QueueConfig> Configs = new Dictionary<int, QueueConfig>()
        {
            {CommandQueueLayer.Normal,new QueueConfig(){Capacity = DefaultCapacity,Jobs = 1}}
        };

        public static QueueConfig GetConfig(int layer)
        {
            QueueConfig config;
            if (!Configs.TryGetValue(layer,out config))
            {
                config = new QueueConfig(){Capacity = DefaultCapacity,Jobs = 1};
                Configs.Add(layer,config);
            }

            return config;
        }

        public static void AddConfig(int layer, int capacity, int jobs)
        {
            if (Configs.ContainsKey(layer))
            {
                throw new Exception("the layer has exist " + layer);
            }

            Configs.Add(layer,new QueueConfig(){Capacity = capacity,Jobs = jobs});
        }
    }
}