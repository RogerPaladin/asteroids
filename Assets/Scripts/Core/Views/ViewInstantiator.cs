using System;
using Model;
using Model.Background;
using Model.Effects.Score;
using Model.Enemies;
using Model.Player;
using Model.Projectiles;
using Model.Score;
using Model.WeaponInfo;
using Static.Enemies;
using Static.Weapons;
using Views.GamePlay.Projectiles;
using Views.Hud;
using Object = UnityEngine.Object;

namespace Views
{
	public class ViewInstantiator
	{
		private readonly BasePrefabs _basePrefabs;
		private readonly HudView _hudView;

		public ViewInstantiator(BasePrefabs basePrefabs, HudView hudView)
		{
			_basePrefabs = basePrefabs;
			_hudView = hudView;
		}
		
		public AbstractView Instantiate(IModel model)
		{
			switch (model)
			{
				case BackgroundModel:
					return Object.Instantiate(_basePrefabs.BackgroundView);
				case EffectScoreModel:
					return Object.Instantiate(_basePrefabs.EffectScoreView);
				case EnemyModel enemyModel:
				{
					switch (enemyModel.ModelId)
					{
						case EnemyType.BIG_ASTEROID:
							return Object.Instantiate(_basePrefabs.BigAsteroidView);
						case EnemyType.SMALL_ASTEROID:
							return Object.Instantiate(_basePrefabs.SmallAsteroidView);
						case EnemyType.UFO:
							return Object.Instantiate(_basePrefabs.UfoView);
						default:
							throw new Exception(enemyModel.ModelId);
					}
				}
				case ProjectileModel config:
				{
					ProjectileView projectileView;
					switch (config.ModelId)
					{
						case WeaponType.BULLET:
							projectileView = _basePrefabs.BulletView;
							break;
						case WeaponType.LASER:
							projectileView = _basePrefabs.LaserView;
							break;
						default:
							throw new Exception(config.ModelId);
					}
					return Object.Instantiate(projectileView);
				}
				case PlayerShipModel:
					return Object.Instantiate(_basePrefabs.PlayerShipView);
				case PlayerInfoModel:
					return _hudView.PlayerInfoView;				
				case ScoreModel:
					return _hudView.ScoreView;
				case WeaponInfoModel:
					return _hudView.WeaponInfoView;
			}

			return null;
		}
	}
}