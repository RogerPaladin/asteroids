
using Views.GamePlay.Background;
using Views.GamePlay.Effects.Score;
using Views.GamePlay.Enemies;
using Views.GamePlay.Player;
using Views.GamePlay.Projectiles.Laser;
using UnityEngine;
using Views.GamePlay.Projectiles.Bullet;
using Views.Windows.Preloader;
using Views.Windows.Restart;

namespace Views
{
	public class BasePrefabs : MonoBehaviour
	{
		public PlayerShipView PlayerShipView;
		public EnemyView BigAsteroidView;
		public EnemyView SmallAsteroidView;
		public EnemyView UfoView;
		public ProjectileBulletView BulletView;
		public ProjectileLaserView LaserView;
		public BackgroundView BackgroundView;
		public EffectScoreView EffectScoreView;
		
		public PreloaderWindowView PreloaderWindowView;
		public RestartWindowView RestartWindowView;

	}
}