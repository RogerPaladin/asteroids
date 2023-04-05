using Model.Player;
using Static.Enemies;
using Utils.Events;
using Utils.MovementObserver;
using Utils.OffScreenChecker;
using UnityEngine;
using Utils;
using Utils.Reactivity;

namespace Model.Enemies
{
	public class EnemyModel : ModelMovementObservable, IModel, IActivateDeactivate
	{
		private readonly EnemyConfig _config;
		private readonly PlayerShipModel _playerShipModel;

		public UpdateSystem UpdateSystem { get; private set; }
		public OffScreenCheckerTeleport OffScreenChecker { get; private set; }
		
		public float Speed => _config.Speed;
		public int Score => _config.Score;
		public string ModelId => _config.ModelId;
		
		public Vector2 PlayerPosition => _playerShipModel.Position.Value;
		
		public Observable<Transform> Parent { get; private set; } = new Observable<Transform>(null);
		public Observable<bool> IsActive { get; private set; } = new Observable<bool>(false);

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
		
		public void SetParent(Transform t)
		{
			Parent.Value = t;
		}

		public void Activate()
		{
			Parent.Notify();
			
			Position.Notify();
			Rotation.Notify();
			Velocity.Notify();

			IsActive.Value = true;
		}
		
		public void Deactivate()
		{
			IsActive.Value = false;
		}
	}
}