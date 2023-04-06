using UnityEngine;
using Utils;
using Utils.Reactivity;

namespace Model.Background
{
	public class BackgroundModel : IModel, IActivateDeactivate
	{
		private Vector2 _lastScreenSize = Vector2.zero;
		public Observable<Vector2> BackgroundSize { get; private set; } = new Observable<Vector2>(Vector2.zero);

		public bool WasScreenChange(Vector2 newScreenSize)
		{
			if (_lastScreenSize == newScreenSize)
				return false;

			_lastScreenSize = newScreenSize;
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