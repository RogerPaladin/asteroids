using System;
using Controllers.Weapons;
using Model.Player;
using Utils;
using Utils.Events;
using UnityEngine;
using Utils.Collisions;

namespace Controllers.Player
{
	public class PlayerShipController : IUpdateListener, IActivateDeactivate
	{
		public PlayerShipModel Model { get; private set; }

		public AbstractWeaponController WeaponFirst { get; private set; }
		public AbstractWeaponController WeaponSecond { get; private set; }
		
		public event Action OnDestroyEvent;
		
		public PlayerShipController(PlayerShipModel model)
		{
			Model = model;
		}

		public void SetParent(Transform transform)
		{
			Model.SetParent(transform);
		}

		public PlayerShipController SetWeaponFirst(AbstractWeaponController weapon)
		{
			WeaponFirst = weapon;
			return this;
		}

		public PlayerShipController SetWeaponSecond(AbstractWeaponController weapon)
		{
			WeaponSecond = weapon;
			return this;
		}

		public void Activate()
		{
			if (Model.IsActive.Value)
				return;
			
			Model.Activate();
			Model.UpdateSystem.AddListener(this);
			
			WeaponFirst?.Activate();
			WeaponSecond?.Activate();
		}

		public void Deactivate()
		{
			if (!Model.IsActive.Value)
				return;
			
			Model.Deactivate();
			Model.UpdateSystem.RemoveListener(this);
			
			WeaponFirst?.Deactivate();
			WeaponSecond?.Deactivate();
		}
		
		public void OnLevelStart()
		{
			Activate();
		}
		
		public void OnLevelEnd()
		{
			Deactivate();
			
			Model.OnLevelEnd();
			
			WeaponFirst?.OnLevelEnd();
			WeaponSecond?.OnLevelEnd();
		}
		
		public void Update(float deltaTime)
		{
			ApplyMovement(deltaTime);

			if (CheckCollisions())
				return;

			CheckWeapons();
		}

		private void ApplyMovement(float deltaTime)
		{
			var angle =
							Quaternion.Euler(0f, 0f,
											 Model.MaxRotationSpeed * -Model.InputController.Rotation * deltaTime *
											 Mathf.Rad2Deg);

			Model.SetRotation(Model.Rotation.Value * angle);

			Vector2 forward = Model.Rotation.Value * Vector2.up;
			
			var velocity = Model.Velocity.Value;

			if (Model.InputController.Thrust > 0)
				velocity += Model.Acceleration * deltaTime * forward;
			else
				velocity += Model.DeAcceleration * deltaTime * -Model.Velocity.Value;

			velocity = Vector3.ClampMagnitude(velocity, Model.MaxSpeed);

			Model.SetVelocity(velocity);

			var pos = Model.Position.Value + Model.Velocity.Value * deltaTime;
			Model.OffScreenChecker.CheckPosition(ref pos);
			Model.SetPosition(pos);
		}

		private bool CheckCollisions()
		{
			if (Model.IsHaveCollision)
			{
				OnDestroy();
				return true;
			}
			
			return false;
		}

		private void CheckWeapons()
		{
			if (Model.InputController.NeedShootFirstWeapon)
				WeaponFirst?.Shoot();
			
			if (Model.InputController.NeedShootSecondWeapon)
				WeaponSecond?.Shoot();
		}
		
		private void OnDestroy()
		{
			OnDestroyEvent?.Invoke();
		}

		public void RemoveListeners()
		{
			
		}

		public void SetProjectileSpawnPoint(Vector3 position)
		{
			Model.SetProjectileSpawnPoint(position);
		}

		public void SetCollisionChecker(CollisionChecker collisionChecker)
		{
			Model.SetCollisionChecker(collisionChecker);
		}
	}
}