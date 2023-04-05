using System;
using System.Collections;
using UnityEngine;

namespace Utils.Clock
{
	public class ClockBehaviour: MonoBehaviour
	{
		public event Action<float> OnFrameUpdate;
		public event Action OnSecondUpdate;
		
		private IEnumerator Start() 
		{
			while (true) 
			{
				yield return new WaitForSeconds(1f);
				OnTimer();
			}
		}
		
		private void Update()
		{
			OnFrameUpdate?.Invoke(Time.deltaTime);
		}
		
		private void OnTimer() 
		{
			OnSecondUpdate?.Invoke();
		}
	}
}