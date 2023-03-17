using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Hud.WeaponInfo
{
	public class WeaponInfoView: MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _ammoCount;
		[SerializeField] private TextMeshProUGUI _refreshTimer;

		public void SetAmmoCount(int ammoCount)
		{
			_ammoCount.text = ammoCount.ToString();
		}
		
		public void SetRefreshTimer(string time)
		{
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