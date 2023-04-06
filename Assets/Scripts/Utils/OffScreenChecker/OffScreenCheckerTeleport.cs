using UnityEngine;

namespace Utils.OffScreenChecker
{
	public class OffScreenCheckerTeleport
	{
		private readonly Camera _camera;

		public OffScreenCheckerTeleport(Camera camera)
		{
			_camera = camera;
		}

		public void CheckPosition(ref Vector2 pos)
		{
			Vector3 viewportPoint = _camera.WorldToViewportPoint(pos);
			
			var isOutOffScreen = false;
			
			if (viewportPoint.x < 0.0f) 
			{
				viewportPoint = new Vector3(1.0f - viewportPoint.x % 1f, viewportPoint.y, viewportPoint.z);
				isOutOffScreen = true;
			}
			else if (viewportPoint.x >= 1.0f) 
			{
				viewportPoint = new Vector3(viewportPoint.x % 1f, viewportPoint.y, viewportPoint.z);
				isOutOffScreen = true;
			}
			if (viewportPoint.y < 0.0f) 
			{
				viewportPoint = new Vector3(viewportPoint.x, 1f - viewportPoint.y % 1f, viewportPoint.z);
				isOutOffScreen = true;
			}
			else if (viewportPoint.y >= 1.0f) 
			{
				viewportPoint = new Vector3(viewportPoint.x, viewportPoint.y % 1f, viewportPoint.z);
				isOutOffScreen = true;
			}

			if (isOutOffScreen)
				pos = _camera.ViewportToWorldPoint(viewportPoint);
		}
	}
}