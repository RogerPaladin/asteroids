using UnityEngine;

namespace Model.ViewPort
{
	public class ViewPortModel
	{
		public Rect Rect { get; private set; }
		public float OrthographicSize { get; private set; }
		public int ScreenWidth { get; private set; }
		public int ScreenHeight { get; private set; }

		public void SetRect(Rect rect)
		{
			Rect = rect;
		}

		public void SetOrthographicSize(float value)
		{
			OrthographicSize = value;
		}
		
		public void SetScreenWidthHeight(int width, int height)
		{
			ScreenWidth = width;
			ScreenHeight = height;
		}

		public Vector2 WorldToViewportPoint(Vector2 pos)
		{
			return Rect.PointToNormalized(Rect, pos);
		}

		public Vector2 ViewportToWorldPoint(Vector2 viewportPoint)
		{
			return Rect.NormalizedToPoint(Rect, viewportPoint);
		}

		public Vector2 WorldToScreenPoint(Vector2 worldPos)
		{
			var viewPortPoint = WorldToViewportPoint(worldPos);
			return new Vector2(viewPortPoint.x * ScreenWidth, viewPortPoint.y * ScreenHeight);
		}
	}
}