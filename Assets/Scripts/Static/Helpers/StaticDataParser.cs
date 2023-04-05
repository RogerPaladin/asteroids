using System;
using System.Collections.Generic;
using Static.Effects;
using Static.Enemies;
using Static.Helpers;
using Static.Player;
using Static.Weapons;
using UnityEngine.Diagnostics;

namespace Static
{
	public class StaticDataParser
	{
		private readonly HashSet<string> _files;

		public StaticDataParser(HashSet<string> files)
		{
			_files = files;
		}

		public StaticDataResponse Parse(Action<float> progress)
		{
			var data = new StaticDataResponse { Files = new StaticDataFiles() };

			var totalFilesCount = _files.Count;

			var index = 0;
			foreach (var file in _files)
			{
				LoadNextFile(file);
				CheckTime(index, totalFilesCount);
				index++;
			}

			return data;
			
			void LoadNextFile(string file)
			{
				if (file == StaticDataFileName.PLAYER)
					data.Files.PlayerConfig = Utils.Utils.LoadJsonFromResources<PlayerConfig>(file);
				else if (file == StaticDataFileName.ENEMIES)
					data.Files.EnemiesJson = Utils.Utils.LoadJsonFromResources<Dictionary<int, EnemyConfig>>(file);
				else if (file == StaticDataFileName.WEAPONS)
					data.Files.WeaponsJson = Utils.Utils.LoadJsonFromResources<Dictionary<int, WeaponConfig>>(file);
				else if (file == StaticDataFileName.EFFECTS)
					data.Files.EffectsJson = Utils.Utils.LoadJsonFromResources<Dictionary<int, EffectConfig>>(file);
			}

			void CheckTime(int current = 0, int total = 0)
			{
				if (total > 0)
					progress?.Invoke(current / (float) total);
			}
		}
	}
}