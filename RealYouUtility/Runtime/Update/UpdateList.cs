using System;
using System.Collections.Generic;

namespace RealYou.Utility.Update
{
    public class UpdateList : List<Func<bool>>, IUpdateList<Func<bool>>
    {
        public void Update()
        {
            for (int i = 0; i < Count;)
            {
                var temp = this[i];
                if (temp != null && temp())
                {
                    i++;
                    continue;
                }

                var last = Count - 1;
                if (i < last)
                {
                    this[i] = this[Count - 1];
                }
                
                RemoveAt(last);
            }
        }
    }
}