using System;
using Utils.Reactivity;

namespace Model.Windows
{
	public class RestartWindowModel : AbstractWindowModel
	{
		public Observable<int> Score { get; private set; } = new Observable<int>(0);
		public Observable<Action> OnRestartBtnCallback { get; private set; } = new Observable<Action>(null);
		
		public void SetScore(int score)
		{
			Score.Value = score;
		}

		public void SetOnRestartBtnCallback(Action action)
		{
			OnRestartBtnCallback.Value = action;
		}
	}
}