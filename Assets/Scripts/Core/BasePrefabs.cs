using Assets.Scripts.GamePlay.Background;
using Assets.Scripts.GamePlay.Effects.Score;
using Assets.Scripts.GamePlay.Enemies;
using Assets.Scripts.GamePlay.Player;
using Assets.Scripts.GamePlay.Projectiles;
using Assets.Scripts.GamePlay.Projectiles.Laser;
using UnityEngine;

namespace Assets.Scripts.Core
{
	public class BasePrefabs : MonoBehaviour
	{
		public PlayerShipView PlayerShipView;
		public EnemyView BigAsteroidView;
		public EnemyView SmallAsteroidView;
		public EnemyView UfoView;
		public ProjectileView BulletView;
		public ProjectileLaserView LaserView;
		public BackgroundView BackgroundView;
		public EffectScoreView EffectScoreView;
	}
}