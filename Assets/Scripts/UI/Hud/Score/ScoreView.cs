using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Hud.Score
{
	public class ScoreView: MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _scoreCount;
		
		public void SetScoreCount(int val)
		{
			_scoreCount.text = val.ToString();
		}
	}
}