using Model.Player;
using Static.Catalogs;
using Utils.MovementObserver;
using UnityEngine;
using Utils;
using Utils.Reactivity;

namespace Model.Enemies
{
	public class EnemyModel : ModelMovementObservable, IModel, IActivateDeactivate
	{
		public readonly EnemyDataCatalog EnemyDataCatalog;
		private readonly PlayerShipModel _playerShipModel;

		public float Speed => EnemyDataCatalog.Speed;
		public int Score => EnemyDataCatalog.Score;
		public string ModelId => EnemyDataCatalog.Type.ToString();
		
		public Vector2 PlayerPosition => _playerShipModel.Position.Value;
		
		public Observable<bool> IsActive { get; } = new Observable<bool>(false);

		public EnemyModel(EnemyDataCatalog enemyDataCatalog, PlayerShipModel playerShipModel)
		{
			EnemyDataCatalog = enemyDataCatalog;
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