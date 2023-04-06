using System;
using Core.Controllers.ViewPort;
using Model.Enemies;
using Utils;
using Utils.Events;
using UnityEngine;

namespace Controllers.Enemies
{
	public abstract class AbstractEnemyController : IUpdateListener, IActivateDeactivate
	{
		public EnemyModel Model { get; private set; }
		protected ViewPortController ViewPortController { get; }

		public event Action<AbstractEnemyController> OnDestroyEvent;
		
		public AbstractEnemyController(EnemyModel model, ViewPortController viewPortController)
		{
			Model = model;
			ViewPortController = viewPortController;
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

		public abstract void Update(float deltaTime);

		public void SetPosition(Vector2 pos)
		{
			Model.SetPosition(pos);
		}
		
		public void SetRotation(Quaternion rotation)
		{
			Model.SetRotation(rotation);
		}

		public void OnCollision()
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