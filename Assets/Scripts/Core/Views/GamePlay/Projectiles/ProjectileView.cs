using Controllers.Projectiles;
using Model.Projectiles;
using UnityEngine;
using Utils;
using Utils.Collisions;

namespace Views.GamePlay.Projectiles
{
	public abstract class ProjectileView: AbstractViewWithMovementObserver<ProjectileModel>, IActivateDeactivate
	{
		[SerializeField] private Collider2D _collider;
		
		private CollisionChecker _collisionChecker;
		private AbstractProjectileController _controller;
		
		public Collider2D Collider => _collider;

		protected override void AfterAwake()
		{
			base.AfterAwake();
			
			_collisionChecker = new CollisionChecker(_collider);
		}
		
		public void SetData(ProjectileModel model, AbstractProjectileController controller) //todo!!!! Уже есть BindModel(IModel)
		{
			BindModel(model);
			_controller = controller;
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