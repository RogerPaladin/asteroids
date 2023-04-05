using Model.WeaponInfo;
using TMPro;
using UnityEngine;
using Utils;

namespace Views.Hud.WeaponInfo
{
	public class WeaponInfoView: AbstractView, IWeaponInfoView
	{
		[SerializeField] private TextMeshProUGUI _ammoCount;
		[SerializeField] private TextMeshProUGUI _refreshTimer;

		protected WeaponInfoModel Model => base.Model as WeaponInfoModel;

		protected override void SyncModel()
		{
			SetAmmoCount(Model.AmmoCount.Value);
			SetRefreshTimer(Model.CurrentRefreshTimeLeft.Value);
			OnNeedShowHideTimerPanelChange(Model.NeedShowHideTimerPanel.Value);
		}
		
		protected override void AddChildListeners()
		{
			Model.AmmoCount.Changed += SetAmmoCount;
			Model.CurrentRefreshTimeLeft.Changed += SetRefreshTimer;
			Model.NeedShowHideTimerPanel.Changed += OnNeedShowHideTimerPanelChange;
		}

		protected override void RemoveChildListeners()
		{
			Model.AmmoCount.Changed -= SetAmmoCount;
			Model.CurrentRefreshTimeLeft.Changed -= SetRefreshTimer;
			Model.NeedShowHideTimerPanel.Changed -= OnNeedShowHideTimerPanelChange;
		}

		public void OnNeedShowHideTimerPanelChange(bool val)
		{
			if (val)
				ShowTimer();
			else
				HideTimer();
		}
		
		public void SetAmmoCount(int ammoCount)
		{
			_ammoCount.text = ammoCount.ToString();
		}

		public void SetRefreshTimer(float timeLeft)
		{
			var time = timeLeft.GetNumericTime(false);
			
			_refreshTimer.text = time;
		}

		public void ShowTimer()
		{
			_refreshTimer.gameObject.SetActive(true);
		}
		
		public void HideTimer()
		{
			_refreshTimer.gameObject.SetActive(false);
		}
	}
}