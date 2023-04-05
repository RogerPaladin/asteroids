using Model.WeaponInfo;
using Utils;
using Utils.DiContainers;

namespace Controllers.UI.Hud.Score
{
	public class WeaponInfoController : IDiContainerChild, IActivateDeactivate
	{
		private WeaponInfoModel _model;

		public WeaponInfoController()
		{
		}

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