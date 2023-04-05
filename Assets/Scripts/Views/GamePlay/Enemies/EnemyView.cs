using System;
using Model.Enemies;
using UnityEngine;
using Utils;

namespace Views.GamePlay.Enemies
{
	public class EnemyView: AbstractViewWithMovementObserver, IEnemyView, IActivateDeactivate
	{
		public Collider2D Collider;
		
		private EnemyModel Model => base.Model as EnemyModel;

		public event Action OnCollisionEvent;

		protected override void AddChildListeners()
		{
			Model.Position.Changed += OnPositionChange;
			Model.Rotation.Changed += OnRotationChange;
			Model.Velocity.Changed += OnVelocityChange;
			
			Model.Parent.Changed += OnChangeParent;
			Model.IsActive.Changed += OnActiveChange;
		}

		protected override void RemoveChildListeners()
		{
			Model.Position.Changed -= OnPositionChange;
			Model.Rotation.Changed -= OnRotationChange;
			Model.Velocity.Changed -= OnVelocityChange;
			
			Model.Parent.Changed -= OnChangeParent;
			Model.IsActive.Changed -= OnActiveChange;
		}

		public void OnCollision()
		{
			OnCollisionEvent?.Invoke();
		}
		
		public void OnChangeParent(Transform parent)
		{
			transform.SetParent(parent);
		}
		
		public void Activate()
		{
			gameObject.SetActive(true);
		}

		public void Deactivate()
		{
			gameObject.SetActive(false);
		}
		
		private void OnActiveChange(bool val)
		{
			if (val)
				Activate();
			else
				Deactivate();
		}
	}
}