using System;
using Utils.Collisions;
using Utils.MovementObserver;

namespace Views.GamePlay.Enemies
{
	public interface IEnemyView : IMovementObserver, ICollisionDetector
	{
		event Action OnCollisionEvent;
	}
}