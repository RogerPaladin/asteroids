using Static.Effects;
using UnityEngine;
using Utils.Events;
using Utils.Reactivity;

namespace Model.Effects.Score
{
	public class EffectScoreModel : EffectModel
	{
		public Observable<int> Score { get; private set; } = new Observable<int>(0);
		
		public EffectScoreModel(EffectConfig effectConfig, UpdateSystem updateSystem) : base(effectConfig, updateSystem)
		{
		}

		public void SetScore(int score)
		{
			Score.Value = score;
		}
	}
}