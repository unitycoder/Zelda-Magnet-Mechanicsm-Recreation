using UnityEngine;
using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Core.ServiceSystem
{
    public static class ServiceProvider
    {
        private static Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        public static T Get<T>() where T : class, IService, new()
        {
            if (_services.ContainsKey(typeof(T)))
            {
                return (T) _services[typeof(T)];
            }

            if (typeof(T).IsSubclassOf(typeof(Object)))
            {
               Object o =  GameObject.FindObjectOfType(typeof(T));

               T val = o as T;

               _services.Add(typeof(T), val);
               
               return val;
            }
            
            _services.Add(typeof(T), new T());

            return (T) _services[typeof(T)];
        }
    }
}