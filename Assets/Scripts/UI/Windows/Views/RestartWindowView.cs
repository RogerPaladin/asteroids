using System;
using Assets.Scripts.UI.WindowsSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Windows.Views
{
	public class RestartWindowView: AbstractWindowView
	{
		[SerializeField] private TextMeshProUGUI _scoreText;
		[SerializeField] private Button _restartBtn;


		public void SetScore(int score)
		{
			_scoreText.text = score.ToString();
		}

		public void SetRestartBtnCallBack(Action onRestartBtnClick)
		{
			_restartBtn.onClick.AddListener(() => onRestartBtnClick.Invoke());
		}
	}
}