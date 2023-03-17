namespace Assets.Scripts.UI.Hud.WeaponInfo
{
	public interface IWeaponObserver
	{
		public void OnAmmoCountChange(int ammoCount);
		public void OnAmmoRefreshTimeChange(float timeLeft, float timeToRefresh);
	}
}