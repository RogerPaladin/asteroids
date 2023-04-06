using UnityEngine;
using Utils.MovementObserver;

namespace Views.GamePlay.Player
{
	public interface IPlayerShipView : IView, IMovementObserver
	{
		public Collider2D Collider => null;
		public Transform ProjectileSpawnPoint => null;
	}
}