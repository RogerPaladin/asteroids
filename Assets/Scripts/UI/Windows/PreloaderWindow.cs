using Assets.Scripts.Events;
using Assets.Scripts.GamePlay.Input;
using Assets.Scripts.UI.Windows.Views;
using Assets.Scripts.UI.WindowsSystem;

namespace Assets.Scripts.UI.Windows
{
	public class PreloaderWindow : AbstractWindow, IUpdateListener
	{
		private InputController _inputController;
		private UpdateSystem _updateSystem;

		private PreloaderWindowView View => AbstractView as PreloaderWindowView;

		public PreloaderWindow(WindowsController windowsController, PreloaderWindowView windowAbstractView) : base(windowsController, windowAbstractView)
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
			View.SetProgress(progress);
		}

		public void OnCompleteLoad()
		{
			View.HideProgressBar();
			View.ShowPressAnyKeyText();
			
			_updateSystem.AddListener(this);
		}

		protected override void OnClose()
		{
			base.OnClose();
			
			_updateSystem.RemoveListener(this);
		}
	}
}