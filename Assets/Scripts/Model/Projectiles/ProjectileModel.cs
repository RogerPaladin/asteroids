using Static.Weapons;
using Utils;
using Utils.Collisions;
using Utils.MovementObserver;
using Utils.Reactivity;

namespace Model.Projectiles
{
	public class ProjectileModel: ModelMovementObservable, IModel, IActivateDeactivate
	{
		protected readonly WeaponConfig Config;

		public float LifeTime { get; private set; }
		
		public float Speed => Config.ProjectileSpeed;
		public string ModelId => Config.ModelId;
		public bool NeedDestroyOnCollision => Config.ProjectileDestroyOnCollision;
		
		public bool IsHaveCollision { get; private set; }
		public ICollisionDetector CurrentCollision { get; private set; }
		
		public Observable<bool> IsActive { get; private set; } = new Observable<bool>(false);

		public ProjectileModel(WeaponConfig config)
		{
			Config = config;
		}

		public virtual void Activate()
		{
			LifeTime = Config.ProjectileLifeTime;

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