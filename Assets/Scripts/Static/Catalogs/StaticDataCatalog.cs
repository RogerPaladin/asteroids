using UnityEngine;

namespace Static.Catalogs
{
    [CreateAssetMenu(fileName = "StaticDataCatalog", menuName = "Catalogs/Data/StaticDataCatalog")]
    public class StaticDataCatalog : ScriptableObject
    {
        [field: SerializeField] public PlayerDataCatalog PlayerDataCatalog { get; private set; }
        [field: SerializeField] public EnemiesDataCatalog EnemiesDataCatalog { get; private set; }
        [field: SerializeField] public EffectsDataCatalog EffectsDataCatalog { get; private set; }
        [field: SerializeField] public WeaponsDataCatalog WeaponsDataCatalog { get; private set; }
	}
}