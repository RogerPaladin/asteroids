using UnityEngine;
using Utils;
using Utils.Reactivity;

namespace Model.Background
{
	public class BackgroundModel : IModel, IActivateDeactivate
	{
		private Vector2 _lastScreenSize = Vector2.zero;
		private float _lastOrthographicSize = 0f;
		
		public Observable<Vector2> BackgroundSize { get; private set; } = new Observable<Vector2>(Vector2.zero);

		public bool WasScreenChange(Vector2 newScreenSize, float newOrthographicSize)
		{
			if (_lastScreenSize == newScreenSize && Mathf.Approximately(_lastOrthographicSize, newOrthographicSize))
				return false;

			_lastScreenSize = newScreenSize;
			_lastOrthographicSize = newOrthographicSize;
			
			return true;
		}

		public void SetBackgroundSize(Vector2 backGroundSize)
		{
			BackgroundSize.Value = backGroundSize;
		}

		public void Activate()
		{

		}

		public void Deactivate()
		{
			
		}
	}
}