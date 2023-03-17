using Assets.Scripts.Events;
using Assets.Scripts.GamePlay.Player;
using Assets.Scripts.Static.Enemies;
using Assets.Scripts.Utils.MovementObserver;
using Assets.Scripts.Utils.OffScreenChecker;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Enemies
{
	public class EnemyModel : ModelMovementObservable
	{
		private readonly EnemyConfig _config;
		private readonly PlayerShipModel _playerShipModel;

		public UpdateSystem UpdateSystem { get; private set; }
		public OffScreenCheckerTeleport OffScreenChecker { get; private set; }
		
		public float Speed => _config.Speed;
		public int Score => _config.Score;
		public string ModelId => _config.ModelId;
		
		public Vector2 PlayerPosition => _playerShipModel.Position;

		public EnemyModel(EnemyConfig config,
						  UpdateSystem updateSystem,
						  OffScreenCheckerTeleport offScreenChecker,
						  PlayerShipModel playerShipModel)
		{
			_config = config;
			UpdateSystem = updateSystem;
			OffScreenChecker = offScreenChecker;
			_playerShipModel = playerShipModel;
		}

		public void OnActivate()
		{

		}
		
		public void OnDeactivate()
		{

		}
	}
}