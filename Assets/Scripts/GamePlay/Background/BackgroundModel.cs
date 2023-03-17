using System;
using Assets.Scripts.Events;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Background
{
	public class BackgroundModel
	{
		public Camera Camera { get; private set; }
		public UpdateSystem UpdateSystem { get; private set; }

		private Vector2 _lastScreenSize = Vector2.zero;
		private Vector2 _backgroundSize = Vector2.zero;
		
		public Action<Vector2> OnSizeChangeEvent { get; set; }

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
			_backgroundSize = backGroundSize;
			OnSizeChangeEvent?.Invoke(_backgroundSize);
		}
	}
}