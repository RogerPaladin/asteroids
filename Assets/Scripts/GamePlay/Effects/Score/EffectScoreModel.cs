using System.Collections.Generic;
using Assets.Scripts.Events;
using Assets.Scripts.GamePlay.Effects.Score;
using Assets.Scripts.Static.Effects;

namespace Assets.Scripts.GamePlay.Effects
{
	public class EffectScoreModel : EffectModel, IEffectScoreModelObservable
	{
		private int _score = 0;

		private List<IEffectScoreViewObserver> _observers = new List<IEffectScoreViewObserver>();
		
		public EffectScoreModel(EffectConfig effectConfig, UpdateSystem updateSystem) : base(effectConfig, updateSystem)
		{
		}

		public void SetScore(int score)
		{
			_score = score;
			
			NotifyScoreChanged();
		}

		public void RegisterObserver(IEffectScoreViewObserver o)
		{
			_observers.Add(o);
			
			o.OnScoreChange(_score);
		}

		public void RemoveObserver(IEffectScoreViewObserver o) { _observers.Remove(o); }

		public void NotifyScoreChanged()
		{
			foreach(IEffectScoreViewObserver lv in _observers)
				lv.OnScoreChange(_score);
		}
	}
}