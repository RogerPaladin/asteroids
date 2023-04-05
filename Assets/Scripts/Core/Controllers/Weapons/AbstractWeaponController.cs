using System;
using Factories.Projectiles;
using Model.Player;
using Model.Weapons;
using Utils;
using Utils.Events;

namespace Controllers.Weapons
{
	public abstract class AbstractWeaponController : ITimerListener, IDisposable, IActivateDeactivate
	{
		public WeaponModel Model { get; private set; }
		private readonly PlayerShipModel _playerShipModel;
		private readonly ProjectilesSpawner _projectilesSpawner;
		
		public AbstractWeaponController(WeaponModel model, PlayerShipModel playerShipModel, ProjectilesSpawner projectilesSpawner)
		{
			Model = model;
			_playerShipModel = playerShipModel;
			_projectilesSpawner = projectilesSpawner;
		}

		public void Activate()
		{
			Model.TimerSystem.AddListener(this);
			Model.Activate();
		}
		
		public void Deactivate()
		{
			Model.TimerSystem.RemoveListener(this);
			Model.Deactivate();
		}

		public void OnLevelEnd()
		{
			_projectilesSpawner.OnLevelEnd();
		}

		public void OnTimer()
		{
			if (Model.HaveUnlimitedAmmo)
				return;

			if (Model.AmmoCount.Value < Model.AmmoMaxCount)
			{
				if (Model.CurrentRefreshTimeLeft.Value == 0)
					Model.SetRefreshTime(Model.AmmoRefreshTime);
				else
				{
					var tmpTime = Math.Max(0, Model.CurrentRefreshTimeLeft.Value - 1);

					if (tmpTime == 0)
					{
						Model.SetAmmo(Model.AmmoCount.Value + 1);

						if (Model.AmmoCount.Value < Model.AmmoMaxCount)
							tmpTime = Model.AmmoRefreshTime;
					}
					
					Model.SetRefreshTime(tmpTime);
				}
			}
		}

		public void Shoot()
		{
			if (!Model.CanShoot)
				return;
			
			_projectilesSpawner.Spawn(Model.Config, _playerShipModel.ProjectileSpawnPosition, _playerShipModel.Rotation.Value);
			
			if (Model.HaveUnlimitedAmmo)
				return;
			
			Model.SetAmmo(Model.AmmoCount.Value - 1);
			
			if (Model.AmmoRefreshTime == 0)
				Model.SetRefreshTime(Model.AmmoRefreshTime);
		}

		public void Dispose()
		{
			_projectilesSpawner.Dispose();
		}
	}
}