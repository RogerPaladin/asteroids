using System;
using System.Collections.Generic;
using Assets.Scripts.Static.Effects;
using Assets.Scripts.Static.Enemies;
using Assets.Scripts.Static.Helpers;
using Assets.Scripts.Static.Player;
using Assets.Scripts.Static.Weapons;

namespace Assets.Scripts.Static
{
	public class StaticDataParser
	{
		private readonly List<string> _files;

		public StaticDataParser(List<string> files)
		{
			_files = files;
		}

		public StaticDataResponse Parse(Action<float> progress)
		{
			var data = new StaticDataResponse { Files = new StaticDataFiles() };

			var totalFilesCount = _files.Count;

			for (int i = 0; i < _files.Count; i++)
			{
				var index = i;
				var nextFile = _files[index];
				LoadNextFile(nextFile);
				CheckTime(index, totalFilesCount);
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