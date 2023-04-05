using Utils.DiContainers;
using Views.Hud.PlayerInfo;
using Views.Hud.Score;
using Views.Hud.WeaponInfo;
using UnityEngine;

namespace Views.Hud
{
	public class HudView: MonoBehaviour, IDiContainerChild
	{
		public ScoreView ScoreView;
		public PlayerInfoView PlayerInfoView;
		public WeaponInfoView WeaponInfoView;
	}
}