using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utils.MovementObserver
{
	public abstract class ModelMovementObservable: IModelMovementObservable
	{
		public Vector2 Position { get; private set; }
		public Quaternion Rotation { get; private set; } = Quaternion.identity;
		public Vector2 Velocity { get; private set; }

		private List<IMovementObserver> _observers = new List<IMovementObserver>();

		public void RegisterObserver(IMovementObserver o)
		{
			_observers.Add(o);
			
			o.OnPositionChange(Position);
			o.OnRotationChange(Rotation);
			o.OnVelocityChange(Velocity);
		}
		
		public void RemoveObserver(IMovementObserver o) { _observers.Remove(o); }

		public void SetPosition(Vector2 pos)
		{
			Position = pos;
			NotifyUpdatePosition();
		}
		
		public void SetRotation(Quaternion rotation)
		{
			Rotation = rotation;
			NotifyUpdateRotation();
		}

		public void SetVelocity(Vector2 velocity)
		{
			Velocity = velocity;
			NotifyUpdateVelocity();
		}
		
		public void NotifyUpdatePosition() 
		{         
			foreach(IMovementObserver o in _observers)
				o.OnPositionChange(Position);
		}

		public void NotifyUpdateRotation()
		{
			foreach(IMovementObserver o in _observers)
				o.OnRotationChange(Rotation);
		}

		public void NotifyUpdateVelocity()
		{
			foreach(IMovementObserver o in _observers)
				o.OnVelocityChange(Velocity);
		}
	}
}