using Controllers.Enemies;
using Controllers.Enemies.Asteroids;
using Controllers.Enemies.Ufos;
using Core.Controllers.ViewPort;
using Model.Enemies;
using Model.Player;
using Static.Catalogs;
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

		public AbstractEnemyController Create(EnemyDataCatalog enemyDataCatalog, PlayerShipModel playerShipModel, ViewPortController viewPortController)
		{
			var model = new EnemyModel(enemyDataCatalog, playerShipModel);

			switch (enemyDataCatalog.Type)
			{
				case EnemyType.BigAsteroid:
					return new BigAsteroidController(model, _updateSystem, viewPortController);
				case EnemyType.SmallAsteroid:
					return new SmallAsteroidController(model, _updateSystem, viewPortController);
				case EnemyType.Ufo:
					return new UfoController(model, _updateSystem, viewPortController);
			}

			return null;
		}
	}
}