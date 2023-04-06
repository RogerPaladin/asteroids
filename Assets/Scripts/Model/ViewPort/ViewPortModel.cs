using UnityEngine;

namespace Model.ViewPort
{
	public class ViewPortModel
	{
		public Rect Rect { get; private set; }
		public float OrthographicSize { get; private set; }

		public void SetRect(Rect rect)
		{
			Rect = rect;
		}

		public void SetOrthographicSize(float value)
		{
			OrthographicSize = value;
		}

		public Vector3 WorldToViewportPoint(Vector2 pos)
		{
			return Rect.PointToNormalized(Rect, pos);
		}

		public Vector2 ViewportToWorldPoint(Vector2 viewportPoint)
		{
			return Rect.NormalizedToPoint(Rect, viewportPoint);
		}
	}
}