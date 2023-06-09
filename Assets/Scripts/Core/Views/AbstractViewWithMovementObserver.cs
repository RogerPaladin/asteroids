using Model;
using UnityEngine;

namespace Views
{
	public abstract class AbstractViewWithMovementObserver<T> : AbstractView<T> where T : IModel
	{
		public void OnRotationChange(Quaternion rotation) { transform.rotation = rotation; }
		public void OnVelocityChange(Vector2 velocity) { }
		public void OnPositionChange(Vector2 pos) { transform.position = pos; }
	}
}