using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Events
{
	public class UpdateSystem : MonoBehaviour
	{
		private List<IUpdateListener> _listeners = new List<IUpdateListener>();

		private int updates = 0;
		
		public void AddListener(IUpdateListener listener)
		{
			_listeners.Add(listener);
		}

		public void RemoveListener(IUpdateListener listener)
		{
			_listeners.Remove(listener);
		}
		
		private void Update()
		{
			updates++;
			
			for (int i = 0; i < _listeners.Count; i++)
			{
				_listeners[i].Update(Time.deltaTime);
			}
		}

		public void Clear()
		{
			_listeners.Clear();
		}
	}
}