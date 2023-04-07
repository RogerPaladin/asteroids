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