using System;
using Model.Effects;
using Utils;
using Utils.Events;
using UnityEngine;

namespace Controllers.Effects
{
	public abstract class AbstractEffectController: IUpdateListener, IActivateDeactivate
	{
		private readonly UpdateSystem _updateSystem;
		public EffectModel Model { get; }

		//todo!!!! Все ивенты идут в модели. Уменьшаем связность контроллеров
		public event Action<AbstractEffectController> OnDestroyEvent;

		public AbstractEffectController(EffectModel model, UpdateSystem updateSystem)
		{
			_updateSystem = updateSystem;
			Model = model;
		}
		
		public virtual void Activate()
		{
			Model.Activate();
			_updateSystem.AddListener(this);
		}

		public virtual void Deactivate()
		{
			Model.Deactivate();
			_updateSystem.RemoveListener(this);
		}

		public abstract void Update(float deltaTime);

		public void SetPosition(Vector2 pos)
		{
			Model.SetPosition(pos);
		}

		protected void CheckLifeTime(float deltaTime)
		{
			Model.SetLifeTime(Model.LifeTime - deltaTime);

			if (Model.LifeTime <= 0)
				OnLifeTimeEnd();
		}

		private void OnLifeTimeEnd()
		{
			OnDestroy();
		}
		
		protected void OnDestroy()
		{
			Deactivate();
			
			OnDestroyEvent?.Invoke(this);
		}
	}
}