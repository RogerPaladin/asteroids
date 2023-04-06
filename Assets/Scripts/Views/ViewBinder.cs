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
using UnityEngine;
using Utils.DiContainers;
using Views.GamePlay.Enemies;
using Views.GamePlay.Projectiles;
using Views.Hud;

namespace Views
{
	public class ViewBinder
	{
		private readonly BasePrefabs _basePrefabs;
		private readonly HudView _hudView;

		public ViewBinder(BasePrefabs basePrefabs, HudView hudView)
		{
			_basePrefabs = basePrefabs;
			_hudView = hudView;
		}
		
		public AbstractView TryBindViewByModel(IModel model)
		{
			switch (model)
			{
				case BackgroundModel:
					return InstantiateAndBind(_basePrefabs.BackgroundView, model);
				case EffectScoreModel:
					return InstantiateAndBind(_basePrefabs.EffectScoreView, model);
				case EnemyModel:
				{
					var config = model as EnemyModel;
					EnemyView enemyView = null;
					
					switch (config.ModelId)
					{
						case EnemyType.BIG_ASTEROID:
							enemyView = _basePrefabs.BigAsteroidView;
							break;
						case EnemyType.SMALL_ASTEROID:
							enemyView = _basePrefabs.SmallAsteroidView;
							break;
						case EnemyType.UFO:
							enemyView = _basePrefabs.UfoView;
							break;
					}
					return InstantiateAndBind(enemyView, model);
				}
				case ProjectileModel:
				{
					var config = model as ProjectileModel;
					ProjectileView projectileView = null;
					
					switch (config.ModelId)
					{
						case WeaponType.BULLET:
							projectileView = _basePrefabs.BulletView;
							break;
						case WeaponType.LASER:
							projectileView = _basePrefabs.LaserView;
							break;
					}
					
					return InstantiateAndBind(projectileView, model);
				}
				case PlayerShipModel:
					return InstantiateAndBind(_basePrefabs.PlayerShipView, model);
				case PlayerInfoModel:
					return Bind(_hudView.PlayerInfoView, model);				
				case ScoreModel:
					return Bind(_hudView.ScoreView, model);
				case WeaponInfoModel:
					return Bind(_hudView.WeaponInfoView, model);
			}

			return null;
		}
		
		private AbstractView InstantiateAndBind(AbstractView view, IModel model, Transform transform = null)
		{
			var instance = Object.Instantiate(view, transform);
			instance.BindModel(model);
			return instance;
		}
		
		private AbstractView Bind(AbstractView view, IModel model)
		{
			view.BindModel(model);
			return view;
		}
	}
}