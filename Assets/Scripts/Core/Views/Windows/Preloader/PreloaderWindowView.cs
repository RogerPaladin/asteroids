using Model.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Windows.Preloader
{
	public class PreloaderWindowView: AbstractWindowView
	{
		[SerializeField] private TextMeshProUGUI _progressText;
		[SerializeField] private TextMeshProUGUI _pressAnyKeyText;
		[SerializeField] private Slider _progressBar;

		private PreloaderWindowModel Model => base.Model as PreloaderWindowModel;
		
		protected override void SyncModel()
		{
			SetProgress(Model.Progress.Value);
			OnNeedShowProgressBarChange(Model.NeedShowProgressBar.Value);
			OnNeedShowAnyKeyTextChange(Model.NeedShowAnyKeyText.Value);
		}
		
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

		private void OnNeedShowProgressBarChange(bool val)
		{
			if (!val)
				HideProgressBar();
		}
		
		private void OnNeedShowAnyKeyTextChange(bool val)
		{
			if (val)
				ShowPressAnyKeyText();
		}
		
		protected override void AddChildListeners()
		{
			Model.Progress.Changed += SetProgress;
			Model.NeedShowProgressBar.Changed += OnNeedShowProgressBarChange;
			Model.NeedShowAnyKeyText.Changed += OnNeedShowAnyKeyTextChange;
		}

		protected override void RemoveChildListeners()
		{
			Model.Progress.Changed -= SetProgress;
			Model.NeedShowProgressBar.Changed -= OnNeedShowProgressBarChange;
			Model.NeedShowAnyKeyText.Changed += OnNeedShowAnyKeyTextChange;
		}
	}
}