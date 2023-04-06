using UnityEngine;
using Utils.MovementObserver;

namespace Views.GamePlay.Projectiles
{
	public interface IProjectileView: IView, IMovementObserver
	{
		Collider2D Collider => null;
	}
}