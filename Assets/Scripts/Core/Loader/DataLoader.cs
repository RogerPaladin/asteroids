using System;
using Controllers.UI.Windows;
using Model.Input;
using Model.Windows;
using Utils.Events;
using Views.Catalogs;

namespace Core.Loader
{
	public class DataLoader
	{
		private readonly WindowsSystem _windowsSystem;
		private readonly InputModel _inputModel;
		private readonly UpdateSystem _updateSystem;
		private readonly WindowsViewCatalog _windowsViewCatalog;

		private PreloaderWindow _preloaderWindow;
		private int _progress = 0;

		public DataLoader(WindowsSystem windowsSystem, InputModel inputModel, UpdateSystem updateSystem, WindowsViewCatalog windowsViewCatalog)
		{
			_windowsSystem = windowsSystem;
			_inputModel = inputModel;
			_updateSystem = updateSystem;
			_windowsViewCatalog = windowsViewCatalog;
		}

		public void StartLoad(Action onCompleteLoad)
		{
			ShowPreloader();

			_preloaderWindow.OnCompleteLoad();
			_preloaderWindow.SetOnClose(() => onCompleteLoad?.Invoke());
		}

		private void ShowPreloader()
		{
			var model = new PreloaderWindowModel();
			var view = _windowsViewCatalog.Create(model);
			view.BindModel(model);
			_preloaderWindow = _windowsSystem.ShowWindow<PreloaderWindow>(model, view);
			_preloaderWindow.SetData(_inputModel, _updateSystem);
			SetProgress(0);
		}

		private void SetProgress(int value)
		{
			_progress = value;
			_preloaderWindow.SetProgress(_progress);
		}
	}
}