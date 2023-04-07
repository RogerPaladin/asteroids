using Model;
using UnityEngine;
using Utils.MovementObserver;

namespace Views
{
	public abstract class AbstractViewWithMovementObserver<T> : AbstractView<T>, IMovementObserver where T : IModel
	{
		public void OnRotationChange(Quaternion rotation) { transform.rotation = rotation; }
		public void OnVelocityChange(Vector2 velocity) { }
		public void OnPositionChange(Vector2 pos) { transform.position = pos; }
	}
}