using Static.Player;
using UnityEngine;
using Utils;
using Utils.Collisions;
using Utils.MovementObserver;
using Utils.Reactivity;

namespace Model.Player
{
	public class PlayerShipModel : ModelMovementObservable, IModel, IActivateDeactivate
	{
		private readonly PlayerConfig _config;
		private CollisionChecker _collisionChecker;//todo!!!! THINK
		private Vector2 _projectileLocalSpawnPoint;

		public float Acceleration => _config.Acceleration;
		public float DeAcceleration => _config.DeAcceleration;
		public float MaxSpeed => _config.MaxSpeed;
		public float MaxRotationSpeed => _config.MaxRotationSpeed;

		public bool IsHaveCollision => _collisionChecker?.IsHaveCollision ?? false;

		public Vector2 ProjectileSpawnPosition => Position.Value + (Vector2)(Rotation.Value * _projectileLocalSpawnPoint);
		
		public Observable<bool> IsActive { get; private set; } = new Observable<bool>(false);

		public PlayerShipModel(PlayerConfig config)
		{
			_config = config;
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
	}
}