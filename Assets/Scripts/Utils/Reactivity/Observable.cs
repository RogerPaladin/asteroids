using System;

namespace Utils.Reactivity
{
	public class Observable<T>
	{
		private T _value;
		
		public T Value
		{
			get => _value;
			set
			{
				_value = value;
				Changed?.Invoke(_value);
			}
		}

		public event Action<T> Changed; 

		public Observable(T obj)
		{
			_value = obj;
		}
	}
}