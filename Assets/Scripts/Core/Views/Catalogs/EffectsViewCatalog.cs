using System;
using System.Linq;
using Model.Effects;
using Static.Catalogs;
using UnityEngine;
using Views.GamePlay.Effects;

namespace Views.Catalogs
{
    [CreateAssetMenu(fileName = "EffectsViewCatalog", menuName = "Catalogs/View/EffectsViewCatalog")]
    public class EffectsViewCatalog : ScriptableObject
    {
        [field: SerializeField] public EffectViewCatalog[] Effects { get; private set; }

		public EffectViewCatalog GetByType(EffectType type)
		{
			return Effects.FirstOrDefault(e => e.Type == type);
		}
		
		public EffectView Create(EffectModel effectModel)
		{
			var effectViewCatalog = GetByType(effectModel.EffectDataCatalog.Type);

			if (effectViewCatalog == null)
				throw new Exception(effectModel.EffectDataCatalog.Type.ToString());
			
			return Instantiate(effectViewCatalog.View);
		}
	}

    [Serializable]
    public class EffectViewCatalog
    {
        [field: SerializeField] public EffectType Type { get; private set; }
        [field: SerializeField] public EffectView View { get; private set; }
    }
}