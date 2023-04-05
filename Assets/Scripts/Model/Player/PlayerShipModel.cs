using Model.Weapons;
using Static.Player;
using Utils.Collisions;
using Utils.Events;
using Utils.MovementObserver;
using Utils.OffScreenChecker;
using UnityEngine;
using Utils;
using Utils.Input;
using Utils.Reactivity;

namespace Model.Player
{
	public class PlayerShipModel : ModelMovementObservable, IModel, IActivateDeactivate
	{
		private readonly PlayerConfig _config;
		private CollisionChecker _collisionChecker;
		private Vector2 _projectileLocalSpawnPoint;

		public InputController InputController { get; private set; }

		public UpdateSystem UpdateSystem { get; private set; }
		public OffScreenCheckerTeleport OffScreenChecker { get; private set; }

		public float Acceleration => _config.Acceleration;
		public float DeAcceleration => _config.DeAcceleration;
		public float MaxSpeed => _config.MaxSpeed;
		public float MaxRotationSpeed => _config.MaxRotationSpeed;

		public bool IsHaveCollision => _collisionChecker?.IsHaveCollision ?? false;

		public Vector2 ProjectileSpawnPosition => Position.Value + (Vector2)(Rotation.Value * _projectileLocalSpawnPoint);
		
		public Observable<Transform> Parent { get; private set; } = new Observable<Transform>(null);
		public Observable<bool> IsActive { get; private set; } = new Observable<bool>(false);

		public PlayerShipModel(PlayerConfig config, 
							   InputController inputController,
							   UpdateSystem updateSystem,
							   OffScreenCheckerTeleport offScreenChecker,
							   CollisionChecker collisionChecker = null,
							   Vector2 projectileLocalSpawnPoint = default)
		{
			_config = config;
			InputController = inputController;
			
			UpdateSystem = updateSystem;
			OffScreenChecker = offScreenChecker;
			_collisionChecker = collisionChecker;
			_projectileLocalSpawnPoint = projectileLocalSpawnPoint;
		}

		public void SetProjectileSpawnPoint(Vector2 projectileLocalSpawnPoint)
		{
			_projectileLocalSpawnPoint = projectileLocalSpawnPoint;
		}
		
		public void SetCollisionChecker(CollisionChecker collisionChecker)
		{
			_collisionChecker = collisionChecker;
		}

		public void Activate()
		{
			SetPosition(Vector2.zero);
			SetRotation(Quaternion.identity);
			SetVelocity(Vector2.zero);

			IsActive.Value = true;
		}
		
		public void Deactivate()
		{
			IsActive.Value = false;
		}

		public void OnLevelEnd()
		{

		}
		
		public void SetParent(Transform transform)
		{
			Parent.Value = transform;
		}
	}
}