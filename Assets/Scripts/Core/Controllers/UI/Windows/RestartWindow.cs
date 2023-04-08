using System;
using Model.Windows;

namespace Controllers.UI.Windows
{
	public class RestartWindow : AbstractWindow
	{
		private RestartWindowModel Model => base.Model as RestartWindowModel;
		
		public RestartWindow(WindowsSystem windowsSystem, RestartWindowModel model) : base(windowsSystem, model)
		{
		}

		public void SetData(Action action)
		{
			Model.SetOnRestartBtnCallback(action);
		}

		protected override void OnClose()
		{
			Model.SetOnRestartBtnCallback(null);
		}
	}
}