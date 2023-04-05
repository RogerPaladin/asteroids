using Model.WeaponInfo;
using Utils;
using Utils.DiContainers;

namespace Controllers.UI.Hud.PlayerInfo
{
	public class PlayerInfoController : IDiContainerChild, IActivateDeactivate
	{
		public PlayerInfoModel Model { get; private set; }

		public PlayerInfoController(PlayerInfoModel model = null)
		{
			Model = model;
		}

		public void SetModel(PlayerInfoModel model)
		{
			Model = model;
		}
		
		public void RemoveListeners()
		{
			// _view.RemoveListeners();
		}

		public void Activate()
		{
			Model?.Activate();
		}

		public void Deactivate()
		{
			Model?.Deactivate();
		}
	}
}