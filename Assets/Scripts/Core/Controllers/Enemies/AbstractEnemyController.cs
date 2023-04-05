using System;
using Model.Enemies;
using Utils;
using Utils.Events;
using UnityEngine;

namespace Controllers.Enemies
{
	public abstract class AbstractEnemyController : IUpdateListener, IActivateDeactivate
	{
		public EnemyModel Model { get; private set; }

		public event Action<AbstractEnemyController> OnDestroyEvent;
		
		public AbstractEnemyController(EnemyModel model)
		{
			Model = model;
		}
		
		public void SetParent(Transform t)
		{
			Model.SetParent(t);
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