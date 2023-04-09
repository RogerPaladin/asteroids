using Static.Catalogs;
using Utils.Reactivity;

namespace Model.Effects.Score
{
	public class EffectScoreModel : EffectModel
	{
		public Observable<int> Score { get; } = new Observable<int>(0);
		
		public EffectScoreModel(EffectDataCatalog effectDataCatalog) : base(effectDataCatalog)
		{
		}

		public void SetScore(int score)
		{
			Score.Value = score;
		}
	}
}