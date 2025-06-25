using System;
using RealYou.Utility.BFException;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RealYou.Utility.Pool
{
    public class GameObjectPool : ObjectPool<GameObject>
    {
        
        private class InnerGameObject
        {
            
            public GameObject _prefab;

            public Transform _parent;

            public GameObject CreateGameObject()
            {
                return Object.Instantiate(_prefab, _parent);
            }

            public static void DestroyGameObject(GameObject go)
            {
                if(go == null)
                    return;
            
                Object.Destroy(go);
            }
        }
        
        private GameObjectPool(int capacity, Func<GameObject> create, Action<GameObject> destroy) : base(capacity, create, destroy)
        {
        }

        public override GameObject Create()
        {
            GameObject go = base.Create();
            if (go != null)
            {
                go.SetActive(true);
            }

            return go;
        }

        public override void Release(GameObject val)
        {
            if (val == null)
            {
                return;
            }
            
            val.SetActive(false);
            
            base.Release(val);
        }

        public static GameObjectPool CreatePool(GameObject prefab, Transform parent, int capacity)
        {
            if(prefab == null)
                throw new ParamsNullException();
            
            var gameObject = new InnerGameObject()
            {
                _prefab = prefab,
                _parent = parent
            };
            
            return new GameObjectPool(capacity,gameObject.CreateGameObject,InnerGameObject.DestroyGameObject);
            
        }
    }
}