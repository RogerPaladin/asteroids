using System;
using Model.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Windows.Restart
{
	public class RestartWindowView: AbstractWindowView
	{
		[SerializeField] private TextMeshProUGUI _scoreText;
		[SerializeField] private Button _restartBtn;

		private RestartWindowModel Model => base.Model as RestartWindowModel;
		
		protected override void SyncModel()
		{
			SetScore(Model.Score.Value);
			SetRestartBtnCallBack(Model.OnRestartBtnCallback.Value);
		}
		
		public void SetScore(int score)
		{
			_scoreText.text = score.ToString();
		}

		public void SetRestartBtnCallBack(Action onRestartBtnClick)
		{
			_restartBtn.onClick.RemoveAllListeners();
			_restartBtn.onClick.AddListener(() => onRestartBtnClick?.Invoke());
		}

		protected override void AddChildListeners()
		{
			Model.Score.Changed += SetScore;
			Model.OnRestartBtnCallback.Changed += SetRestartBtnCallBack;
		}

		protected override void RemoveChildListeners()
		{
			Model.Score.Changed -= SetScore;
			Model.OnRestartBtnCallback.Changed -= SetRestartBtnCallBack;
		}
	}
}