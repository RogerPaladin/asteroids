namespace Assets.Scripts.GamePlay.Projectiles.Laser
{
	public interface IProjectileLaserModelObservable
	{
		void RegisterLaserObserver(IProjectileLaserViewObserver o);
		void RemoveLaserObserver(IProjectileLaserViewObserver o);
		void NotifyGrowSizeChanged();
	}
}