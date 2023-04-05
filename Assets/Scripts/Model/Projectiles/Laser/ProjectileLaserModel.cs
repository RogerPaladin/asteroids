using Static.Weapons;
using Utils.Collisions;
using Utils.Events;
using Utils.OffScreenChecker;
using UnityEngine;
using Utils.Reactivity;

namespace Model.Projectiles.Laser
{
	public class ProjectileLaserModel: ProjectileModel
	{
		public Observable<Vector2> GrowSize { get; private set; } = new Observable<Vector2>(Vector2.zero);
		public float GrowTime { get; private set; }

		public bool NeedGrow => GrowTime > 0;
		public float GrowSpeed => Config.ProjectileGrowSpeed;

		public ProjectileLaserModel(WeaponConfig config, UpdateSystem updateSystem, OffScreenCheckerTeleport offScreenChecker) : base(config, updateSystem, offScreenChecker)
		{
		}

		public override void Activate()
		{
			base.Activate();
			
			GrowSize.Value = Vector2.zero;
			GrowTime = Config.ProjectileGrowTime;
		}

		public void SetGrowSize(Vector2 growSize)
		{
			if (GrowSize.Value == growSize)
				return;
			
			GrowSize.Value = growSize;
		}

		public void SetGrowTime(float growTime)
		{
			GrowTime = growTime;
		}
	}
}