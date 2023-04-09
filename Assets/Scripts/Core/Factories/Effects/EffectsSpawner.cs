using Controllers.Effects;
using Controllers.Effects.Score;
using Model.Enemies;
using Static.Catalogs;
using Utils.Spawner;
using UnityEngine;
using Views.Catalogs;
using EffectType = Static.Catalogs.EffectType;

namespace Factories.Effects.Score
{
	public class EffectsSpawner : AbstractSpawner<AbstractEffectController, EffectDataCatalog>
	{
		private readonly EffectsDataCatalog _effectsDataCatalog;
		private readonly EffectsFactory _effectsFactory;
		private readonly EffectsViewCatalog _effectsViewCatalog;
		private readonly Transform _effectsContainer;

		public EffectsSpawner(EffectsDataCatalog effectsDataCatalog, EffectsFactory effectsFactory, Transform effectsContainer, EffectsViewCatalog effectsViewCatalog)
		{
			_effectsDataCatalog = effectsDataCatalog;
			_effectsFactory = effectsFactory;
			_effectsContainer = effectsContainer;
			_effectsViewCatalog = effectsViewCatalog;
		}
		
		public override AbstractEffectController Spawn(EffectDataCatalog effectDataCatalog, Vector2 pos,
													   Quaternion rotation)
		{
			var pool = GetPoolByKey(effectDataCatalog.Type.ToString());
			var activeList = GetActiveListByKey(effectDataCatalog.Type.ToString());
			
			var effect = pool.Get();

			if (effect == null)
			{
				effect = _effectsFactory.Create(effectDataCatalog);
				
				var view = _effectsViewCatalog.Create(effect.Model);
				view.BindModel(effect.Model);
				
				view.SetParent(_effectsContainer, false);
				effect.OnDestroyEvent += OnObjDestroy;
			}
			
			effect.SetPosition(pos);
			effect.Activate();

			activeList.Add(effect);
			
			return effect;
		}
		
		protected override string GetKey(AbstractEffectController obj) => obj.Model.ModelId;

		public void OnEnemyDestroy(EnemyModel enemyModel)
		{
			var effectDataCatalog = _effectsDataCatalog.GetByType(EffectType.Score);

			var effect = Spawn(effectDataCatalog, enemyModel.Position.Value, Quaternion.identity) as EffectScoreController;
			effect.SetScore(enemyModel.Score);
		}

		public void OnLevelEnd()
		{
			ReturnAllActiveToPool();
		}
	}
}