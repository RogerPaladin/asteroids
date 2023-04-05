using Utils.DiContainers;
using Views.GamePlay.Background;
using Views.GamePlay.Effects.Score;
using Views.GamePlay.Enemies;
using Views.GamePlay.Player;
using Views.GamePlay.Projectiles.Laser;
using UnityEngine;
using Views.GamePlay.Projectiles.Bullet;

namespace Views
{
	public class BasePrefabs : MonoBehaviour, IDiContainerChild
	{
		public PlayerShipView PlayerShipView;
		public EnemyView BigAsteroidView;
		public EnemyView SmallAsteroidView;
		public EnemyView UfoView;
		public ProjectileBulletView BulletView;
		public ProjectileLaserView LaserView;
		public BackgroundView BackgroundView;
		public EffectScoreView EffectScoreView;
	}
}