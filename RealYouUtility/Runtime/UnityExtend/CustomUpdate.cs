using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityExtend
{
    internal class CustomUpdate : MonoBehaviour
    {
        private class Info
        {
            private float _time;

            private bool _isDone;
            
            public float Delay;

            public Action Action;

            public Info()
            {
                _time = 0;
            }

            public void Reset()
            {
                _time = 0;
                Action = null;
            }

            public bool IsDone(float delta)
            {
                _time += delta;

                _isDone = _time >= Delay;

                return _isDone;
            }
            

            public bool TryInvoke()
            {
                if (!_isDone)
                    return false;

                try
                {
                    Action?.Invoke();
                }
                catch (Exception e)
                {
                    #if UNITY_EDITOR
                    throw e;
                    #endif
                }
                return true;
            }
        }

        private static CustomUpdate _instance;
        
        private Dictionary<ulong,Info> _infos = new Dictionary<ulong,Info>();
        
        private List<Info> _pool = new List<Info>();
        
        private List<ulong> _done = new List<ulong>();

        private ulong _uid = 0;

        #region static

        public static ulong Add(float delay, Action action)
        {
            CheckInstance();

            return _instance.InternalAdd(delay, action);
        }

        public static void Remove(ulong token)
        {
            CheckInstance();
            _instance.InternalRemove(token);
        }

        private static void CheckInstance()
        {
            if (_instance == null)
            {
                GameObject go = new GameObject(){hideFlags = HideFlags.HideAndDontSave};
                _instance = go.AddComponent<CustomUpdate>();
            }
        }

        #endregion

        private ulong InternalAdd(float delay, Action action)
        {
            Info temp = null;
            if (_pool.Count > 0)
            {
                var last = _pool.Count - 1;
                temp = _pool[last];
                _pool.RemoveAt(last);

                temp.Reset();
                temp.Delay = delay;
                temp.Action = action;
            }
            else
            {
                temp = new Info() {Delay = delay, Action = action};
            }


            IncreaseUid();
            
            if(_infos.ContainsKey(_uid))
            {
                for (ulong i = 0; i < ulong.MaxValue; i++)
                {
                    IncreaseUid();
                    if (!_infos.ContainsKey(_uid))
                        break;
                }
            }
            _infos.Add(_uid,temp);
            return _uid;
        }

        private void InternalRemove(ulong token)
        {
            if (_infos.TryGetValue(token,out var temp))
            {
                _infos.Remove(token);
                
                _pool.Add(temp);
            }
        }

        private void Update()
        {
            foreach (var info in _infos)
            {
                if (info.Value.IsDone(Time.deltaTime))
                {
                    _done.Add(info.Key);
                }
            }

            foreach (var token in _done)
            {
                DoneAndRemove(token);
            }
            
            _done.Clear();
        }

        private void DoneAndRemove(ulong token)
        {
            if (_infos.TryGetValue(token,out var temp))
            {
                temp.TryInvoke();
            }

            InternalRemove(token);
        }

        private void IncreaseUid()
        {
            _uid++;
            if (_uid == ulong.MaxValue)
            {
                _uid = 0;
            }
        }
    }
}