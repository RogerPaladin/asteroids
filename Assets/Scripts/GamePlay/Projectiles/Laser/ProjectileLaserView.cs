using UnityEngine;

namespace Assets.Scripts.GamePlay.Projectiles.Laser
{
	public class ProjectileLaserView: ProjectileView, IProjectileLaserViewObserver
	{
		[SerializeField] private SpriteRenderer _sprite;

		private BoxCollider2D _boxCollider => Collider as BoxCollider2D;

		private Vector2 _defaultSize;

		private void Awake()
		{
			_defaultSize = _sprite.size;
		}

		public override void OnActivate()
		{
			base.OnActivate();
			
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