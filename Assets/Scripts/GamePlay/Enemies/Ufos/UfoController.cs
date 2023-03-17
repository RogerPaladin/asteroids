using UnityEngine;

namespace Assets.Scripts.GamePlay.Enemies.Asteroids
{
	public class UfoController : AbstractEnemyController
	{
		public UfoController(EnemyModel model, EnemyView view) : base(model, view)
		{
		}

		public override void Update(float deltaTime)
		{
			Model.SetPosition(Vector2.MoveTowards(Model.Position, Model.PlayerPosition, Model.Speed * deltaTime));
		}
	}
}