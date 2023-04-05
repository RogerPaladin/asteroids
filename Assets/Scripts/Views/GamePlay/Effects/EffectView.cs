using Model.Effects;
using UnityEngine;
using Utils;

namespace Views.GamePlay.Effects
{
	public abstract class EffectView: AbstractViewWithMovementObserver, IEffectView, IActivateDeactivate
	{
		public EffectModel Model => base.Model as EffectModel;

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
			transform.SetParent(parent, false);
		}
		
		public void Activate()
		{
			if (gameObject.activeSelf)
				return;
				
			gameObject.SetActive(true);
			
			SyncModel();
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