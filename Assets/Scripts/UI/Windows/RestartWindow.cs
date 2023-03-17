using System;
using Assets.Scripts.UI.Windows.Views;
using Assets.Scripts.UI.WindowsSystem;

namespace Assets.Scripts.UI.Windows
{
	public class RestartWindow : AbstractWindow
	{
		private RestartWindowView View => AbstractView as RestartWindowView;

		private int _score;
		private Action _onBtnClickCallback;

		public RestartWindow(WindowsController windowsController, RestartWindowView windowAbstractView) : base(windowsController, windowAbstractView)
		{
		}

		public void Init(int score, Action onRestartBtnClick)
		{
			_score = score;
			_onBtnClickCallback = onRestartBtnClick;

			View.SetScore(score);
			View.SetRestartBtnCallBack(OnRestartBtnClick);
		}

		private void OnRestartBtnClick()
		{
			Close();
			_onBtnClickCallback?.Invoke();
		}
	}
}