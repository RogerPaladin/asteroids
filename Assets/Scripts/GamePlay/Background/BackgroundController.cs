using Assets.Scripts.Events;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Background
{
	public class BackgroundController : IUpdateListener
	{
		private readonly BackgroundModel _model;
		private readonly BackgroundView _view;

		public BackgroundController(BackgroundModel model, BackgroundView view)
		{
			_model = model;
			_view = view;
		}

		public void AddBackgroundToTransform(Transform t)
		{
			_view.transform.SetParent(t);

			AddListeners();
		}

		public void Update(float deltaTime)
		{
			if (!_model.WasScreenChange(new Vector2(Screen.width, Screen.height)))
				return;
			
			float cameraHeight = _model.Camera.orthographicSize * 2;
			float cameraWidth = cameraHeight * Screen.width / Screen.height;

			_model.SetBackgroundSize(new Vector2(cameraWidth, cameraHeight));
		}

		private void AddListeners()
		{
			_view.Watch(_model);
			
			_model.UpdateSystem.AddListener(this);
		}
		
		private void RemoveListeners()
		{
			_view.UnWatch(_model);
			
			_model.UpdateSystem.RemoveListener(this);
		}
	}
}