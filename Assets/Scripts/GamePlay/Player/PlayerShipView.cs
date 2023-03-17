using Assets.Scripts.Utils.MovementObserver;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Player
{
	public class PlayerShipView: ViewMovementObserver
	{
		public Collider2D Collider;
		public Transform ProjectileSpawnPoint;
		
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