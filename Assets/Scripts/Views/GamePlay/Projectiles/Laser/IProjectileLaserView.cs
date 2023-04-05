using UnityEngine;

namespace Views.GamePlay.Projectiles.Laser
{
	public interface IProjectileLaserView : IProjectileView
	{
		void OnGrowChange(Vector2 grow);
	}
}