using System;
using Model.Background;
using Model.Player;
using UnityEngine;
using Views.GamePlay.Background;
using Views.GamePlay.Player;

namespace Views.Catalogs
{
	[Serializable]
	[CreateAssetMenu(fileName = "BackgroundViewCatalog", menuName = "Catalogs/View/BackgroundViewCatalog")]
    public class BackgroundViewCatalog: ScriptableObject
    {
		[field: SerializeField] public BackgroundView View { get; private set; }

		public BackgroundView Create(BackgroundModel model)
		{
			return Instantiate(View);
		}
	}
}