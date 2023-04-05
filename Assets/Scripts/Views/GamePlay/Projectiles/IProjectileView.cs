using UnityEngine;
using Utils.MovementObserver;

namespace Views.GamePlay.Projectiles
{
	public interface IProjectileView: IMovementObserver
	{
		Collider2D Collider => null;
	}
}