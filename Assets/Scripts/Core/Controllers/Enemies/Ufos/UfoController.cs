using Core.Controllers.ViewPort;
using Model.Enemies;
using UnityEngine;

namespace Controllers.Enemies.Ufos
{
	public class UfoController : AbstractEnemyController
	{
		public UfoController(EnemyModel model, ViewPortController viewPortController) : base(model, viewPortController)
		{
		}

		public override void Update(float deltaTime)
		{
			Model.SetPosition(Vector2.MoveTowards(Model.Position.Value, Model.PlayerPosition, Model.Speed * deltaTime));
		}
	}
}