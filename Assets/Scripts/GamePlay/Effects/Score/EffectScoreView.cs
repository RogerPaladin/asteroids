using TMPro;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Effects.Score
{
	public class EffectScoreView: EffectView, IEffectScoreViewObserver
	{
		[SerializeField] private TextMeshProUGUI _scoreText;
		[SerializeField] private Animator _animator;
		
		public void OnScoreChange(int score)
		{
			_scoreText.text = score.ToString();
		}
	}
}