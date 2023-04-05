namespace Views.GamePlay.Effects.Score
{
	public interface IEffectScoreView : IEffectView
	{
		void OnScoreChange(int score);
	}
}