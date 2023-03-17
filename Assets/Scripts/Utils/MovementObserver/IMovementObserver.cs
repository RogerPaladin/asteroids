using UnityEngine;

namespace Assets.Scripts.Utils.MovementObserver
{
	public interface IMovementObserver
	{
		public void OnPositionChange(Vector2 pos);
		public void OnRotationChange(Quaternion rotation);
		public void OnVelocityChange(Vector2 velocity);
	}
}