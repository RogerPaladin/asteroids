using System;
using Core.Controllers.ViewPort;
using Model.Projectiles;
using Utils;
using Utils.Events;
using UnityEngine;
using Utils.Collisions;

namespace Controllers.Projectiles
{
	public abstract class AbstractProjectileController: IUpdateListener, IActivateDeactivate
	{
		protected readonly ViewPortController _viewPortController;
		private readonly UpdateSystem _updateSystem;
		public ProjectileModel Model { get; }

		public event Action<AbstractProjectileController> OnDestroyEvent;
		
		public AbstractProjectileController(ProjectileModel model, ViewPortController viewPortController, UpdateSystem updateSystem)
		{
			_viewPortController = viewPortController;
			_updateSystem = updateSystem;
			Model = model;
		}

		public virtual void Activate()
		{
			Model.Activate();
			_updateSystem.AddListener(this);
		}

		public virtual void Deactivate()
		{
			Model.Deactivate();
			_updateSystem.RemoveListener(this);
		}
		
		public void Update(float deltaTime)
		{
			ApplyMovement(deltaTime);
			
			if (CheckCollisions())
				return;

			CheckLifeTime(deltaTime);
		}

		public void SetPosition(Vector2 pos)
		{
			Model.SetPosition(pos);
		}
		
		public void SetRotation(Quaternion rotation)
		{
			Model.SetRotation(rotation);
		}
		
		public void OnCollision(bool val, ICollisionDetector collisionDetector = null)
		{
			Model.SetCollision(val, collisionDetector);
		}
		
		protected abstract void ApplyMovement(float deltaTime);
		
		private bool CheckCollisions()
		{
			if (Model.IsHaveCollision)
			{
				Model.CurrentCollision?.OnCollision();
				
				if (Model.NeedDestroyOnCollision)
				{
					OnDestroy();
					
					Model.RemoveCollision();
					
					return true;
				}
				
				Model.RemoveCollision();
			}
			
			return false;
		}

		private void CheckLifeTime(float deltaTime)
		{
			Model.SetLifeTime(Model.LifeTime - deltaTime);

			if (Model.LifeTime <= 0)
				OnLifeTimeEnd();
		}

		private void OnLifeTimeEnd()
		{
			OnDestroy();
		}
		
		private void OnDestroy()
		{
			Deactivate();
			
			OnDestroyEvent?.Invoke(this);
		}
	}
}