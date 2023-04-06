using Core.Controllers.ViewPort;
using Model.Enemies;
using UnityEngine;
using Utils.Events;

namespace Controllers.Enemies.Asteroids
{
	public class SmallAsteroidController : AbstractEnemyController
	{
		public SmallAsteroidController(EnemyModel model, UpdateSystem updateSystem,
									   ViewPortController viewPortController) : base(model, updateSystem, viewPortController)
		{

		}

		public override void Update(float deltaTime)
		{
			Vector2 forward = Model.Rotation.Value * Vector2.up * Model.Speed;

			var pos = Model.Position.Value + forward * deltaTime;
			ViewPortController.CheckPosition(ref pos);
			Model.SetPosition(pos);
		}
	}
}