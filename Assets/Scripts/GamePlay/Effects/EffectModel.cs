using Assets.Scripts.Events;
using Assets.Scripts.Static.Effects;
using Assets.Scripts.Utils.MovementObserver;

namespace Assets.Scripts.GamePlay.Effects
{
	public class EffectModel : ModelMovementObservable
	{
		protected readonly EffectConfig Config;
		
		public UpdateSystem UpdateSystem { get; private set; }

		public float LifeTime { get; private set; }
		
		public string ModelId => Config.ModelId;
		public float Time => Config.Time;

		public EffectModel(EffectConfig effectConfig, UpdateSystem updateSystem)
		{
			Config = effectConfig;
			UpdateSystem = updateSystem;
		}
		
		public virtual void OnActivate()
		{
			LifeTime = Time;
		}
		
		public virtual void OnDeactivate()
		{

		}
		
		public void SetLifeTime(float lifeTime)
		{
			LifeTime = lifeTime;
		}
	}
}