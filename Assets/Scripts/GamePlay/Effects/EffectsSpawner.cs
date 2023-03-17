using Assets.Scripts.GamePlay.Enemies;
using Assets.Scripts.Static;
using Assets.Scripts.Static.Effects;
using Assets.Scripts.Utils.Spawner;
using UnityEngine;

namespace Assets.Scripts.GamePlay.Effects.Score
{
	public class EffectsSpawner : AbstractSpawner<AbstractEffectController, EffectConfig>
	{
		private readonly StaticData _staticData;
		private readonly EffectsFactory _effectsFactory;

		public EffectsSpawner(StaticData staticData, EffectsFactory effectsFactory)
		{
			_staticData = staticData;
			_effectsFactory = effectsFactory;
		}
		
		public override AbstractEffectController Spawn(EffectConfig config, Vector2 pos, Quaternion rotation)
		{
			var pool = GetPoolByKey(config.ModelId);
			var activeList = GetActiveListByKey(config.ModelId);
			
			var effect = pool.Get();

			if (effect == null)
			{
				effect = _effectsFactory.Create(config);
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

			var effect = Spawn(config, enemyModel.Position, Quaternion.identity) as EffectScoreController;
			effect.SetScore(enemyModel.Score);
		}

		public void OnLevelEnd()
		{
			ReturnAllActiveToPool();
		}
	}
}