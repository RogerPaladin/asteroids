using Core.Controllers.ViewPort;
using Model.Enemies;
using UnityEngine;
using Utils.Events;

namespace Controllers.Enemies.Ufos
{
	public class UfoController : AbstractEnemyController
	{
		public UfoController(EnemyModel model, UpdateSystem updateSystem, ViewPortController viewPortController) : base(model, updateSystem, viewPortController)
		{
		}

		public override void Update(float deltaTime)
		{
			Model.SetPosition(Vector2.MoveTowards(Model.Position.Value, Model.PlayerPosition, Model.Speed * deltaTime));
		}
	}
}