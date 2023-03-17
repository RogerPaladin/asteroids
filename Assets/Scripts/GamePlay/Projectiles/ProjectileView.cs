using Assets.Scripts.Utils.MovementObserver;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Projectiles
{
	public class ProjectileView: ViewMovementObserver
	{
		public Collider2D Collider;

		public virtual void OnActivate()
		{
			gameObject.SetActive(true);
		}

		public virtual void OnDeactivate()
		{
			gameObject.SetActive(false);
		}
	}
}