using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Events;
using UnityEngine;

namespace Assets.Scripts.Events
{
	public class TimerSystem: MonoBehaviour
	{
		private List<ITimerListener> _listeners = new List<ITimerListener>();
		
		public void AddListener(ITimerListener listener)
		{
			_listeners.Add(listener);
		}

		public void RemoveListener(ITimerListener listener)
		{
			_listeners.Remove(listener);
		}
		
		private IEnumerator Start() 
		{
			while (true) 
			{
				yield return new WaitForSeconds(1f);
				OnTimer();
			}
		}
		
		private void OnTimer() 
		{
			foreach (var iUpdatable in _listeners)
			{
				iUpdatable.OnTimer();
			}
		}

		public void Clear()
		{
			_listeners.Clear();
		}
	}
}