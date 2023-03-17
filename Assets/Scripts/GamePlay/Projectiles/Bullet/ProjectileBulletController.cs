using UnityEngine;

namespace Assets.Scripts.GamePlay.Projectiles.Bullet
{
	public class ProjectileBulletController: AbstractProjectileController
	{
		
		public ProjectileBulletController(ProjectileModel model, ProjectileView view) : base(model, view)
		{

		}

		protected override void ApplyMovement(float deltaTime)
		{
			Vector2 forward = Model.Rotation * Vector2.up * Model.Speed;
			
			var pos = Model.Position + forward * deltaTime;
			Model.OffScreenChecker.CheckPosition(ref pos);
			Model.SetPosition(pos);
		}
	}
}