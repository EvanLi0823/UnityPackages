using System;

namespace RealYou.Utility.Update
{
    public interface IUpdateList<T>
    {
        void Add(T item);

        void Update();
    }
}