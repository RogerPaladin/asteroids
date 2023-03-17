using Assets.Scripts.Events;
using Assets.Scripts.GamePlay.Collisions;
using Assets.Scripts.Static.Weapons;
using Assets.Scripts.Utils.MovementObserver;
using Assets.Scripts.Utils.OffScreenChecker;

namespace Assets.Scripts.GamePlay.Projectiles
{
	public class ProjectileModel: ModelMovementObservable
	{
		protected readonly WeaponConfig Config;
		private readonly CollisionChecker _collisionChecker;

		public UpdateSystem UpdateSystem { get; private set; }
		public OffScreenCheckerTeleport OffScreenChecker { get; private set; }
		
		public float LifeTime { get; private set; }
		
		public float Speed => Config.ProjectileSpeed;
		public string ModelId => Config.ModelId;
		public bool NeedDestroyOnCollision => Config.ProjectileDestroyOnCollision;
		
		public bool IsHaveCollision => _collisionChecker.IsHaveCollision;

		public ProjectileModel(WeaponConfig config,
							   UpdateSystem updateSystem,
							   OffScreenCheckerTeleport offScreenChecker,
							   CollisionChecker collisionChecker)
		{
			Config = config;
			UpdateSystem = updateSystem;
			OffScreenChecker = offScreenChecker;
			_collisionChecker = collisionChecker;
		}

		public virtual void OnActivate()
		{
			LifeTime = Config.ProjectileLifeTime;
		}
		
		public virtual void OnDeactivate()
		{

		}

		public void SetLifeTime(float lifeTime)
		{
			LifeTime = lifeTime;
		}
	}
}