using System;

namespace Views.Windows.Restart
{
	internal interface IRestartWindowView
	{
		void SetScore(int score);
		void SetRestartBtnCallBack(Action onRestartBtnClick);
	}
}