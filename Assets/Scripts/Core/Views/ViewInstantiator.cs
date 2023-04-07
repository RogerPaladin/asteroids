using System;
using Model.Background;
using Model.Effects;
using Model.Enemies;
using Model.Player;
using Model.Projectiles;
using Model.Score;
using Model.WeaponInfo;
using Model.Windows;
using Static.Effects;
using Static.Enemies;
using Static.Weapons;
using Views.GamePlay.Background;
using Views.GamePlay.Effects;
using Views.GamePlay.Enemies;
using Views.GamePlay.Player;
using Views.GamePlay.Projectiles;
using Views.Hud;
using Views.Hud.PlayerInfo;
using Views.Hud.Score;
using Views.Hud.WeaponInfo;
using Views.Windows.Preloader;
using Views.Windows.Restart;
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

		public BackgroundView Instantiate(BackgroundModel model)
		{
			return Object.Instantiate(_basePrefabs.BackgroundView);
		}

		public EffectView Instantiate(EffectModel model)
		{
			switch (model.ModelId)
			{
				case EffectType.SCORE:
					return Object.Instantiate(_basePrefabs.EffectScoreView);
				default:
					throw new Exception(model.ModelId);
			}
		}

		public EnemyView Instantiate(EnemyModel model)
		{
			switch (model.ModelId)
			{
				case EnemyType.BIG_ASTEROID:
					return Object.Instantiate(_basePrefabs.BigAsteroidView);
				case EnemyType.SMALL_ASTEROID:
					return Object.Instantiate(_basePrefabs.SmallAsteroidView);
				case EnemyType.UFO:
					return Object.Instantiate(_basePrefabs.UfoView);
				default:
					throw new Exception(model.ModelId);
			}
		}

		public ProjectileView Instantiate(ProjectileModel model)
		{
			ProjectileView projectileView;
			switch (model.ModelId)
			{
				case WeaponType.BULLET:
					projectileView = _basePrefabs.BulletView;
					break;
				case WeaponType.LASER:
					projectileView = _basePrefabs.LaserView;
					break;
				default:
					throw new Exception(model.ModelId);
			}
			return Object.Instantiate(projectileView);
		}

		public PlayerShipView Instantiate(PlayerShipModel model)
		{
			return Object.Instantiate(_basePrefabs.PlayerShipView);
		}

		public PreloaderWindowView Instantiate(PreloaderWindowModel model)
		{
			return Object.Instantiate(_basePrefabs.PreloaderWindowView);
		}

		public RestartWindowView Instantiate(RestartWindowModel model)
		{
			return Object.Instantiate(_basePrefabs.RestartWindowView);
		}

		public PlayerInfoView Instantiate(PlayerInfoModel model)
		{
			return _hudView.PlayerInfoView;	
		}

		public ScoreView Instantiate(ScoreModel model)
		{
			return _hudView.ScoreView;
		}

		public WeaponInfoView Instantiate(WeaponInfoModel model)
		{
			return _hudView.WeaponInfoView;
		}
	}
}