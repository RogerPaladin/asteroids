using System;
using Model.Player;
using UnityEngine;
using Utils;
using Utils.Reactivity;

namespace Model.WeaponInfo
{
	public class PlayerInfoModel : IModel, IActivateDeactivate
	{
		private PlayerShipModel _model;
		private readonly Camera _camera;

		public Observable<string> PositionString { get; private set; } = new Observable<string>(string.Empty);
		public Observable<string> RotationString { get; private set; } = new Observable<string>(string.Empty);
		public Observable<string> VelocityString { get; private set; } = new Observable<string>(string.Empty);

		public PlayerInfoModel(PlayerShipModel model, Camera camera)
		{
			_model = model;
			_camera = camera;
			_model.Position.Changed += OnPositionChange;
			_model.Rotation.Changed += OnRotationChange;
			_model.Velocity.Changed += OnVelocityChange;
		}
		
		private void OnPositionChange(Vector2 position)
		{
			var coords = _camera.WorldToScreenPoint(position);
			PositionString.Value = $"{Math.Round(coords.x)}; {Math.Round(coords.y)}";
		}
		
		private void OnRotationChange(Quaternion rotation)
		{
			float angle = rotation.eulerAngles.z;
			angle = (angle - 360) * -1;
			var rot = (int)Math.Round(angle);
			
			if (rot == 360)
				rot = 0;
			
			RotationString.Value = rot.ToString();
		}

		private void OnVelocityChange(Vector2 velocity)
		{
			var speed = velocity.magnitude;
			VelocityString.Value = speed.ToString("F2");
		}

		public void RemoveListeners()
		{
			if (_model == null)
				return;
			
			_model.Position.Changed -= OnPositionChange;
			_model.Rotation.Changed -= OnRotationChange;
			_model.Velocity.Changed -= OnVelocityChange;

			_model = null;
		}

		public void Activate()
		{
			
		}

		public void Deactivate()
		{
			RemoveListeners();
		}
	}
}