using Controllers.Projectiles;
using Model.Projectiles;
using UnityEngine;
using Utils.MovementObserver;

namespace Views.GamePlay.Projectiles
{
	public interface IProjectileView: IView, IMovementObserver
	{
		Collider2D Collider => null;
		
		public void SetData(ProjectileModel model, AbstractProjectileController controller);
	}
}