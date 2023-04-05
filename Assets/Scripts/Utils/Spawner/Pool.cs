using System.Collections.Generic;

namespace Utils.Spawner
{
	public class Pool<T>
	{
		private Stack<T> _pool = new Stack<T>();

		public T Get()
		{
			if (_pool.TryPop(out T result))
				return result;

			return default;
		}

		public void Add(T value)
		{
			_pool.Push(value);
		}

		public void Clear()
		{
			_pool.Clear();
		}
	}
}