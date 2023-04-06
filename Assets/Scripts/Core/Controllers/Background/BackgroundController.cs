using Model.Background;
using Model.ViewPort;
using UnityEngine;
using Utils;
using Utils.Events;

namespace Controllers.Background
{
	public class BackgroundController : IUpdateListener, IActivateDeactivate
	{
		public readonly BackgroundModel Model;
		
		private readonly UpdateSystem _updateSystem;
		private readonly ViewPortModel _viewPortModel;

		public BackgroundController(BackgroundModel model, UpdateSystem updateSystem, ViewPortModel viewPortModel)
		{
			Model = model;
			_updateSystem = updateSystem;
			_viewPortModel = viewPortModel;
		}

		public void Update(float deltaTime)
		{
			if (!Model.WasScreenChange(new Vector2(Screen.width, Screen.height)))
				return;
			
			float cameraHeight = _viewPortModel.OrthographicSize * 2;
			float cameraWidth = cameraHeight * Screen.width / Screen.height;

			Model.SetBackgroundSize(new Vector2(cameraWidth, cameraHeight));
		}

		public void Activate()
		{
			Model.Activate();
			_updateSystem.AddListener(this);
		}

		public void Deactivate()
		{
			Model.Deactivate();
			_updateSystem.RemoveListener(this);
		}
	}
}