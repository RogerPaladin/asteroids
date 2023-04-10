using UnityEngine;

namespace Views.Catalogs
{
    [CreateAssetMenu(fileName = "ViewsCatalog", menuName = "Catalogs/View/ViewsCatalog")]
    public class ViewsCatalog : ScriptableObject
    {
        [field: SerializeField] public PlayerViewCatalog PlayerViewCatalog { get; private set; }
        [field: SerializeField] public EnemiesViewCatalog EnemiesViewCatalog { get; private set; }
        [field: SerializeField] public EffectsViewCatalog EffectsViewCatalog { get; private set; }
        [field: SerializeField] public ProjectilesViewCatalog ProjectilesViewCatalog { get; private set; }
        [field: SerializeField] public WindowsViewCatalog WindowsViewCatalog { get; private set; }
        [field: SerializeField] public BackgroundViewCatalog BackgroundViewCatalog { get; private set; }
	}
}