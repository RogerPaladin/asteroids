using System;
using Static.Effects;
using Static.Enemies;
using Static.Helpers;
using Static.Player;
using Static.Weapons;

namespace Static
{
	public class StaticData
	{
		public PlayerConfig PlayerConfig { get; private set; }
		public EnemiesData EnemiesData { get; private set; }
		public WeaponsData WeaponsData { get; private set; }
		public EffectsData EffectsData { get; private set; }

		public void LoadDataFromResources(Action<float> progress)
		{
			var response = new StaticDataParser(StaticDataFileName.ALL).Parse(progress);
			ParseResponse(response);
		}

		private void ParseResponse(StaticDataResponse model)
		{
			PlayerConfig = model.Files.PlayerConfig;
			EnemiesData = new EnemiesData(model.Files.EnemiesJson);
			WeaponsData = new WeaponsData(model.Files.WeaponsJson);
			EffectsData = new EffectsData(model.Files.EffectsJson);
		}
	}
}