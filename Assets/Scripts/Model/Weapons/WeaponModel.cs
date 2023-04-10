using Static.Catalogs;
using Utils;
using Utils.Events;
using Utils.Reactivity;

namespace Model.Weapons
{
	public class WeaponModel : IActivateDeactivate
	{
		public WeaponDataCatalog WeaponDataCatalog { get; }
		public TimerSystem TimerSystem { get; }

		public Observable<int> AmmoCount { get; } = new Observable<int>(0);
		public Observable<float> CurrentRefreshTimeLeft { get; } = new Observable<float>(0);
		
		public bool CanShoot => HaveUnlimitedAmmo || AmmoCount.Value > 0;

		public bool HaveUnlimitedAmmo => WeaponDataCatalog.AmmoStartCount < 0;
		public int AmmoMaxCount => WeaponDataCatalog.AmmoMaxCount;
		public float AmmoRefreshTime => WeaponDataCatalog.AmmoRefreshTime;

		public WeaponModel(WeaponDataCatalog weaponDataCatalog, TimerSystem timerSystem)
		{
			WeaponDataCatalog = weaponDataCatalog;
			TimerSystem = timerSystem;
		}

		public void Activate()
		{
			SetAmmo(WeaponDataCatalog.AmmoStartCount);
			SetRefreshTime(0);
		}
		
		public void Deactivate()
		{

		}
		
		public void SetAmmo(int ammo)
		{
			AmmoCount.Value = ammo;
		}
		
		public void SetRefreshTime(float timeLeft)
		{
			CurrentRefreshTimeLeft.Value = timeLeft;
		}
	}
}