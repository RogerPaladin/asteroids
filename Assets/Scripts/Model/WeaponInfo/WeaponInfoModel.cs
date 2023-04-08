using Model.Weapons;
using Utils;
using Utils.Reactivity;

namespace Model.WeaponInfo
{
	public class WeaponInfoModel : IModel, IActivateDeactivate
	{
		private WeaponModel _model;

		public Observable<int> AmmoCount => _model.AmmoCount;
		public Observable<float> CurrentRefreshTimeLeft => _model.CurrentRefreshTimeLeft;
		public Observable<bool> NeedShowHideTimerPanel { get; } = new Observable<bool>(false);

		public WeaponInfoModel(WeaponModel model)
		{
			_model = model;
		}

		private void OnCurrentRefreshTimeLeftChanged(float timeLeft)
		{
			if (timeLeft == 0)
				NeedShowHideTimerPanel.Value = false;
			else
				NeedShowHideTimerPanel.Value = true;
		}

		public void Activate()
		{
			_model.CurrentRefreshTimeLeft.Changed += OnCurrentRefreshTimeLeftChanged;
		}

		public void Deactivate()
		{
			RemoveListeners();
		}
		
		private void RemoveListeners()
		{
			if (_model == null)
				return;
			
			_model.CurrentRefreshTimeLeft.Changed -= OnCurrentRefreshTimeLeftChanged;
		}
	}
}