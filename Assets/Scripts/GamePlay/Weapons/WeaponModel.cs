using System.Collections.Generic;
using Assets.Scripts.Events;
using Assets.Scripts.Static.Weapons;
using Assets.Scripts.UI.Hud.WeaponInfo;

namespace Assets.Scripts.GamePlay.Weapons
{
	public class WeaponModel : IWeaponModelObservable
	{
		public WeaponConfig Config { get; private set; }
		public TimerSystem TimerSystem { get; private set; }

		public int AmmoCount { get; private set; } = 0;
		public float CurrentRefreshTimeLeft { get; private set; } = 0;
		
		public bool CanShoot => HaveUnlimitedAmmo || AmmoCount > 0;

		public bool HaveUnlimitedAmmo => Config.AmmoStartCount < 0;
		public int AmmoMaxCount => Config.AmmoMaxCount;
		public float AmmoRefreshTime => Config.AmmoRefresh;

		private List<IWeaponObserver> _observers = new List<IWeaponObserver>();
		
		public WeaponModel(WeaponConfig config, TimerSystem timerSystem)
		{
			Config = config;
			TimerSystem = timerSystem;
		}

		public void OnActivate()
		{
			SetAmmo(Config.AmmoStartCount);
			SetRefreshTime(0);
		}
		
		public void OnDeactivate()
		{

		}
		
		public void SetAmmo(int ammo)
		{
			AmmoCount = ammo;
			NotifyUpdateAmmo();
		}
		
		public void SetRefreshTime(float timeLeft)
		{
			CurrentRefreshTimeLeft = timeLeft;
			NotifyUpdateRefreshTime();
		}

		public void RegisterObserver(IWeaponObserver o)
		{
			_observers.Add(o);
			
			o.OnAmmoCountChange(AmmoCount);
			o.OnAmmoRefreshTimeChange(CurrentRefreshTimeLeft, AmmoRefreshTime);
		}

		public void RemoveObserver(IWeaponObserver o) { _observers.Add(o); }

		public void NotifyUpdateAmmo()
		{
			foreach(IWeaponObserver o in _observers)
				o.OnAmmoCountChange(AmmoCount);
		}

		public void NotifyUpdateRefreshTime()
		{
			foreach(IWeaponObserver o in _observers)
				o.OnAmmoRefreshTimeChange(CurrentRefreshTimeLeft, AmmoRefreshTime);
		}
	}
}