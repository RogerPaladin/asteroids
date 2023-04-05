using Model.Background;
using UnityEngine;
using Utils;
using Utils.Events;

namespace Controllers.Background
{
	public class BackgroundController : IUpdateListener, IActivateDeactivate
	{
		public readonly BackgroundModel Model;

		public BackgroundController(BackgroundModel model)
		{
			Model = model;
		}

		public void SetParent(Transform t)
		{
			Model.SetParent(t);
		}

		public void Update(float deltaTime)
		{
			if (!Model.WasScreenChange(new Vector2(Screen.width, Screen.height)))
				return;
			
			float cameraHeight = Model.Camera.orthographicSize * 2;
			float cameraWidth = cameraHeight * Screen.width / Screen.height;

			Model.SetBackgroundSize(new Vector2(cameraWidth, cameraHeight));
		}

		public void Activate()
		{
			Model.Activate();
			Model.UpdateSystem.AddListener(this);
		}

		public void Deactivate()
		{
			Model.Deactivate();
			Model.UpdateSystem.RemoveListener(this);
		}
	}
}