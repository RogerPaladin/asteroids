using Assets.Scripts.Utils.MovementObserver;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Effects
{
	public class EffectView: ViewMovementObserver
	{
		public void OnActivate()
		{
			gameObject.SetActive(true);
		}

		public void OnDeactivate()
		{
			gameObject.SetActive(false);
		}
	}
}