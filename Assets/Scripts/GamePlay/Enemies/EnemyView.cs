using System;
using Assets.Scripts.GamePlay.Collisions;
using Assets.Scripts.Utils.MovementObserver;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Enemies
{
	public class EnemyView: ViewMovementObserver, ICollisionDetector
	{
		public Collider2D Collider;

		public event Action OnCollisionEvent;

		public void OnCollision()
		{
			OnCollisionEvent?.Invoke();
		}
		
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