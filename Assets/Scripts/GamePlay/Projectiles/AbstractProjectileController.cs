using System;
using Assets.Scripts.Events;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Projectiles
{
	public abstract class AbstractProjectileController: IUpdateListener, IActivateDeactivate
	{
		public ProjectileModel Model { get; private set; }
		public ProjectileView View { get; private set; }
		
		public event Action<AbstractProjectileController> OnDestroyEvent;
		
		public AbstractProjectileController(ProjectileModel model, ProjectileView view)
		{
			Model = model;
			View = view;
		}

		public virtual void Activate()
		{
			Model.RegisterObserver(View);
			Model.OnActivate();
			View.OnActivate();
			Model.UpdateSystem.AddListener(this);
		}

		public virtual void Deactivate()
		{
			Model.RemoveObserver(View);
			Model.OnDeactivate();
			View.OnDeactivate();
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