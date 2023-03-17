using Assets.Scripts.Core;
using Assets.Scripts.Events;
using Assets.Scripts.GamePlay.Effects.Score;
using Assets.Scripts.Static.Effects;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Effects
{
	public class EffectsFactory
	{
		private readonly DiContainer _diContainer;
		private readonly UpdateSystem _updateSystem;
		private readonly EffectScoreView _effectScoreView;
		private readonly EffectsContainer _effectsContainer;

		public EffectsFactory(DiContainer diContainer)
		{
			_diContainer = diContainer;
			
			_effectScoreView = _diContainer.Resolve<BasePrefabs>().EffectScoreView;
			_updateSystem = _diContainer.Resolve<UpdateSystem>();
			_effectsContainer = _diContainer.Resolve<EffectsContainer>();
		}

		public AbstractEffectController Create(EffectConfig config)
		{
			switch (config.ModelId)
			{
				case EffectType.SCORE:
					var view = Object.Instantiate(_effectScoreView, _effectsContainer.transform);
					var model = new EffectScoreModel(config, _updateSystem);
					return new EffectScoreController(model, view);
			}

			return null;
		}
	}
}