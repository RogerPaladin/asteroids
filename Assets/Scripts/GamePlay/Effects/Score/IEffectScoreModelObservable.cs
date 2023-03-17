namespace Assets.Scripts.GamePlay.Effects.Score
{
	public interface IEffectScoreModelObservable
	{
		void RegisterObserver(IEffectScoreViewObserver o);
		void RemoveObserver(IEffectScoreViewObserver o);
		void NotifyScoreChanged();
	}
}