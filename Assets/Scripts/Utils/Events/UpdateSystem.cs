using System.Collections.Generic;

namespace Utils.Events
{
	public class UpdateSystem
	{
		private HashSet<IUpdateListener> _listeners = new HashSet<IUpdateListener>();
		private HashSet<IUpdateListener> _toAddListeners = new HashSet<IUpdateListener>();
		private HashSet<IUpdateListener> _toRemoveListeners = new HashSet<IUpdateListener>();

		public void AddListener(IUpdateListener listener)
		{
			_toRemoveListeners.Remove(listener);
			_toAddListeners.Add(listener);
		}

		public void RemoveListener(IUpdateListener listener)
		{
			_toAddListeners.Remove(listener);
			_toRemoveListeners.Add(listener);
		}

		public void Update(float deltaTime)
		{
			ApplyDifference();
			
			foreach (var updateListener in _listeners)
				updateListener.Update(deltaTime);
		}

		private void ApplyDifference()
		{
			_listeners.UnionWith(_toAddListeners);
			_listeners.ExceptWith(_toRemoveListeners);
			
			_toAddListeners.Clear();
			_toRemoveListeners.Clear();
		}
	}
}