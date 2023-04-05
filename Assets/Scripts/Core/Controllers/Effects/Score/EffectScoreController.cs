using Model.Effects.Score;

namespace Controllers.Effects.Score
{
	public class EffectScoreController: AbstractEffectController
	{
		private EffectScoreModel Model => base.Model as EffectScoreModel;

		public EffectScoreController(EffectScoreModel model) : base(model)
		{
		}

		public override void Update(float deltaTime)
		{
			CheckLifeTime(deltaTime);
		}
		
		public void SetScore(int score)
		{
			Model.SetScore(score);
		}
	}
}