using System;
using Controllers.Weapons;
using Core.Controllers.ViewPort;
using Model.Player;
using Utils;
using Utils.Events;
using UnityEngine;
using Utils.Collisions;
using Utils.Input;

namespace Controllers.Player
{
	public class PlayerShipController : IUpdateListener, IActivateDeactivate
	{
		private readonly InputController _inputController;
		private readonly UpdateSystem _updateSystem;
		private readonly ViewPortController _viewPortController;
		public PlayerShipModel Model { get; private set; }

		public AbstractWeaponController WeaponFirst { get; private set; }
		public AbstractWeaponController WeaponSecond { get; private set; }
		
		public event Action OnDestroyEvent;
		
		public PlayerShipController(PlayerShipModel model, InputController inputController, UpdateSystem updateSystem, ViewPortController viewPortController)
		{
			_inputController = inputController;
			_updateSystem = updateSystem;
			_viewPortController = viewPortController;
			Model = model;
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
			_updateSystem.AddListener(this);
			
			WeaponFirst?.Activate();
			WeaponSecond?.Activate();
		}

		public void Deactivate()
		{
			if (!Model.IsActive.Value)
				return;
			
			Model.Deactivate();
			_updateSystem.RemoveListener(this);
			
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
											 Model.MaxRotationSpeed * -_inputController.Rotation * deltaTime *
											 Mathf.Rad2Deg);

			Model.SetRotation(Model.Rotation.Value * angle);

			Vector2 forward = Model.Rotation.Value * Vector2.up;
			
			var velocity = Model.Velocity.Value;

			if (_inputController.Thrust > 0)
				velocity += Model.Acceleration * deltaTime * forward;
			else
				velocity += Model.DeAcceleration * deltaTime * -Model.Velocity.Value;

			velocity = Vector3.ClampMagnitude(velocity, Model.MaxSpeed);

			Model.SetVelocity(velocity);

			var pos = Model.Position.Value + Model.Velocity.Value * deltaTime;
			_viewPortController.CheckPosition(ref pos);
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
			if (_inputController.NeedShootFirstWeapon)
				WeaponFirst?.Shoot();
			
			if (_inputController.NeedShootSecondWeapon)
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