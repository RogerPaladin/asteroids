using System.Collections.Generic;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Static.Helpers
{
	public static class StaticDataFileName
	{
		public const string PLAYER = "player";
		public const string ENEMIES = "enemies";
		public const string WEAPONS = "weapons";
		public const string EFFECTS = "effects";
		
		public static List<string> ALL
		{
			get
			{
				var result = typeof(StaticDataFileName).GetAllPublicStaticFieldsValues<string>();
				return result;
			}
		}
	}
}