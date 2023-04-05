using Controllers.Effects;
using Controllers.Effects.Score;
using Model.Effects.Score;
using Static.Effects;
using Utils.DiContainers;
using Utils.Events;

namespace Factories.Effects
{
	public class EffectsFactory : IDiContainerChild
	{
		private readonly DiContainer _diContainer;
		private readonly UpdateSystem _updateSystem;

		public EffectsFactory(DiContainer diContainer)
		{
			_diContainer = diContainer;
			
			_updateSystem = _diContainer.Resolve<UpdateSystem>();
		}

		public AbstractEffectController Create(EffectConfig config)
		{
			switch (config.ModelId)
			{
				case EffectType.SCORE:
					var model = new EffectScoreModel(config, _updateSystem);
					return new EffectScoreController(model);
			}

			return null;
		}
	}
}