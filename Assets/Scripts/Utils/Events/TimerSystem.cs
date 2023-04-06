using System.Collections.Generic;
using Utils.DiContainers;

namespace Utils.Events
{
	public class TimerSystem
	{
		private HashSet<ITimerListener> _listeners = new HashSet<ITimerListener>();
		
		public void AddListener(ITimerListener listener)
		{
			_listeners.Add(listener);
		}

		public void RemoveListener(ITimerListener listener)
		{
			_listeners.Remove(listener);
		}

		public void Clear()
		{
			_listeners.Clear();
		}
		
		public void OnTimer() 
		{
			foreach (var iUpdatable in _listeners)
				iUpdatable.OnTimer();
		}
	}
}