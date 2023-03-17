using Assets.Scripts.UI.WindowsSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Windows.Views
{
	public class PreloaderWindowView: AbstractWindowView
	{
		[SerializeField] private TextMeshProUGUI _progressText;
		[SerializeField] private TextMeshProUGUI _pressAnyKeyText;
		[SerializeField] private Slider _progressBar;

		public void SetProgress(int val)
		{

			if (_progressText)
				_progressText.text = val + "%";
			if (_progressBar)
				_progressBar.value = val / 100f;
		}

		public void HideProgressBar()
		{
			_progressBar.gameObject.SetActive(false);
		}

		public void ShowPressAnyKeyText()
		{
			_pressAnyKeyText.gameObject.SetActive(true);
		}
	}
}