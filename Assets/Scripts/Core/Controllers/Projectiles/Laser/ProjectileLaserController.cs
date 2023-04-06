using Core.Controllers.ViewPort;
using Model.Projectiles.Laser;
using UnityEngine;
using Utils.Events;

namespace Controllers.Projectiles.Laser
{
	public class ProjectileLaserController: AbstractProjectileController
	{
		private ProjectileLaserModel Model => base.Model as ProjectileLaserModel;

		public ProjectileLaserController(ProjectileLaserModel model, ViewPortController viewPortController, UpdateSystem updateSystem) : base(model, viewPortController, updateSystem)
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