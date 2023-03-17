using Assets.Scripts.Utils.MovementObserver;
using UnityEngine;

namespace Assets.Scripts.UI.Hud.PlayerInfo
{
	public class PlayerInfoController : IMovementObserver
	{
		private Camera _camera;
		private PlayerInfoView _view;
		private ModelMovementObservable _watchModel;

		public PlayerInfoController(Camera camera, PlayerInfoView view)
		{
			_camera = camera;
			_view = view;
		}

		public void OnPositionChange(Vector2 position)
		{
			var coords = _camera.WorldToScreenPoint(position);
			_view.SetCoords(coords);
		}
		
		public void OnRotationChange(Quaternion rotation)
		{
			float angle = rotation.eulerAngles.z;
			angle = (angle - 360) * -1;
			_view.SetAngle(angle);
		}
		
		public void OnVelocityChange(Vector2 velocity)
		{
			var speed = velocity.magnitude;
			_view.SetSpeed(speed);
		}
	}
}