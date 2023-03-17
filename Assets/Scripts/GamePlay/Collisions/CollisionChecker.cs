using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Collisions
{
	public class CollisionChecker
	{
		private readonly Collider2D _collider;
		private readonly ContactFilter2D _filter;
		private readonly List<Collider2D> _results;

		public CollisionChecker(Collider2D collider)
		{
			_collider = collider;
			_filter = new ContactFilter2D().NoFilter();
			_results = new List<Collider2D>();
		}

		public bool IsHaveCollision
		{ 
			get
			{
				if (Physics2D.OverlapCollider(_collider, _filter, _results) == 0)
					return false;
				
				if (!_results[0].TryGetComponent(out ICollisionDetector collisionDetector))
					return false;

				collisionDetector.OnCollision();

				return true;
			}
		}
	}
}