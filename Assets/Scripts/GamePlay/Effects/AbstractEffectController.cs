using System;
using Assets.Scripts.Events;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Effects
{
	public abstract class AbstractEffectController: IUpdateListener, IActivateDeactivate
	{
		public EffectModel Model { get; private set; }
		public EffectView View { get; private set; }
		
		public event Action<AbstractEffectController> OnDestroyEvent;

		public AbstractEffectController(EffectModel model, EffectView view)
		{
			Model = model;
			View = view;
		}
		
		public virtual void Activate()
		{
			Model.RegisterObserver(View);
			Model.OnActivate();
			View.OnActivate();
			Model.UpdateSystem.AddListener(this);
		}

		public virtual void Deactivate()
		{
			Model.RemoveObserver(View);
			Model.OnDeactivate();
			View.OnDeactivate();
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