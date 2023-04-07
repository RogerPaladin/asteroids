using Static.Weapons;
using Utils;
using Utils.Events;
using Utils.Reactivity;

namespace Model.Weapons
{
	public class WeaponModel : IActivateDeactivate
	{
		public WeaponConfig Config { get; }
		public TimerSystem TimerSystem { get; }

		public Observable<int> AmmoCount { get; } = new Observable<int>(0);
		public Observable<float> CurrentRefreshTimeLeft { get; } = new Observable<float>(0);
		
		public bool CanShoot => HaveUnlimitedAmmo || AmmoCount.Value > 0;

		public bool HaveUnlimitedAmmo => Config.AmmoStartCount < 0;
		public int AmmoMaxCount => Config.AmmoMaxCount;
		public float AmmoRefreshTime => Config.AmmoRefresh;

		public WeaponModel(WeaponConfig config, TimerSystem timerSystem)
		{
			Config = config;
			TimerSystem = timerSystem;
		}

		public void Activate()
		{
			SetAmmo(Config.AmmoStartCount);
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