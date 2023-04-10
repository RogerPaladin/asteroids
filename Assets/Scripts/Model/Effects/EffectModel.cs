using Static.Catalogs;
using Utils;
using Utils.MovementObserver;
using Utils.Reactivity;

namespace Model.Effects
{
	public class EffectModel : ModelMovementObservable, IModel, IActivateDeactivate
	{
		public readonly EffectDataCatalog EffectDataCatalog;
		
		public float LifeTime { get; private set; }
		
		public Observable<bool> IsActive { get; } = new Observable<bool>(false);
		
		public string ModelId => EffectDataCatalog.Type.ToString();
		public float Time => EffectDataCatalog.Time;

		public EffectModel(EffectDataCatalog effectDataCatalog)
		{
			EffectDataCatalog = effectDataCatalog;
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