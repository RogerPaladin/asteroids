using System;
using Assets.Scripts.Events;
using Assets.Scripts.GamePlay.Input;
using Assets.Scripts.Static;
using Assets.Scripts.UI.Windows;
using Assets.Scripts.UI.WindowsSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
	public class DataLoader
	{
		private readonly WindowsController _windowsController;
		private readonly InputController _inputController;
		private readonly UpdateSystem _updateSystem;
		private readonly StaticData _staticData;
		
		private PreloaderWindow _preloaderWindow;
		private int _progress = 0;


		private int Progress
		{
			get => _progress;
			set
			{
				_progress = value;
				_preloaderWindow.SetProgress(_progress);
			}
		}
		
		public DataLoader(WindowsController windowsController, InputController inputController, UpdateSystem updateSystem, StaticData staticData)
		{
			_windowsController = windowsController;
			_inputController = inputController;
			_updateSystem = updateSystem;
			_staticData = staticData;
		}

		public void StartLoad(Action onCompleteLoad)
		{
			ShowPreloader();
			
			_staticData.LoadDataFromResources(x => SetProgress(x, 20, 100));
			
			_preloaderWindow.OnCompleteLoad();
			_preloaderWindow.SetOnClose(() => onCompleteLoad?.Invoke());
		}

		private void ShowPreloader()
		{
			_preloaderWindow = _windowsController.ShowWindow<PreloaderWindow>(false, w => w.Init(_inputController, _updateSystem));
			Progress = 0;
		}

		private void SetProgress(float value, int start, int end) => Progress = (int) (start + (end - start) * value);
	}
}