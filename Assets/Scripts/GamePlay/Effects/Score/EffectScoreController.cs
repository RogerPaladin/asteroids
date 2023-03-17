namespace Assets.Scripts.GamePlay.Effects.Score
{
	public class EffectScoreController: AbstractEffectController
	{
		private EffectScoreModel _scoreModel => Model as EffectScoreModel;
		private EffectScoreView _scoreView => View as EffectScoreView;

		public EffectScoreController(EffectScoreModel model, EffectScoreView view) : base(model, view)
		{
		}
		
		public override void Activate()
		{
			base.Activate();
			
			_scoreModel.RegisterObserver(_scoreView);
		}

		public override void Deactivate()
		{
			base.Deactivate();
			
			_scoreModel.RemoveObserver(_scoreView);
		}

		public override void Update(float deltaTime)
		{
			CheckLifeTime(deltaTime);
		}
		
		public void SetScore(int score)
		{
			_scoreModel.SetScore(score);
		}
	}
}