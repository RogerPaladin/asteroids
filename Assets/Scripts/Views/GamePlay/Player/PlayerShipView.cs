using Model.Player;
using UnityEngine;
using Utils;

namespace Views.GamePlay.Player
{
	public class PlayerShipView: AbstractViewWithMovementObserver, IPlayerShipView, IActivateDeactivate
	{
		[SerializeField] private Collider2D _collider;
		[SerializeField] private Transform _projectileSpawnPoint;

		public Collider2D Collider => _collider;
		public Transform ProjectileSpawnPoint => _projectileSpawnPoint;
		
		private PlayerShipModel Model => base.Model as PlayerShipModel;

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