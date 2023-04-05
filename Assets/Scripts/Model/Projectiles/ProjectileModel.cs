using Static.Weapons;
using UnityEngine;
using Utils;
using Utils.Collisions;
using Utils.Events;
using Utils.MovementObserver;
using Utils.OffScreenChecker;
using Utils.Reactivity;

namespace Model.Projectiles
{
	public class ProjectileModel: ModelMovementObservable, IModel, IActivateDeactivate
	{
		protected readonly WeaponConfig Config;
		private CollisionChecker _collisionChecker;

		public UpdateSystem UpdateSystem { get; private set; }
		public OffScreenCheckerTeleport OffScreenChecker { get; private set; }
		
		public float LifeTime { get; private set; }
		
		public float Speed => Config.ProjectileSpeed;
		public string ModelId => Config.ModelId;
		public bool NeedDestroyOnCollision => Config.ProjectileDestroyOnCollision;
		
		public bool IsHaveCollision => _collisionChecker.IsHaveCollision;
		
		public Observable<Transform> Parent { get; private set; } = new Observable<Transform>(null);
		public Observable<bool> IsActive { get; private set; } = new Observable<bool>(false);

		public ProjectileModel(WeaponConfig config,
							   UpdateSystem updateSystem,
							   OffScreenCheckerTeleport offScreenChecker,
							   CollisionChecker collisionChecker = null)
		{
			Config = config;
			UpdateSystem = updateSystem;
			OffScreenChecker = offScreenChecker;
			_collisionChecker = collisionChecker;
		}

		public void SetCollisionChecker(CollisionChecker collisionChecker)
		{
			_collisionChecker = collisionChecker;
		}
		
		public virtual void Activate()
		{
			LifeTime = Config.ProjectileLifeTime;
			
			Parent.Notify();
			
			Position.Notify();
			Rotation.Notify();
			Velocity.Notify();
			
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
		
		public void SetParent(Transform transform)
		{
			Parent.Value = transform;
		}
	}
}