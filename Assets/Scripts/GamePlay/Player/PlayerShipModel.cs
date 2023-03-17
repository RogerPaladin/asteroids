using Assets.Scripts.Events;
using Assets.Scripts.GamePlay.Collisions;
using Assets.Scripts.GamePlay.Input;
using Assets.Scripts.GamePlay.Weapons;
using Assets.Scripts.Static.Player;
using Assets.Scripts.Utils.MovementObserver;
using Assets.Scripts.Utils.OffScreenChecker;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Player
{
	public class PlayerShipModel : ModelMovementObservable
	{
		private readonly PlayerConfig _config;
		private readonly CollisionChecker _collisionChecker;
		private readonly Vector2 _projectileLocalSpawnPoint;

		public InputController InputController { get; private set; }
		
		public AbstractWeaponController WeaponFirst { get; private set; }
		public AbstractWeaponController WeaponSecond { get; private set; }
		
		public UpdateSystem UpdateSystem { get; private set; }
		public OffScreenCheckerTeleport OffScreenChecker { get; private set; }
		
		public bool IsActive { get; private set; }

		public float Acceleration => _config.Acceleration;
		public float DeAcceleration => _config.DeAcceleration;
		public float MaxSpeed => _config.MaxSpeed;
		public float MaxRotationSpeed => _config.MaxRotationSpeed;

		public bool IsHaveCollision => _collisionChecker.IsHaveCollision;

		public Vector2 ProjectileSpawnPosition => Position + (Vector2)(Rotation * _projectileLocalSpawnPoint);

		public PlayerShipModel(PlayerConfig config, 
							   InputController inputController,
							   UpdateSystem updateSystem,
							   CollisionChecker collisionChecker,
							   OffScreenCheckerTeleport offScreenChecker,
							   Vector2 projectileLocalSpawnPoint)
		{
			_config = config;
			InputController = inputController;
			_collisionChecker = collisionChecker;

			UpdateSystem = updateSystem;
			OffScreenChecker = offScreenChecker;
			_projectileLocalSpawnPoint = projectileLocalSpawnPoint;
		}

		public PlayerShipModel SetWeaponFirst(AbstractWeaponController weapon)
		{
			WeaponFirst = weapon;
			return this;
		}
		
		public PlayerShipModel SetWeaponSecond(AbstractWeaponController weapon)
		{
			WeaponSecond = weapon;
			return this;
		}
		
		public void OnActivate()
		{
			SetPosition(Vector2.zero);
			SetRotation(Quaternion.identity);
			SetVelocity(Vector2.zero);
			
			WeaponFirst?.Activate();
			WeaponSecond?.Activate();

			IsActive = true;
		}
		
		public void OnDeactivate()
		{
			WeaponFirst?.Deactivate();
			WeaponSecond?.Deactivate();

			IsActive = false;
		}

		public void OnLevelEnd()
		{
			WeaponFirst?.OnLevelEnd();
			WeaponSecond?.OnLevelEnd();
		}
	}
}