using UnityEngine;

namespace Assets.Scripts.GamePlay.Enemies.Asteroids
{
	public class SmallAsteroidController : AbstractEnemyController
	{
		public SmallAsteroidController(EnemyModel model, EnemyView view) : base(model, view)
		{

		}

		public override void Update(float deltaTime)
		{
			Vector2 forward = Model.Rotation * Vector2.up * Model.Speed;

			var pos = Model.Position + forward * deltaTime;
			Model.OffScreenChecker.CheckPosition(ref pos);
			Model.SetPosition(pos);
		}
	}
}