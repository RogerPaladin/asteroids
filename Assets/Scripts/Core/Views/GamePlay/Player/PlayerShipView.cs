using Controllers.Player;
using Model.Player;
using UnityEngine;
using Utils;
using Utils.Collisions;

namespace Views.GamePlay.Player
{
	public class PlayerShipView: AbstractViewWithMovementObserver, IPlayerShipView, IActivateDeactivate
	{
		[SerializeField] private Collider2D _collider;
		[SerializeField] private Transform _projectileSpawnPoint;
		
		private PlayerShipController _controller;
		private CollisionChecker _collisionChecker;

		public Collider2D Collider => _collider;
		public Transform ProjectileSpawnPoint => _projectileSpawnPoint;

		private PlayerShipModel Model => base.Model as PlayerShipModel;

		protected override void AfterAwake()
		{
			base.AfterAwake();
			
			_collisionChecker = new CollisionChecker(_collider);
		}

		protected override void SyncModel()
		{
			OnPositionChange(Model.Position.Value);
			OnRotationChange(Model.Rotation.Value);
			OnVelocityChange(Model.Velocity.Value);
			
			OnActiveChange(Model.IsActive.Value);
		}
		
		protected override void AddChildListeners()
		{
			Model.Position.Changed += OnPositionChange;
			Model.Rotation.Changed += OnRotationChange;
			Model.Velocity.Changed += OnVelocityChange;
			
			Model.IsActive.Changed += OnActiveChange;
		}

		protected override void RemoveChildListeners() 
		{ 			
			Model.Position.Changed -= OnPositionChange;
			Model.Rotation.Changed -= OnRotationChange;
			Model.Velocity.Changed -= OnVelocityChange;
			
			Model.IsActive.Changed -= OnActiveChange;
		}

		public void SetData(PlayerShipModel model, PlayerShipController controller) //todo!!!! Уже есть BindModel(IModel)
		{
			BindModel(model);
			_controller = controller;
		}

		public void SetParent(Transform parent)
		{
			transform.SetParent(parent);
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
		
		private void Update()
		{
			CheckCollision();
		}

		private void CheckCollision()
		{
			if (_controller == null)
				return;

			ICollisionDetector collisionDetector = null;
			
			if (_collisionChecker.Check(ref collisionDetector))
			{
				_controller.OnCollision(true, collisionDetector);
				return;
			}
			
			_controller.OnCollision(false);
		}
	}
}