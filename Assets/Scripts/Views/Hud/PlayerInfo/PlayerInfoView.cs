using System;
using Model.WeaponInfo;
using TMPro;
using UnityEngine;
using Utils;

namespace Views.Hud.PlayerInfo
{
	public class PlayerInfoView: AbstractView, IPlayerInfoView, IActivateDeactivate
	{
		[SerializeField] private TextMeshProUGUI _coords;
		[SerializeField] private TextMeshProUGUI _angle;
		[SerializeField] private TextMeshProUGUI _speed;

		public PlayerInfoModel Model => base.Model as PlayerInfoModel;

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

		public void Activate()
		{
			gameObject.SetActive(true);
		}

		public void Deactivate()
		{
			gameObject.SetActive(false);
		}
	}
}