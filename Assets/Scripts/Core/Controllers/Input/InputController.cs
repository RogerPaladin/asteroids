using Model.Input;

namespace Core.Controllers.Input
{
	public class InputController
	{
		public InputModel Model { get; }

		public InputController(InputModel model)
		{
			Model = model;
		}
		
		public void SetRotation(float val) { Model.SetRotation(val); }
		public void SetThrust(float val) { Model.SetThrust(val); }
		public void SetNeedShootFirstWeapon(bool val) { Model.SetNeedShootFirstWeapon(val); }
		public void SetNeedShootSecondWeapon(bool val) { Model.SetNeedShootSecondWeapon(val); }
		public void SetIsAnyKeyPressed(bool val) { Model.SetIsAnyKeyPressed(val); }
	}
}