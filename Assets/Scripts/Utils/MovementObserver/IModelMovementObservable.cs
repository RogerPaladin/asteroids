namespace Utils.MovementObserver
{
	public interface IModelMovementObservable
	{
		void RegisterObserver(IMovementObserver o);
		void RemoveObserver(IMovementObserver o);
		void NotifyUpdatePosition();
		void NotifyUpdateRotation();
		void NotifyUpdateVelocity();
	}
}