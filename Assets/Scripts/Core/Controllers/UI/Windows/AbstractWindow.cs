using System;
using Model.Windows;

namespace Controllers.UI.Windows
{
    public abstract class AbstractWindow
	{
		protected readonly AbstractWindowModel Model;
		private readonly WindowsSystem _system;

		public AbstractWindow(WindowsSystem windowsSystem, AbstractWindowModel model)
		{
			_system = windowsSystem;
			Model = model;
		}

		public void Close()
        {
			Model.Close();

			_system.RemoveFromController(this);

			StartHide();
			OnClose();
		}
		
		public void Minimize()
		{
			Model.Minimize();
		}

		public void Maximize()
		{
			Model.Maximize();
		}

		private void OnHideEnd()
		{
			Model.HideEnd();
		}

		private void SetInteractiveState(bool interactive)
		{
			Model.SetInteractiveState(interactive);
		}

		protected abstract void OnClose();

		private void StartHide()
        {
            SetInteractiveState(false);

			Model.StartHide();

			OnHideEnd();
        }

		public void SetOnClose(Action onClose)
		{
			Model.SetOnCloseCallback(onClose);
		}
	}
}
