using UnityEngine;

namespace Assets.Scripts.GamePlay.Projectiles.Laser
{
	public class ProjectileLaserController: AbstractProjectileController
	{
		private ProjectileLaserView _laserView => View as ProjectileLaserView;
		private ProjectileLaserModel _laserModel => Model as ProjectileLaserModel;
		
		public ProjectileLaserController(ProjectileLaserModel model, ProjectileLaserView view) : base(model, view)
		{
		}

		public override void Activate()
		{
			_laserModel.RegisterLaserObserver(_laserView);
			
			base.Activate();
		}
		
		public override void Deactivate()
		{
			_laserModel.RemoveLaserObserver(_laserView);
			
			base.Deactivate();
		}

		protected override void ApplyMovement(float deltaTime)
		{
			var grow = Vector2.zero;

			if (_laserModel.NeedGrow)
			{
				grow = Vector2.up * _laserModel.GrowSpeed * deltaTime;
				_laserModel.SetGrowTime(_laserModel.GrowTime - deltaTime);
			}
			else
			{
				Vector2 forward = Model.Rotation * Vector2.up * Model.Speed;

				var pos = Model.Position + forward * deltaTime;
				Model.SetPosition(pos);
			}
			
			_laserModel.SetGrowSize(grow);
		}
	}
}