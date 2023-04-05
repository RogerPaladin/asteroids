using System;
using Model.Projectiles;
using Utils;
using Utils.Events;
using UnityEngine;
using Utils.Collisions;

namespace Controllers.Projectiles
{
	public abstract class AbstractProjectileController: IUpdateListener, IActivateDeactivate
	{
		public ProjectileModel Model { get; private set; }

		public event Action<AbstractProjectileController> OnDestroyEvent;
		
		public AbstractProjectileController(ProjectileModel model)
		{
			Model = model;
		}

		public void SetParent(Transform transform)
		{
			Model.SetParent(transform);
		}
		
		public virtual void Activate()
		{
			Model.Activate();
			Model.UpdateSystem.AddListener(this);
		}

		public virtual void Deactivate()
		{
			Model.Deactivate();
			Model.UpdateSystem.RemoveListener(this);
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
		
		public void SetCollisionChecker(CollisionChecker collisionChecker)
		{
			Model.SetCollisionChecker(collisionChecker);
		}
		
		protected abstract void ApplyMovement(float deltaTime);
		
		private bool CheckCollisions()
		{
			if (Model.IsHaveCollision && Model.NeedDestroyOnCollision)
			{
				OnDestroy();
				return true;
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