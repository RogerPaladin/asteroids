using System;
using Controllers.UI.Windows;
using Model.Input;
using Model.Windows;
using Static;
using Utils.Events;
using Views;

namespace Core.Loader
{
	public class DataLoader
	{
		private readonly WindowsSystem _windowsSystem;
		private readonly InputModel _inputModel;
		private readonly UpdateSystem _updateSystem;
		private readonly ViewInstantiator _viewInstantiator;
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
		
		public DataLoader(WindowsSystem windowsSystem, InputModel inputModel, UpdateSystem updateSystem, ViewInstantiator viewInstantiator, StaticData staticData)
		{
			_windowsSystem = windowsSystem;
			_inputModel = inputModel;
			_updateSystem = updateSystem;
			_viewInstantiator = viewInstantiator;
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
			var view = _viewInstantiator.Instantiate(model);
			view.BindModel(model);
			_preloaderWindow = _windowsSystem.ShowWindow<PreloaderWindow>(model, view);
			_preloaderWindow.SetData(_inputModel, _updateSystem);
			Progress = 0;
		}

		private void SetProgress(float value, int start, int end) => Progress = (int) (start + (end - start) * value);
	}
}