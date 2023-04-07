using System;
using Utils.Reactivity;

namespace Model.Windows
{
	public class RestartWindowModel : AbstractWindowModel
	{
		public Observable<int> Score { get; } = new Observable<int>(0);
		public Observable<Action> OnRestartBtnCallback { get; } = new Observable<Action>(null);
		
		public RestartWindowModel(int scoreValue)
		{
			Score.Value = scoreValue;
		}
		
		public void SetOnRestartBtnCallback(Action action)
		{
			OnRestartBtnCallback.Value = action;
		}
	}
}