using UnityEngine;

namespace Utils.MovementObserver
{
	public abstract class ViewMovementObserver: MonoBehaviour, IMovementObserver
	{
		public void OnRotationChange(Quaternion rotation) { transform.rotation = rotation; }
		public void OnVelocityChange(Vector2 velocity) { }
		public void OnPositionChange(Vector2 pos) { transform.position = pos; }
	}
}