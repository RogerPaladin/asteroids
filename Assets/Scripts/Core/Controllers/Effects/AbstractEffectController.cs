using System;
using Model.Effects;
using Utils;
using Utils.Events;
using UnityEngine;

namespace Controllers.Effects
{
	public abstract class AbstractEffectController: IUpdateListener, IActivateDeactivate
	{
		public EffectModel Model { get; private set; }

		public event Action<AbstractEffectController> OnDestroyEvent;

		public AbstractEffectController(EffectModel model)
		{
			Model = model;
		}
		
		public void SetParent(Transform t)
		{
			Model.SetParent(t);
		}
		
		public virtual void Activate()
		{
			Model.Activate();
			Model.UpdateSystem.AddListener(this);
		}

		public virtual void Deactivate()
		{
			Model.Deactivate();
			Model.UpdateSystem.RemoveListener(this);
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