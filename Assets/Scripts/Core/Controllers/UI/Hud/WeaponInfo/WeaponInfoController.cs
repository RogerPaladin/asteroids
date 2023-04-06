using Model.WeaponInfo;
using Utils;


namespace Controllers.UI.Hud.Score
{
	public class WeaponInfoController: IActivateDeactivate
	{
		private WeaponInfoModel _model;

		public void SetModel(WeaponInfoModel model)
		{
			_model = model;
		}

		public void Activate()
		{
			_model?.Activate();
		}

		public void Deactivate()
		{
			_model?.Deactivate();
		}
	}
}