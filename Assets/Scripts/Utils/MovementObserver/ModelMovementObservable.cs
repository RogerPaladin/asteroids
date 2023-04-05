using UnityEngine;
using Utils.Reactivity;

namespace Utils.MovementObserver
{
	public abstract class ModelMovementObservable
	{
		public Observable<Vector2> Position { get; private set; } = new Observable<Vector2>(Vector2.zero);
		public Observable<Quaternion> Rotation { get; private set; } = new Observable<Quaternion>(Quaternion.identity);
		public Observable<Vector2> Velocity { get; private set; } = new Observable<Vector2>(Vector2.zero);

		public void SetPosition(Vector2 pos)
		{
			Position.Value = pos;
		}
		
		public void SetRotation(Quaternion rotation)
		{
			Rotation.Value = rotation;
		}

		public void SetVelocity(Vector2 velocity)
		{
			Velocity.Value = velocity;
		}
	}
}