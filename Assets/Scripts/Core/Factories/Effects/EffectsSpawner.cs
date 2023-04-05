using Controllers.Effects;
using Controllers.Effects.Score;
using Model.Enemies;
using Static;
using Static.Effects;
using Utils.DiContainers;
using Utils.Spawner;
using UnityEngine;
using Utils.DiContainers.Effects;
using Views;

namespace Factories.Effects.Score
{
	public class EffectsSpawner : AbstractSpawner<AbstractEffectController, EffectConfig>, IDiContainerChild
	{
		private readonly StaticData _staticData;
		private readonly EffectsFactory _effectsFactory;
		private readonly ViewBinder _viewBinder;
		private readonly EffectsContainer _effectsContainer;

		public EffectsSpawner(StaticData staticData, EffectsFactory effectsFactory, EffectsContainer effectsContainer, ViewBinder viewBinder)
		{
			_staticData = staticData;
			_effectsFactory = effectsFactory;
			_effectsContainer = effectsContainer;
			_viewBinder = viewBinder;
		}
		
		public override AbstractEffectController Spawn(EffectConfig config, Vector2 pos, Quaternion rotation)
		{
			var pool = GetPoolByKey(config.ModelId);
			var activeList = GetActiveListByKey(config.ModelId);
			
			var effect = pool.Get();

			if (effect == null)
			{
				effect = _effectsFactory.Create(config);
				
				_viewBinder.TryBindViewByModel(effect.Model);
				
				effect.SetParent(_effectsContainer.transform);
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
			var config = _staticData.EffectsData.GetByType(EffectType.SCORE);

			var effect = Spawn(config, enemyModel.Position.Value, Quaternion.identity) as EffectScoreController;
			effect.SetScore(enemyModel.Score);
		}

		public void OnLevelEnd()
		{
			ReturnAllActiveToPool();
		}
	}
}