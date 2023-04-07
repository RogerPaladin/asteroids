using Model.Input;
using Model.Windows;
using Utils.Events;

namespace Controllers.UI.Windows
{
	public class PreloaderWindow : AbstractWindow, IUpdateListener
	{
		private PreloaderWindowModel Model => base.Model as PreloaderWindowModel;
		
		private InputModel _inputModel;
		private UpdateSystem _updateSystem;

		public PreloaderWindow(WindowsSystem windowsSystem, PreloaderWindowModel model) : base(windowsSystem, model)
		{
		}
		
		public void SetData(InputModel inputModel, UpdateSystem updateSystem)
		{
			_inputModel = inputModel;
			_updateSystem = updateSystem;
		}
		
		public void Update(float deltaTime)
		{
			if (_inputModel.IsAnyKeyPressed)
				Close();
		}

		public void SetProgress(int progress)
		{
			Model.SetProgress(progress);
		}

		public void OnCompleteLoad()
		{
			Model.OnCompleteLoad();

			_updateSystem.AddListener(this);
		}

		protected override void OnClose()
		{
			base.OnClose();
			
			_updateSystem.RemoveListener(this);
		}
	}
}