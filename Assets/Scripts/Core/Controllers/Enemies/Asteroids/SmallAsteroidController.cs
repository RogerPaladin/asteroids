using Model.Enemies;
using UnityEngine;

namespace Controllers.Enemies.Asteroids
{
	public class SmallAsteroidController : AbstractEnemyController
	{
		public SmallAsteroidController(EnemyModel model) : base(model)
		{

		}

		public override void Update(float deltaTime)
		{
			Vector2 forward = Model.Rotation.Value * Vector2.up * Model.Speed;

			var pos = Model.Position.Value + forward * deltaTime;
			Model.OffScreenChecker.CheckPosition(ref pos);
			Model.SetPosition(pos);
		}
	}
}