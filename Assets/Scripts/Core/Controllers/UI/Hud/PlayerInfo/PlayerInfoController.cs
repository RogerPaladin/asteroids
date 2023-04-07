using System;
using Core.Controllers.ViewPort;
using Model.Player;
using Model.WeaponInfo;
using UnityEngine;
using Utils;

namespace Controllers.UI.Hud.PlayerInfo
{
	public class PlayerInfoController: IActivateDeactivate
	{
		private readonly ViewPortController _viewPortController;
		
		public PlayerInfoModel Model { get; private set; }
		private PlayerShipModel _playerShipModel;

		public PlayerInfoController(PlayerInfoModel model, ViewPortController viewPortController)
		{
			_viewPortController = viewPortController;
			Model = model;
		}

		public void SetPlayerShipModel(PlayerShipModel model)
		{
			RemoveListeners();
			
			_playerShipModel = model;

			AddListeners();
		}

		private void AddListeners()
		{
			if (_playerShipModel == null)
				return;
			
			_playerShipModel.Position.Changed += OnPositionChange;
			_playerShipModel.Rotation.Changed += OnRotationChange;
			_playerShipModel.Velocity.Changed += OnVelocityChange;
		}

		public void RemoveListeners()
		{
			if (_playerShipModel == null)
				return;
			
			_playerShipModel.Position.Changed -= OnPositionChange;
			_playerShipModel.Rotation.Changed -= OnRotationChange;
			_playerShipModel.Velocity.Changed -= OnVelocityChange;

			_playerShipModel = null;
		}

		public void Activate()
		{

		}

		public void Deactivate()
		{
			RemoveListeners();
		}
		
		private void OnPositionChange(Vector2 position)
		{
			var coords = _viewPortController.GetWorldToScreenPoint(position);
			Model.SetPositionString($"{Math.Round(coords.x)}; {Math.Round(coords.y)}");
		}
		
		private void OnRotationChange(Quaternion rotation)
		{
			float angle = rotation.eulerAngles.z;
			angle = (angle - 360) * -1;
			var rot = (int)Math.Round(angle);
			
			if (rot == 360)
				rot = 0;
			
			Model.SetRotationString(rot.ToString());
		}

		private void OnVelocityChange(Vector2 velocity)
		{
			var speed = velocity.magnitude;
			Model.SetVelocityString(speed.ToString("F2"));
		}
	}
}