using System;
using System.Linq;
using UnityEngine;

namespace Static.Catalogs
{
	[CreateAssetMenu(fileName = "EffectsDataCatalog", menuName = "Catalogs/Data/EffectsDataCatalog")]
	public class EffectsDataCatalog: ScriptableObject
	{
		[field: SerializeField] public EffectDataCatalog[] Effects { get; private set; }

		public EffectDataCatalog GetByType(EffectType type)
		{
			return Effects.FirstOrDefault(e => e.Type == type);
		}
	}
	
	[Serializable]
	public class EffectDataCatalog
	{
		[field: SerializeField] public EffectType Type { get; private set; }
        
		[field: SerializeField] public float Time { get; private set; }
	}

	public enum EffectType
	{
		Score
	}
}