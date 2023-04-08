using Model.WeaponInfo;
using TMPro;
using UnityEngine;

namespace Views.Hud.PlayerInfo
{
	public class PlayerInfoView: AbstractView<PlayerInfoModel>
	{
		[SerializeField] private TextMeshProUGUI _coords;
		[SerializeField] private TextMeshProUGUI _angle;
		[SerializeField] private TextMeshProUGUI _speed;

		protected override void SyncModel()
		{
			SetCoords(Model.PositionString.Value);
			SetAngle(Model.RotationString.Value);
			SetSpeed(Model.VelocityString.Value);
		}
		
		protected override void AddChildListeners()
		{
			Model.PositionString.Changed += SetCoords;
			Model.RotationString.Changed += SetAngle;
			Model.VelocityString.Changed += SetSpeed;
		}

		protected override void RemoveChildListeners()
		{
			Model.PositionString.Changed -= SetCoords;
			Model.RotationString.Changed -= SetAngle;
			Model.VelocityString.Changed -= SetSpeed;
		}
		
		public void SetCoords(string val)
		{
			_coords.text = val;
		}
		
		public void SetAngle(string val)
		{
			_angle.text = val;
		}
		
		public void SetSpeed(string val)
		{
			_speed.text = val;
		}
	}
}