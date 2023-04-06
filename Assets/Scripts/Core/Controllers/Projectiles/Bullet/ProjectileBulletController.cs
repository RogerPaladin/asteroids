using Core.Controllers.ViewPort;
using Model.Projectiles;
using UnityEngine;
using Utils.Events;

namespace Controllers.Projectiles.Bullet
{
	public class ProjectileBulletController: AbstractProjectileController
	{
		
		public ProjectileBulletController(ProjectileModel model, ViewPortController viewPortController, UpdateSystem updateSystem) : base(model, viewPortController, updateSystem)
		{

		}

		protected override void ApplyMovement(float deltaTime)
		{
			Vector2 forward = Model.Rotation.Value * Vector2.up * Model.Speed;
			
			var pos = Model.Position.Value + forward * deltaTime;
			_viewPortController.CheckPosition(ref pos);
			Model.SetPosition(pos);
		}
	}
}