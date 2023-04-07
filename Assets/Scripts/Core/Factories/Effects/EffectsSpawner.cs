using Controllers.Effects;
using Controllers.Effects.Score;
using Model.Enemies;
using Static;
using Static.Effects;
using Utils.Spawner;
using UnityEngine;
using Views;

namespace Factories.Effects.Score
{
	public class EffectsSpawner : AbstractSpawner<AbstractEffectController, EffectConfig>
	{
		private readonly StaticData _staticData;
		private readonly EffectsFactory _effectsFactory;
		private readonly ViewInstantiator _viewInstantiator;
		private readonly Transform _effectsContainer;

		public EffectsSpawner(StaticData staticData, EffectsFactory effectsFactory, Transform effectsContainer, ViewInstantiator viewInstantiator)
		{
			_staticData = staticData;
			_effectsFactory = effectsFactory;
			_effectsContainer = effectsContainer;
			_viewInstantiator = viewInstantiator;
		}
		
		public override AbstractEffectController Spawn(EffectConfig config, Vector2 pos, Quaternion rotation)
		{
			var pool = GetPoolByKey(config.ModelId);
			var activeList = GetActiveListByKey(config.ModelId);
			
			var effect = pool.Get();

			if (effect == null)
			{
				effect = _effectsFactory.Create(config);
				
				var view = _viewInstantiator.Instantiate(effect.Model);
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