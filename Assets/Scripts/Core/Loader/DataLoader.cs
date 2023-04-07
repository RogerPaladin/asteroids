using System;
using Controllers.UI.Windows;
using Model.Input;
using Model.Windows;
using Static;
using Utils.Events;

namespace Core.Loader
{
	public class DataLoader
	{
		private readonly WindowsSystem _windowsSystem;
		private readonly InputModel _inputModel;
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
		
		public DataLoader(WindowsSystem windowsSystem, InputModel inputModel, UpdateSystem updateSystem, StaticData staticData)
		{
			_windowsSystem = windowsSystem;
			_inputModel = inputModel;
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
			var model = new PreloaderWindowModel();
			_preloaderWindow = _windowsSystem.ShowWindow<PreloaderWindow>(model);
			_preloaderWindow.SetData(_inputModel, _updateSystem);
			Progress = 0;
		}

		private void SetProgress(float value, int start, int end) => Progress = (int) (start + (end - start) * value);
	}
}