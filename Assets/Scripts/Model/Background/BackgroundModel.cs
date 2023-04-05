using Utils.Events;
using UnityEngine;
using Utils;
using Utils.Reactivity;

namespace Model.Background
{
	public class BackgroundModel : IModel, IActivateDeactivate
	{
		public Camera Camera { get; private set; }
		public UpdateSystem UpdateSystem { get; private set; }

		private Vector2 _lastScreenSize = Vector2.zero;
		public Observable<Vector2> BackgroundSize { get; private set; } = new Observable<Vector2>(Vector2.zero);
		public Observable<Transform> Parent { get; private set; } = new Observable<Transform>(null);

		public BackgroundModel(Camera camera, UpdateSystem updateSystem)
		{
			Camera = camera;
			UpdateSystem = updateSystem;
		}

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

		public void SetParent(Transform transform)
		{
			Parent.Value = transform;
		}

		public void Activate()
		{

		}

		public void Deactivate()
		{
			
		}
	}
}