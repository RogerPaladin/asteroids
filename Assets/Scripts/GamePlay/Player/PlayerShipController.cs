using System;
using Assets.Scripts.Events;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Player
{
	public class PlayerShipController : IUpdateListener, IActivateDeactivate
	{
		public PlayerShipModel Model { get; private set; }
		public PlayerShipView View { get; private set; }

		public event Action OnDestroyEvent;
		
		public PlayerShipController(PlayerShipModel model, PlayerShipView view)
		{
			Model = model;
			View = view;
		}

		public void Activate()
		{
			if (Model.IsActive)
				return;
			
			Model.RegisterObserver(View);
			Model.OnActivate();
			Model.UpdateSystem.AddListener(this);
			View.OnActivate();
		}

		public void Deactivate()
		{
			if (!Model.IsActive)
				return;
			
			Model.RemoveObserver(View);
			Model.OnDeactivate();
			Model.UpdateSystem.RemoveListener(this);
			View.OnDeactivate();
		}
		
		public void OnLevelStart()
		{
			Activate();
		}
		
		public void OnLevelEnd()
		{
			Deactivate();
			Model.OnLevelEnd();
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

			Model.SetRotation(Model.Rotation * angle);

			Vector2 forward = Model.Rotation * Vector2.up;
			
			var velocity = Model.Velocity;

			if (Model.InputController.Thrust > 0)
				velocity += Model.Acceleration * deltaTime * forward;
			else
				velocity += Model.DeAcceleration * deltaTime * -Model.Velocity;

			velocity = Vector3.ClampMagnitude(velocity, Model.MaxSpeed);

			Model.SetVelocity(velocity);

			var pos = Model.Position + Model.Velocity * deltaTime;
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
				Model.WeaponFirst.Shoot();

			if (Model.InputController.NeedShootSecondWeapon)
				Model.WeaponSecond.Shoot();
		}
		
		private void OnDestroy()
		{
			OnDestroyEvent?.Invoke();
		}
	}
}