using Core.Controllers.ViewPort;
using Model.Enemies;
using UnityEngine;
using Utils.Events;

namespace Controllers.Enemies.Asteroids
{
	public class BigAsteroidController : AbstractEnemyController
	{
		public BigAsteroidController(EnemyModel model, UpdateSystem updateSystem, ViewPortController viewPortController) : base(model, updateSystem, viewPortController)
		{
		}

		public override void Activate()
		{
			base.Activate();
			
			Model.SetVelocity(new Vector2(Random.Range(-1f, 1f) * Model.Speed, Random.Range(-1f, 1f) * Model.Speed));
		}

		public override void Update(float deltaTime)
		{
			var pos = Model.Position.Value + Model.Velocity.Value * deltaTime;
			ViewPortController.CheckPosition(ref pos);
			Model.SetPosition(pos);
		}
	}
}