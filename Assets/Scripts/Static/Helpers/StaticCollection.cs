using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Static.Helpers
{
    public class StaticCollection<T>
    {
        public Dictionary<int, T> All = new Dictionary<int, T>();

        public StaticCollection(JToken token)
        {
            try
            {
                All = token != null ? token.ToObject<Dictionary<int, T>>() : new Dictionary<int, T>();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }
        
        public StaticCollection(Dictionary<int, T> all)
        {
            All = all ?? new Dictionary<int, T>();
        }

        public T this[int id] => Get(id);

        public T Get(int id)
		{
			return All.TryGetValue(id, out T result) ? result : default(T);
		}
	}
}
