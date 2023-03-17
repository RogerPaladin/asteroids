using UnityEngine;

namespace Assets.Scripts.GamePlay.Enemies.Asteroids
{
	public class BigAsteroidController : AbstractEnemyController
	{
		public BigAsteroidController(EnemyModel model, EnemyView view) : base(model, view)
		{
		}

		public override void Activate()
		{
			base.Activate();
			
			Model.SetVelocity(new Vector2(Random.Range(-1f, 1f) * Model.Speed, Random.Range(-1f, 1f) * Model.Speed));
		}

		public override void Update(float deltaTime)
		{
			var pos = Model.Position + Model.Velocity * deltaTime;
			Model.OffScreenChecker.CheckPosition(ref pos);
			Model.SetPosition(pos);
		}
	}
}