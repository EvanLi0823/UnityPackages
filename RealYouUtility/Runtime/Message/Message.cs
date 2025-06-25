using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RealYou.Utility.Message
{
    public class Message
    {
        private Dictionary<string, UnityEventBase> _actions;

        public Message(int capacity)
        {
            _actions = new Dictionary<string, UnityEventBase>(capacity);
        }

        public Message() : this(16)
        {

        }

        public void AddListener(string key, UnityAction call)
        {
            if (string.IsNullOrEmpty(key) || call == null)
                return;

            UnityEventBase eventBase = null;
            if (!_actions.TryGetValue(key, out eventBase))
            {
                eventBase = new MessageEvent();
                _actions.Add(key, eventBase);
            }

            if (eventBase is UnityEvent unityEvent)
            {
                unityEvent.AddListener(call);
            }
        }
        
        public void RemoveListener(string key, UnityAction call)
        {
            if (string.IsNullOrEmpty(key) || call == null)
                return;

            UnityEventBase eventBase = null;
            if (!_actions.TryGetValue(key, out eventBase))
            {
               return;
            }

            if (eventBase is UnityEvent unityEvent)
            {
                unityEvent.RemoveListener(call);
            }
        }

        public void AddListener<T>(string key, UnityAction<T> call)
        {
            if (string.IsNullOrEmpty(key) || call == null)
                return;

            UnityEventBase eventBase = null;
            if (!_actions.TryGetValue(key, out eventBase))
            {
                eventBase = new MessageEvent<T>();
                _actions.Add(key, eventBase);
            }

            if (eventBase is MessageEvent<T> unityEvent)
            {
                unityEvent.AddListener(call);
            }
        }

        public void RemoveListener<T>(string key, UnityAction<T> call)
        {
            if (string.IsNullOrEmpty(key) || call == null)
                return;

            UnityEventBase eventBase = null;
            if (!_actions.TryGetValue(key, out eventBase))
            {
                return;
            }

            if (eventBase is UnityEvent<T> unityEvent)
            {
                unityEvent.RemoveListener(call);
            }
        }
        
        public void AddListener<T1, T2>(string key, UnityAction<T1, T2> call)
        {
            if (string.IsNullOrEmpty(key) || call == null)
                return;

            UnityEventBase eventBase = null;
            if (!_actions.TryGetValue(key, out eventBase))
            {
                eventBase = new MessageEvent<T1,T2>();
                _actions.Add(key, eventBase);
            }

            if (eventBase is UnityEvent<T1, T2> unityEvent)
            {
                unityEvent.AddListener(call);
            }
        }
        
        public void RemoveListener<T1, T2>(string key, UnityAction<T1, T2> call)
        {
            if (string.IsNullOrEmpty(key) || call == null)
                return;

            UnityEventBase eventBase = null;
            if (!_actions.TryGetValue(key, out eventBase))
            {
                return;
            }

            if (eventBase is UnityEvent<T1, T2> unityEvent)
            {
                unityEvent.RemoveListener(call);
            }
        }

        public void AddListener<T1, T2, T3>(string key, UnityAction<T1, T2, T3> call)
        {
            if (string.IsNullOrEmpty(key) || call == null)
                return;

            UnityEventBase eventBase = null;
            if (!_actions.TryGetValue(key, out eventBase))
            {
                eventBase = new MessageEvent<T1,T2,T3>();
                _actions.Add(key, eventBase);
            }

            if (eventBase is UnityEvent<T1, T2, T3> unityEvent)
            {
                unityEvent.AddListener(call);
            }
        }
        
        public void RemoveListener<T1, T2, T3>(string key, UnityAction<T1, T2, T3> call)
        {
            if (string.IsNullOrEmpty(key) || call == null)
                return;

            UnityEventBase eventBase = null;
            if (!_actions.TryGetValue(key, out eventBase))
            {
                return;
            }

            if (eventBase is UnityEvent<T1, T2, T3> unityEvent)
            {
                unityEvent.RemoveListener(call);
            }
        }

        public void AddListener<T1, T2, T3, T4>(string key, UnityAction<T1, T2, T3, T4> call)
        {
            if (string.IsNullOrEmpty(key) || call == null)
                return;

            UnityEventBase eventBase = null;
            if (!_actions.TryGetValue(key, out eventBase))
            {
                eventBase = new MessageEvent<T1,T2,T3,T4>();
                _actions.Add(key, eventBase);
            }

            if (eventBase is UnityEvent<T1, T2, T3, T4> unityEvent)
            {
                unityEvent.AddListener(call);
            }
        }
        
        public void RemoveListener<T1, T2, T3, T4>(string key, UnityAction<T1, T2, T3, T4> call)
        {
            if (string.IsNullOrEmpty(key) || call == null)
                return;

            UnityEventBase eventBase = null;
            if (!_actions.TryGetValue(key, out eventBase))
            {
                return;
            }

            if (eventBase is UnityEvent<T1, T2, T3, T4> unityEvent)
            {
                unityEvent.RemoveListener(call);
            }
        }


        public void Invoke(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;
            
            UnityEventBase eventBase = null;
            if (_actions.TryGetValue(key, out eventBase))
            {
                if (eventBase is UnityEvent unityEvent)
                {
                    unityEvent.Invoke();
                }
            }
        }
        
        public void Invoke<T0>(string key, T0 arg0)
        {
            if (string.IsNullOrEmpty(key))
                return;

            UnityEventBase eventBase = null;
            if (_actions.TryGetValue(key, out eventBase))
            {
                if (eventBase is UnityEvent<T0> unityEvent)
                {
                    unityEvent.Invoke(arg0);
                }
            }
        }
        
        public void Invoke<T0,T1>(string key, T0 arg0, T1 arg1)
        {
            if (string.IsNullOrEmpty(key))
                return;

            UnityEventBase eventBase = null;
            if (_actions.TryGetValue(key, out eventBase))
            {
                if (eventBase is UnityEvent<T0,T1> unityEvent)
                {
                    unityEvent.Invoke(arg0,arg1);
                }
            }
        }
        
        public void Invoke<T0,T1,T2>(string key, T0 arg0, T1 arg1, T2 arg2)
        {
            if (string.IsNullOrEmpty(key))
                return;

            UnityEventBase eventBase = null;
            if (_actions.TryGetValue(key, out eventBase))
            {
                if (eventBase is UnityEvent<T0,T1,T2> unityEvent)
                {
                    unityEvent.Invoke(arg0,arg1,arg2);
                }
            }
        }
        
        public void Invoke<T0,T1,T2,T3>(string key, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            if (string.IsNullOrEmpty(key))
                return;

            UnityEventBase eventBase = null;
            if (_actions.TryGetValue(key, out eventBase))
            {
                if (eventBase is UnityEvent<T0,T1,T2,T3> unityEvent)
                {
                    unityEvent.Invoke(arg0,arg1,arg2,arg3);
                }
            }
        }
    }

}
