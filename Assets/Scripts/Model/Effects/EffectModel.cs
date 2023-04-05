using Static.Effects;
using UnityEngine;
using Utils;
using Utils.Events;
using Utils.MovementObserver;
using Utils.Reactivity;

namespace Model.Effects
{
	public class EffectModel : ModelMovementObservable, IModel, IActivateDeactivate
	{
		protected readonly EffectConfig Config;
		
		public UpdateSystem UpdateSystem { get; private set; }

		public float LifeTime { get; private set; }
		
		public Observable<Transform> Parent { get; private set; } = new Observable<Transform>(null);
		public Observable<bool> IsActive { get; private set; } = new Observable<bool>(false);
		
		public string ModelId => Config.ModelId;
		public float Time => Config.Time;

		public EffectModel(EffectConfig effectConfig, UpdateSystem updateSystem)
		{
			Config = effectConfig;
			UpdateSystem = updateSystem;
		}
		
		public void SetParent(Transform t)
		{
			Parent.Value = t;
		}
		
		public virtual void Activate()
		{
			Parent.Notify();
			
			LifeTime = Time;

			IsActive.Value = true;
		}
		
		public virtual void Deactivate()
		{
			IsActive.Value = false;
		}
		
		public void SetLifeTime(float lifeTime)
		{
			LifeTime = lifeTime;
		}
	}
}