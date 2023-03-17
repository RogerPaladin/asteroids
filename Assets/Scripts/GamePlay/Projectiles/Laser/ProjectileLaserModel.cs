using System.Collections.Generic;
using Assets.Scripts.Events;
using Assets.Scripts.GamePlay.Collisions;
using Assets.Scripts.GamePlay.Projectiles.Laser;
using Assets.Scripts.Static.Weapons;
using Assets.Scripts.Utils.OffScreenChecker;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Projectiles
{
	public class ProjectileLaserModel: ProjectileModel, IProjectileLaserModelObservable
	{
		public Vector2 GrowSize { get; private set; }
		public float GrowTime { get; private set; }

		public bool NeedGrow => GrowTime > 0;
		public float GrowSpeed => Config.ProjectileGrowSpeed;
		
		private List<IProjectileLaserViewObserver> _laserViewObservers = new List<IProjectileLaserViewObserver>();

		public ProjectileLaserModel(WeaponConfig config, UpdateSystem updateSystem, OffScreenCheckerTeleport offScreenChecker, CollisionChecker collisionChecker) : base(config, updateSystem, offScreenChecker, collisionChecker)
		{
		}

		public override void OnActivate()
		{
			base.OnActivate();
			
			GrowSize = Vector2.zero;
			GrowTime = Config.ProjectileGrowTime;
		}
		
		public void RegisterLaserObserver(IProjectileLaserViewObserver o) { _laserViewObservers.Add(o); }
		public void RemoveLaserObserver(IProjectileLaserViewObserver o) { _laserViewObservers.Remove(o); }
		
		public void SetGrowSize(Vector2 growSize)
		{
			if (GrowSize == growSize)
				return;
			
			GrowSize = growSize;
			NotifyGrowSizeChanged();
		}

		public void SetGrowTime(float growTime)
		{
			GrowTime = growTime;
		}

		public void NotifyGrowSizeChanged()
		{
			foreach(IProjectileLaserViewObserver lv in _laserViewObservers)
				lv.OnGrowChange(GrowSize);
		}
	}
}