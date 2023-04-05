using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Spawner
{
	public abstract class AbstractSpawner<T, C>: IDisposable where T : IActivateDeactivate
	{
		protected readonly Dictionary<string, Pool<T>> _pools = new Dictionary<string, Pool<T>>();
		protected readonly Dictionary<string, HashSet<T>> _active = new Dictionary<string, HashSet<T>>();
		
		public event Action<T> OnObjDestroyEvent;
		
		protected abstract string GetKey(T obj);
		
		public abstract T Spawn(C config, Vector2 pos, Quaternion rotation);

		protected virtual void OnObjDestroy(T obj)
		{
			ReturnToPool(obj);
			
			OnObjDestroyEvent?.Invoke(obj);
		}
		
		protected void ReturnToPool(T obj)
		{
			var key = GetKey(obj);
			Pool<T> pool = GetPoolByKey(key);
			HashSet<T> activeList = GetActiveListByKey(key);
			
			pool.Add(obj);
			activeList.Remove(obj);
			obj.Deactivate();
		}
		
		protected void ReturnToPool(HashSet<T> list)
		{
			foreach (var obj in list)
			{
				var key = GetKey(obj);
				Pool<T> pool = GetPoolByKey(key);
				pool.Add(obj);
				obj.Deactivate();
			}

			list.Clear();
		}
		
		protected Pool<T> GetPoolByKey(string key)
		{
			Pool<T> pool = null;
			
			if (_pools.ContainsKey(key))
				pool = _pools[key];
			else
			{
				pool = new Pool<T>();
				_pools[key] = pool;
			}
			
			return pool;
		}
		
		protected HashSet<T> GetActiveListByKey(string key)
		{
			HashSet<T> list = null;
			
			if (_active.ContainsKey(key))
				list = _active[key];
			else
			{
				list = new HashSet<T>();
				_active[key] = list;
			}

			return list;
		}
		
		protected void ClearPool()
		{
			foreach (var pool in _pools.Values)
				pool.Clear();
		}
		
		protected void ClearActive()
		{
			foreach (var list in _active.Values)
				list.Clear();
		}

		protected void ReturnAllActiveToPool()
		{
			foreach (var list in _active.Values)
				ReturnToPool(list);
		}
		
		public void Dispose()
		{
			ClearPool();
			ClearActive();
		}
	}
}