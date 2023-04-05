using System;
using Controllers.UI.WindowsSystem;
using Model.Windows;

namespace Controllers.UI.Windows
{
	public class RestartWindow : AbstractWindow
	{
		private RestartWindowModel Model => base.Model as RestartWindowModel;
		
		public RestartWindow(WindowsController windowsController, RestartWindowModel model) : base(windowsController, model)
		{
		}

		public void Init(int score, Action onRestartBtnClick)
		{
			Model.SetScore(score);
			Model.SetOnRestartBtnCallback(() =>
			{
				Close();
				onRestartBtnClick?.Invoke();
			});
		}
	}
}