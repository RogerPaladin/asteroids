using UnityEngine;
using Utils.MovementObserver;

namespace Views
{
	public abstract class AbstractViewWithMovementObserver : AbstractView, IMovementObserver
	{
		public void OnRotationChange(Quaternion rotation) { transform.rotation = rotation; }
		public void OnVelocityChange(Vector2 velocity) { }
		public void OnPositionChange(Vector2 pos) { transform.position = pos; }
	}
}