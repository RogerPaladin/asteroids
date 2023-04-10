using Static.Catalogs;
using UnityEngine;
using Utils;
using Utils.Collisions;
using Utils.MovementObserver;
using Utils.Reactivity;

namespace Model.Player
{
	public class PlayerShipModel : ModelMovementObservable, IModel, IActivateDeactivate
	{
		private readonly PlayerDataCatalog _playerDataCatalog;
		private Vector2 _projectileLocalSpawnPoint;

		public float Acceleration => _playerDataCatalog.Acceleration;
		public float DeAcceleration => _playerDataCatalog.DeAcceleration;
		public float MaxSpeed => _playerDataCatalog.MaxSpeed;
		public float MaxRotationSpeed => _playerDataCatalog.MaxRotationSpeed;

		public bool IsHaveCollision { get; private set; }
		public ICollisionDetector CurrentCollision { get; private set; }

		public Vector2 ProjectileSpawnPosition => Position.Value + (Vector2)(Rotation.Value * _projectileLocalSpawnPoint);
		
		public Observable<bool> IsActive { get; } = new Observable<bool>(false);

		public PlayerShipModel(PlayerDataCatalog playerDataCatalog)
		{
			_playerDataCatalog = playerDataCatalog;
		}

		public void SetProjectileSpawnPoint(Vector2 projectileLocalSpawnPoint)
		{
			_projectileLocalSpawnPoint = projectileLocalSpawnPoint;
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