using Model.Score;
using TMPro;
using UnityEngine;

namespace Views.Hud.Score
{
	public class ScoreView: AbstractView<ScoreModel>, IScoreView
	{
		[SerializeField] private TextMeshProUGUI _scoreCount;

		protected override void SyncModel()
		{
			SetScoreCount(Model.Score.Value);
		}
		
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