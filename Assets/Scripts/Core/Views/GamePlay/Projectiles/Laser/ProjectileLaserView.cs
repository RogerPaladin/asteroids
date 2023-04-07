using Model.Projectiles.Laser;
using UnityEngine;

namespace Views.GamePlay.Projectiles.Laser
{
	public class ProjectileLaserView: ProjectileView
	{
		[SerializeField] private SpriteRenderer _sprite;

		private BoxCollider2D _boxCollider => Collider as BoxCollider2D;

		private Vector2 _defaultSize;

		private ProjectileLaserModel Model => base.Model as ProjectileLaserModel;
		
		protected override void AfterAwake()
		{
			base.AfterAwake();
			
			_defaultSize = _sprite.size;
		}
		
		protected override void SyncModel()
		{
			base.SyncModel();

			OnGrowChange(Model.GrowSize.Value);
		}
		
		protected override void AddChildListeners()
		{
			base.AddChildListeners();
			
			Model.GrowSize.Changed += OnGrowChange;
		}

		protected override void RemoveChildListeners()
		{
			base.RemoveChildListeners();
			
			Model.GrowSize.Changed -= OnGrowChange;
		}

		public override void Activate()
		{
			base.Activate();
			
			SetDefaultSize();
		}

		public void OnGrowChange(Vector2 grow)
		{
			_sprite.size += grow;
			UpdateColliderSize();
		}

		private void SetDefaultSize()
		{
			_sprite.size = _defaultSize;
			UpdateColliderSize();
		}
		
		private void UpdateColliderSize()
		{
			_boxCollider.offset = new Vector2(0, _sprite.size.y  / 2);
			_boxCollider.size = _sprite.size;
		}
	}
}