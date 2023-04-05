using Model.Effects.Score;
using TMPro;
using UnityEngine;

namespace Views.GamePlay.Effects.Score
{
	public class EffectScoreView: EffectView, IEffectScoreView
	{
		[SerializeField] private TextMeshProUGUI _scoreText;
		[SerializeField] private Animator _animator;
		
		private EffectScoreModel Model => base.Model as EffectScoreModel;

		protected override void AddChildListeners()
		{
			base.AddChildListeners();
			
			Model.Score.Changed += OnScoreChange;
		}

		protected override void RemoveChildListeners()
		{
			base.RemoveChildListeners();
			
			Model.Score.Changed -= OnScoreChange;
		}

		public void OnScoreChange(int score)
		{
			_scoreText.text = score.ToString();
		}
	}
}