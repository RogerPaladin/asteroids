using Model.Effects.Score;
using Utils.Events;

namespace Controllers.Effects.Score
{
	public class EffectScoreController: AbstractEffectController
	{
		private EffectScoreModel Model => base.Model as EffectScoreModel;

		public EffectScoreController(EffectScoreModel model, UpdateSystem updateSystem) : base(model, updateSystem)
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