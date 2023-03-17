using System;
using Assets.Scripts.Events;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Enemies
{
	public abstract class AbstractEnemyController : IUpdateListener, IActivateDeactivate
	{
		public EnemyModel Model { get; private set; }
		public EnemyView View { get; private set; }

		public event Action<AbstractEnemyController> OnDestroyEvent;
		
		public AbstractEnemyController(EnemyModel model, EnemyView view)
		{
			Model = model;
			View = view;
			View.OnDeactivate();
		}
		
		public virtual void Activate()
		{
			Model.RegisterObserver(View);
			Model.OnActivate();
			Model.UpdateSystem.AddListener(this);
			View.OnActivate();
			View.OnCollisionEvent += OnCollision;
		}

		public virtual void Deactivate()
		{
			Model.RemoveObserver(View);
			Model.OnDeactivate();
			Model.UpdateSystem.RemoveListener(this);
			View.OnDeactivate();
			View.OnCollisionEvent -= OnCollision;
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

		private void OnCollision()
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