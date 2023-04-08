using Model.Background;
using Model.ViewPort;
using UnityEngine;
using Utils;
using Utils.Events;

namespace Controllers.Background
{
	public class BackgroundController : IUpdateListener, IActivateDeactivate
	{
		private readonly BackgroundModel _model;
		
		private readonly UpdateSystem _updateSystem;
		private readonly ViewPortModel _viewPortModel;

		public BackgroundController(BackgroundModel model, UpdateSystem updateSystem, ViewPortModel viewPortModel)
		{
			_model = model;
			_updateSystem = updateSystem;
			_viewPortModel = viewPortModel;
		}

		public void Update(float deltaTime)
		{
			if (!_model.WasScreenChange(new Vector2(_viewPortModel.ScreenWidth, _viewPortModel.ScreenHeight), _viewPortModel.OrthographicSize))
				return;
			
			float cameraHeight = _viewPortModel.OrthographicSize * 2;
			float cameraWidth = cameraHeight * _viewPortModel.ScreenWidth / _viewPortModel.ScreenHeight;

			_model.SetBackgroundSize(new Vector2(cameraWidth, cameraHeight));
		}

		public void Activate()
		{
			_model.Activate();
			_updateSystem.AddListener(this);
		}

		public void Deactivate()
		{
			_model.Deactivate();
			_updateSystem.RemoveListener(this);
		}
	}
}