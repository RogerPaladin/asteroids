using Utils.Reactivity;

namespace Model.WeaponInfo
{
	public class PlayerInfoModel : IModel
	{
		public Observable<string> PositionString { get; } = new Observable<string>(string.Empty);
		public Observable<string> RotationString { get; } = new Observable<string>(string.Empty);
		public Observable<string> VelocityString { get; } = new Observable<string>(string.Empty);

		public void SetPositionString(string val) { PositionString.Value = val; }
		public void SetRotationString(string val) { RotationString.Value = val; }
		public void SetVelocityString(string val) { VelocityString.Value = val; }
	}
}