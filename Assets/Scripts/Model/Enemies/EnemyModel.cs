using Model.Player;
using Static.Enemies;
using Utils.MovementObserver;
using UnityEngine;
using Utils;
using Utils.Reactivity;

namespace Model.Enemies
{
	public class EnemyModel : ModelMovementObservable, IModel, IActivateDeactivate
	{
		private readonly EnemyConfig _config;
		private readonly PlayerShipModel _playerShipModel;

		public float Speed => _config.Speed;
		public int Score => _config.Score;
		public string ModelId => _config.ModelId;
		
		public Vector2 PlayerPosition => _playerShipModel.Position.Value;
		
		public Observable<bool> IsActive { get; } = new Observable<bool>(false);

		public EnemyModel(EnemyConfig config, PlayerShipModel playerShipModel)
		{
			_config = config;
			_playerShipModel = playerShipModel;
		}
		
		public void Activate()
		{
			IsActive.Value = true;
		}
		
		public void Deactivate()
		{
			IsActive.Value = false;
		}
	}
}