using Static.Effects;
using UnityEngine;
using Utils;
using Utils.MovementObserver;
using Utils.Reactivity;

namespace Model.Effects
{
	public class EffectModel : ModelMovementObservable, IModel, IActivateDeactivate
	{
		protected readonly EffectConfig Config;
		
		public float LifeTime { get; private set; }
		
		public Observable<bool> IsActive { get; private set; } = new Observable<bool>(false);
		
		public string ModelId => Config.ModelId;
		public float Time => Config.Time;

		public EffectModel(EffectConfig effectConfig)
		{
			Config = effectConfig;
		}
		
		public virtual void Activate()
		{
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