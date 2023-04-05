using System;
using System.Collections.Generic;

namespace Utils.DiContainers
{
	public class DiContainer
	{
		private readonly Dictionary<Type, IDiContainerChild> _containers = new Dictionary<Type, IDiContainerChild>();
		
		public T Register<T>(IDiContainerChild instance)
		{
			_containers.Add(typeof(T), instance);
			return (T) instance;
		}
		
		public T Resolve<T>()
		{
			if (_containers.TryGetValue(typeof(T), out IDiContainerChild instance))
				return (T) instance;

			throw new Exception($"[DiContainer] Dependency {typeof(T)} not found");
		}
	}
}