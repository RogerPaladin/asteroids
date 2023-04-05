using Model.Projectiles.Laser;
using UnityEngine;

namespace Controllers.Projectiles.Laser
{
	public class ProjectileLaserController: AbstractProjectileController
	{
		private ProjectileLaserModel Model => base.Model as ProjectileLaserModel;

		public ProjectileLaserController(ProjectileLaserModel model) : base(model)
		{
		}

		protected override void ApplyMovement(float deltaTime)
		{
			var grow = Vector2.zero;

			if (Model.NeedGrow)
			{
				grow = Vector2.up * Model.GrowSpeed * deltaTime;
				Model.SetGrowTime(Model.GrowTime - deltaTime);
			}
			else
			{
				Vector2 forward = Model.Rotation.Value * Vector2.up * Model.Speed;

				var pos = Model.Position.Value + forward * deltaTime;
				Model.SetPosition(pos);
			}
			
			Model.SetGrowSize(grow);
		}
	}
}