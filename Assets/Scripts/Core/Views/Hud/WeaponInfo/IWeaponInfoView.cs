namespace Views.Hud.WeaponInfo
{
	public interface IWeaponInfoView
	{
		void OnNeedShowHideTimerPanelChange(bool val);
		void SetAmmoCount(int ammoCount);
		void SetRefreshTimer(float timeLeft);
		void ShowTimer();
		void HideTimer();
	}
}