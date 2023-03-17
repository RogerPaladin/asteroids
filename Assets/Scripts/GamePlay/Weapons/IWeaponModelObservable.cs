namespace Assets.Scripts.UI.Hud.WeaponInfo
{
	public interface IWeaponModelObservable
	{
		void RegisterObserver(IWeaponObserver o);
		void RemoveObserver(IWeaponObserver o);
		void NotifyUpdateAmmo();
		void NotifyUpdateRefreshTime();
	}
}