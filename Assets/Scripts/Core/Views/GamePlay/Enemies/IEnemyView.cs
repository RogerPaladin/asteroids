using System;
using UnityEngine;
using Utils.Collisions;
using Utils.MovementObserver;

namespace Views.GamePlay.Enemies
{
	public interface IEnemyView : IView, IMovementObserver, ICollisionDetector
	{
		event Action OnCollisionEvent;
	}
}