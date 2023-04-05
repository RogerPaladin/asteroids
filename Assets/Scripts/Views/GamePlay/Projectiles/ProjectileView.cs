using Model.Projectiles;
using UnityEngine;
using Utils;

namespace Views.GamePlay.Projectiles
{
	public abstract class ProjectileView: AbstractViewWithMovementObserver, IProjectileView, IActivateDeactivate
	{
		[SerializeField] private Collider2D _collider;
		
		public Collider2D Collider => _collider;

		public ProjectileModel Model => base.Model as ProjectileModel;

		protected override void SyncModel()
		{
			OnPositionChange(Model.Position.Value);
			OnRotationChange(Model.Rotation.Value);
			OnVelocityChange(Model.Velocity.Value);
			
			OnChangeParent(Model.Parent.Value);
			OnActiveChange(Model.IsActive.Value);
		}

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
		
		public void OnChangeParent(Transform parent)
		{
			transform.SetParent(parent);
		}
		
		public virtual void Activate()
		{
			if (gameObject.activeSelf)
				return;
			
			gameObject.SetActive(true);
			
			SyncModel();
		}

		public virtual void Deactivate()
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