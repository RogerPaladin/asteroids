using UnityEngine;
using Views.Hud.PlayerInfo;
using Views.Hud.Score;
using Views.Hud.WeaponInfo;

namespace Views.Hud
{
	public class HudView: MonoBehaviour
	{
		public ScoreView ScoreView;
		public PlayerInfoView PlayerInfoView;
		public WeaponInfoView WeaponInfoView;
	}
}