using Controllers.UI.WindowsSystem;
using Model.Windows;
using Utils.Events;
using Utils.Input;

namespace Controllers.UI.Windows
{
	public class PreloaderWindow : AbstractWindow, IUpdateListener
	{
		private PreloaderWindowModel Model => base.Model as PreloaderWindowModel;
		
		private InputController _inputController;
		private UpdateSystem _updateSystem;

		public PreloaderWindow(WindowsController windowsController, PreloaderWindowModel model) : base(windowsController, model)
		{
		}
		
		public void Init(InputController inputController, UpdateSystem updateSystem)
		{
			_inputController = inputController;
			_updateSystem = updateSystem;
		}
		
		public void Update(float deltaTime)
		{
			if (_inputController.IsAnyKeyPressed)
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