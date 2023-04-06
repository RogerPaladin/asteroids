using Static.Effects;
using Utils.Reactivity;

namespace Model.Effects.Score
{
	public class EffectScoreModel : EffectModel
	{
		public Observable<int> Score { get; private set; } = new Observable<int>(0);
		
		public EffectScoreModel(EffectConfig effectConfig) : base(effectConfig)
		{
		}

		public void SetScore(int score)
		{
			Score.Value = score;
		}
	}
}