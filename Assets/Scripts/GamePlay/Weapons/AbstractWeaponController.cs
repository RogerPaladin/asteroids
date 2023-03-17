using System;
using Assets.Scripts.Events;
using Assets.Scripts.GamePlay.Player;
using Assets.Scripts.GamePlay.Projectiles;
using Assets.Scripts.Utils;

namespace Assets.Scripts.GamePlay.Weapons
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
			Model.OnActivate();
		}
		
		public void Deactivate()
		{
			Model.TimerSystem.RemoveListener(this);
			Model.OnDeactivate();
		}

		public void OnLevelEnd()
		{
			_projectilesSpawner.OnLevelEnd();
		}

		public void OnTimer()
		{
			if (Model.HaveUnlimitedAmmo)
				return;

			if (Model.AmmoCount < Model.AmmoMaxCount)
			{
				if (Model.CurrentRefreshTimeLeft == 0)
					Model.SetRefreshTime(Model.AmmoRefreshTime);
				else
				{
					var tmpTime = Math.Max(0, Model.CurrentRefreshTimeLeft - 1);

					if (tmpTime == 0)
					{
						Model.SetAmmo(Model.AmmoCount + 1);

						if (Model.AmmoCount < Model.AmmoMaxCount)
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
			
			_projectilesSpawner.Spawn(Model.Config, _playerShipModel.ProjectileSpawnPosition, _playerShipModel.Rotation);
			
			if (Model.HaveUnlimitedAmmo)
				return;
			
			Model.SetAmmo(Model.AmmoCount - 1);
			
			if (Model.AmmoRefreshTime == 0)
				Model.SetRefreshTime(Model.AmmoRefreshTime);
		}

		public void Dispose()
		{
			_projectilesSpawner.Dispose();
		}
	}
}