using System;
using Model.Player;
using UnityEngine;
using Views.GamePlay.Player;

namespace Views.Catalogs
{
	[Serializable]
	[CreateAssetMenu(fileName = "PlayerViewCatalog", menuName = "Catalogs/View/PlayerViewCatalog")]
    public class PlayerViewCatalog: ScriptableObject
    {
		[field: SerializeField] public PlayerShipView View { get; private set; }

		public PlayerShipView Create(PlayerShipModel model)
		{
			return Instantiate(View);
		}
	}
}