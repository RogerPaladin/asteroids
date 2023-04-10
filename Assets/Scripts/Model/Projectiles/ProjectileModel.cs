using Static.Catalogs;
using Utils;
using Utils.Collisions;
using Utils.MovementObserver;
using Utils.Reactivity;

namespace Model.Projectiles
{
	public class ProjectileModel: ModelMovementObservable, IModel, IActivateDeactivate
	{
		public readonly WeaponDataCatalog WeaponDataCatalog;

		public float LifeTime { get; private set; }
		
		public float Speed => WeaponDataCatalog.ProjectileSpeed;
		public string ModelId => WeaponDataCatalog.Type.ToString();
		public bool NeedDestroyOnCollision => WeaponDataCatalog.ProjectileNeedDestroyOnCollision;
		
		public bool IsHaveCollision { get; private set; }
		public ICollisionDetector CurrentCollision { get; private set; }
		
		public Observable<bool> IsActive { get; } = new Observable<bool>(false);

		public ProjectileModel(WeaponDataCatalog weaponDataCatalog)
		{
			WeaponDataCatalog = weaponDataCatalog;
		}

		public virtual void Activate()
		{
			LifeTime = WeaponDataCatalog.ProjectileLifeTime;

			IsActive.Value = true;
		}
		
		public virtual void Deactivate()
		{
			IsActive.Value = false;
		}

		public void SetLifeTime(float lifeTime)
		{
			LifeTime = lifeTime;
		}
		
		public void SetCollision(bool val, ICollisionDetector collisionDetector)
		{
			IsHaveCollision = val;
			CurrentCollision = collisionDetector;
		}

		public void RemoveCollision()
		{
			IsHaveCollision = false;
			CurrentCollision = null;
		}
	}
}