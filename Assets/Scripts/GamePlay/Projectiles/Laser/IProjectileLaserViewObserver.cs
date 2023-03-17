using UnityEngine;

namespace Assets.Scripts.GamePlay.Projectiles.Laser
{
	public interface IProjectileLaserViewObserver
	{
		public void OnGrowChange(Vector2 grow);
	}
}