using Core.Controllers.ViewPort;
using UnityEngine;

namespace Views.ViewPort
{
	public class ViewPortView : MonoBehaviour
	{
		private Camera _camera;
		private ViewPortController _controller;
		
		public void SetData(ViewPortController controller, Camera camera)
		{
			_camera = camera;
			_controller = controller;
		}
		
		private void Update()
		{
			var downLeft = _camera.ViewportToWorldPoint(Vector3.zero);
			var upRight = _camera.ViewportToWorldPoint(Vector3.one);
			
			_controller.SetRect(new Rect(downLeft, upRight - downLeft));
			_controller.SetOrthographicSize(_camera.orthographicSize);
			
			_controller.SetScreenWidthHeight(Screen.width, Screen.height);
		}
	}
}