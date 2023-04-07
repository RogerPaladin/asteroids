
namespace Model.Input
{
	public class InputModel
	{
		public float Rotation { get; private set; }
		public float Thrust { get; private set; }
		public bool NeedShootFirstWeapon { get; private set; }
		public bool NeedShootSecondWeapon { get; private set; }
		public bool IsAnyKeyPressed { get; private set; }

		public void SetRotation(float val) { Rotation = val; }
		public void SetThrust(float val) { Thrust = val; }
		public void SetNeedShootFirstWeapon(bool val) { NeedShootFirstWeapon = val; }
		public void SetNeedShootSecondWeapon(bool val) { NeedShootSecondWeapon = val; }
		public void SetIsAnyKeyPressed(bool val) { IsAnyKeyPressed = val; }
	}
}