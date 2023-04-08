using Controllers.Enemies;
using Controllers.Enemies.Asteroids;
using Controllers.Enemies.Ufos;
using Core.Controllers.ViewPort;
using Model.Enemies;
using Model.Player;
using Static.Enemies;
using Utils.Events;

namespace Factories.Enemies
{
	public class EnemyFactory
	{
		private readonly UpdateSystem _updateSystem;

		public EnemyFactory(UpdateSystem updateSystem)
		{
			_updateSystem = updateSystem;
		}

		public AbstractEnemyController Create(EnemyConfig config, PlayerShipModel playerShipModel, ViewPortController viewPortController)
		{
			var model = new EnemyModel(config, playerShipModel);

			switch (config.ModelId)
			{
				case EnemyType.BIG_ASTEROID:
					return new BigAsteroidController(model, _updateSystem, viewPortController);
				case EnemyType.SMALL_ASTEROID:
					return new SmallAsteroidController(model, _updateSystem, viewPortController);
				case EnemyType.UFO:
					return new UfoController(model, _updateSystem, viewPortController);
			}

			return null;
		}
	}
}