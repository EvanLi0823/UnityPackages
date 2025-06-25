using System;
using System.Collections.Generic;
using RealYou.Utility.BFException;
using UnityEngine;

namespace RealYou.Utility.Pool
{
    public class ObjectPool<T>
    {
        private readonly List<T> _objects;

        private readonly Func<T> _create;

        private readonly Action<T> _destroy;

        public ObjectPool(int capacity, Func<T> create, Action<T> destroy)
        {
            _objects = new List<T>(capacity);

            _create = create ?? throw new ParamsNullException();
            _destroy = destroy ?? throw new ParamsNullException();
        }

        public virtual T Create()
        {
            T ret;
            if (_objects.Count > 0)
            {
                int index = _objects.Count - 1;
                ret = _objects[index];
                _objects.RemoveAt(index);
            }
            else
            {
                ret = _create();
            }

            return ret;
        }

        public virtual void Release(T val)
        {
            if (_objects.Count >= _objects.Capacity)
            {
                _destroy?.Invoke(val);
            }
            else
            {
                _objects.Add(val);
            }
        }
    }
}