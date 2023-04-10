using Controllers.Effects;
using Controllers.Effects.Score;
using Model.Effects.Score;
using Static.Catalogs;
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

		public AbstractEffectController Create(EffectDataCatalog effectDataCatalog)
		{
			switch (effectDataCatalog.Type)
			{
				case EffectType.Score:
					var model = new EffectScoreModel(effectDataCatalog);
					return new EffectScoreController(model, _updateSystem);
			}

			return null;
		}
	}
}