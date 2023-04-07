using Utils.Reactivity;

namespace Model.WeaponInfo
{
	public class PlayerInfoModel : IModel
	{
		public Observable<string> PositionString { get; private set; } = new Observable<string>(string.Empty);
		public Observable<string> RotationString { get; private set; } = new Observable<string>(string.Empty);
		public Observable<string> VelocityString { get; private set; } = new Observable<string>(string.Empty);

		public void SetPositionString(string val) { PositionString.Value = val; }
		public void SetRotationString(string val) { RotationString.Value = val; }
		public void SetVelocityString(string val) { VelocityString.Value = val; }
	}
}