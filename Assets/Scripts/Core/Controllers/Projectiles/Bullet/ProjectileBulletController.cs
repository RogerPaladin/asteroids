using Model.Projectiles;
using UnityEngine;

namespace Controllers.Projectiles.Bullet
{
	public class ProjectileBulletController: AbstractProjectileController
	{
		
		public ProjectileBulletController(ProjectileModel model) : base(model)
		{

		}

		protected override void ApplyMovement(float deltaTime)
		{
			Vector2 forward = Model.Rotation.Value * Vector2.up * Model.Speed;
			
			var pos = Model.Position.Value + forward * deltaTime;
			Model.OffScreenChecker.CheckPosition(ref pos);
			Model.SetPosition(pos);
		}
	}
}