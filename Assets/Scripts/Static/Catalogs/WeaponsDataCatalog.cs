using System;
using System.Linq;
using UnityEngine;

namespace Static.Catalogs
{
    [CreateAssetMenu(fileName = "WeaponsDataCatalog", menuName = "Catalogs/Data/WeaponsDataCatalog")]
    public class WeaponsDataCatalog : ScriptableObject
    {
        [field: SerializeField] public WeaponDataCatalog[] Weapons { get; private set; }

		public WeaponDataCatalog GetByType(WeaponType weaponType)
		{
			return Weapons.FirstOrDefault(w => w.Type == weaponType);
		}
	}

    [Serializable]
    public class WeaponDataCatalog
    {
        [field: SerializeField] public WeaponType Type { get; private set; }
		
        [field: SerializeField] public int AmmoStartCount { get; private set; }
        [field: SerializeField] public int AmmoMaxCount { get; private set; }
        [field: SerializeField] public float AmmoRefreshTime { get; private set; }
		[field: SerializeField] public float ProjectileSpeed { get; private set; }
		[field: SerializeField] public float ProjectileLifeTime { get; private set; }
        [field: SerializeField] public float ProjectileGrowTime { get; private set; }
        [field: SerializeField] public float ProjectileGrowSpeed { get; private set; }
		[field: SerializeField] public bool ProjectileNeedDestroyOnCollision { get; private set; }
	}

    public enum WeaponType
    {
        Bullet,
        Laser
	}
}