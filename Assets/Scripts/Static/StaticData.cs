using System;
using Assets.Scripts.Static.Effects;
using Assets.Scripts.Static.Enemies;
using Assets.Scripts.Static.Helpers;
using Assets.Scripts.Static.Player;
using Assets.Scripts.Static.Weapons;

namespace Assets.Scripts.Static
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