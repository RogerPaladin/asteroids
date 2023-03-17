using Assets.Scripts.UI.Hud.WeaponInfo;
using Assets.Scripts.Utils;

namespace Assets.Scripts.UI.Hud.Score
{
	public class WeaponInfoController : IWeaponObserver
	{
		private WeaponInfoView _view;
		
		public WeaponInfoController(WeaponInfoView view)
		{
			_view = view;
		}
		
		public void OnAmmoCountChange(int ammoCount)
		{
			_view.SetAmmoCount(ammoCount);
		}

		public void OnAmmoRefreshTimeChange(float timeLeft, float timeToRefresh)
		{
			if (timeLeft == 0)
				_view.HideTimer();
			else
				_view.ShowTimer();
			
			var time = timeLeft.GetNumericTime(false);
			_view.SetRefreshTimer(time);
		}
	}
}