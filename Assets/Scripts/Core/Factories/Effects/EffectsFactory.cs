using Controllers.Effects;
using Controllers.Effects.Score;
using Model.Effects.Score;
using Static.Effects;

using Utils.Events;

namespace Factories.Effects
{
	public class EffectsFactory
	{
		private readonly UpdateSystem _updateSystem;

		public EffectsFactory(UpdateSystem updateSystem)
		{
			_updateSystem = updateSystem;
		}

		public AbstractEffectController Create(EffectConfig config)
		{
			switch (config.ModelId)
			{
				case EffectType.SCORE:
					var model = new EffectScoreModel(config);
					return new EffectScoreController(model, _updateSystem);
			}

			return null;
		}
	}
}