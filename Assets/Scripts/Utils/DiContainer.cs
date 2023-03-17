using System;
using System.Collections.Generic;

namespace Assets.Scripts.Utils
{
	public class DiContainer
	{
		private readonly Dictionary<Type, object> _containers = new Dictionary<Type, object>();
		
		public T Register<T>(T instance)
		{
			_containers.Add(typeof(T), instance);
			return instance;
		}
		
		public T Resolve<T>()
		{
			if (_containers.TryGetValue(typeof(T), out object instance))
				return (T) instance;

			throw new Exception($"[DiContainer] Dependency {typeof(T)} not found");
		}
	}
}