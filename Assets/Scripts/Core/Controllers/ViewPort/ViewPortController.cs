using Model.ViewPort;
using UnityEngine;

namespace Core.Controllers.ViewPort
{
	public class ViewPortController
	{
		private readonly ViewPortModel _model;

		public ViewPortController(ViewPortModel model)
		{
			_model = model;
		}

		public void SetRect(Rect rect)
		{
			_model.SetRect(rect);
		}
		
		public void SetOrthographicSize(float value)
		{
			_model.SetOrthographicSize(value);
		}
		
		public void SetScreenWidthHeight(int width, int height)
		{
			_model.SetScreenWidthHeight(width, height);
		}

		public void CheckPosition(ref Vector2 worldPos)
		{
			Vector2 viewportPoint = _model.WorldToViewportPoint(worldPos);
			
			var isOutOffScreen = false;
			
			if (viewportPoint.x <= 0.0f) 
			{
				viewportPoint = new Vector3(1.0f, viewportPoint.y);
				isOutOffScreen = true;
			}
			else if (viewportPoint.x >= 1.0f) 
			{
				viewportPoint = new Vector3(0f, viewportPoint.y);
				isOutOffScreen = true;
			}
			if (viewportPoint.y <= 0.0f) 
			{
				viewportPoint = new Vector3(viewportPoint.x, 1f);
				isOutOffScreen = true;
			}
			else if (viewportPoint.y >= 1.0f) 
			{
				viewportPoint = new Vector3(viewportPoint.x, 0f);
				isOutOffScreen = true;
			}

			if (isOutOffScreen)
				worldPos = _model.ViewportToWorldPoint(viewportPoint);
		}
		
		public Vector2 GetRandomPosition(Vector2 exceptPosition, float minDistance = 25)
		{
			var center = Vector2.zero;
			var randomPoint = _model.ViewportToWorldPoint(new Vector2(Random.value, Random.value));

			var dist = (center - (Vector2)randomPoint).sqrMagnitude;

			if (dist < minDistance)
				return GetRandomPosition(exceptPosition, minDistance);

			return randomPoint;
		}

		public Vector2 GetWorldToScreenPoint(Vector2 worldPos)
		{
			return _model.WorldToScreenPoint(worldPos);
		}
	}
}