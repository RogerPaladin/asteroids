using Model.Score;
using TMPro;
using UnityEngine;

namespace Views.Hud.Score
{
	public class ScoreView: AbstractView, IScoreView
	{
		[SerializeField] private TextMeshProUGUI _scoreCount;

		private ScoreModel Model => base.Model as ScoreModel;

		protected override void AddChildListeners()
		{
			Model.Score.Changed += SetScoreCount;
		}

		protected override void RemoveChildListeners()
		{
			Model.Score.Changed -= SetScoreCount;
		}
		
		public void SetScoreCount(int val)
		{
			_scoreCount.text = val.ToString();
		}
	}
}