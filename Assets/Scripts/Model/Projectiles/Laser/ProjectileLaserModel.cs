using Static.Catalogs;
using UnityEngine;
using Utils.Reactivity;

namespace Model.Projectiles.Laser
{
	public class ProjectileLaserModel: ProjectileModel
	{
		public Observable<Vector2> GrowSize { get; } = new Observable<Vector2>(Vector2.zero);
		public float GrowTime { get; private set; }

		public bool NeedGrow => GrowTime > 0;
		public float GrowSpeed => WeaponDataCatalog.ProjectileGrowSpeed;

		public ProjectileLaserModel(WeaponDataCatalog weaponDataCatalog) : base(weaponDataCatalog)
		{
		}

		public override void Activate()
		{
			base.Activate();
			
			GrowSize.Value = Vector2.zero;
			GrowTime = WeaponDataCatalog.ProjectileGrowTime;
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