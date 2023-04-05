using System.Collections.Generic;
using Utils;

namespace Static.Helpers
{
	public static class StaticDataFileName
	{
		public const string PLAYER = "player";
		public const string ENEMIES = "enemies";
		public const string WEAPONS = "weapons";
		public const string EFFECTS = "effects";
		
		public static HashSet<string> ALL
		{
			get
			{
				var result = typeof(StaticDataFileName).GetAllPublicStaticFieldsValues<string>();
				return result;
			}
		}
	}
}