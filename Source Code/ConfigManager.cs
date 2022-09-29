using System.IO;
using System.Reflection;
using System.Text;
using CoI.Mod.Better.ModConfigs;
using CoI.Mod.Better.Utilities;
using Mafi;
using Mafi.Base;
using Mafi.Collections;
using Mafi.Core;
using Mafi.Core.Game;
using Newtonsoft.Json;
using UnityEngine;

namespace CoI.Mod.Better
{
	public class ConfigManager
	{
		
		public static void Load(Lyst<IConfig> configs)
		{			
			MyDebug.Info("Config loading..");
			LoadModConfig();
			
			MyDebug.Info("Mafi Config loading..");
			foreach (IConfig config in configs)
			{
				LoadChanges(config);
				PrintConfig(config);
			}
		}
		
		private static void LoadModConfig()
		{
			JsonSerializerSettings settings = new JsonSerializerSettings()
			{
				Formatting = Formatting.Indented,
				MaxDepth = 500,
				MissingMemberHandling = MissingMemberHandling.Ignore,
				NullValueHandling = NullValueHandling.Ignore,
				ReferenceLoopHandling = ReferenceLoopHandling.Serialize
			};

			string configFile = BetterMod.ModDirPath + "/globalconfig" + BetterMod.CurrentConfigVersion + ".json";

			if (File.Exists(configFile))
			{
				string content = File.ReadAllText(configFile, Encoding.UTF8);
				BetterMod.Config = JsonConvert.DeserializeObject<ModConfigV2>(content, settings);
			}

			File.WriteAllText(configFile, JsonConvert.SerializeObject(BetterMod.Config, settings));
			BetterMod.Config.Print();
		}

		private static void LoadChanges(IConfig config)
		{
			if (config is IStartingFactoryConfig)
			{
				if (BetterMod.Config.StartSettings.OverrideStartSettings)
				{
					//startConfig.InitialTrucks = Config.StartSettings.InitialTrucks;
					SetConfigValue(config, "InitialTrucks", BetterMod.Config.StartSettings.InitialTrucks);

					//startConfig.InitialExcavators = Config.StartSettings.InitialExcavators;
					SetConfigValue(config, "InitialExcavators", BetterMod.Config.StartSettings.InitialExcavators);

					//startConfig.InitialTreeHarvesters = Config.StartSettings.InitialTreeHarvesters;
					SetConfigValue(config, "InitialTreeHarvesters", BetterMod.Config.StartSettings.InitialTreeHarvesters);
				}
			}
				
			if (config is CoreModConfig coreConfig)
			{
				if (BetterMod.Config.StartSettings.OverrideStartSettings)
				{
					coreConfig.InitialVehiclesCap = BetterMod.Config.StartSettings.InitialVehiclesCap + 5;
					coreConfig.StartingPopulation = BetterMod.Config.StartSettings.StartingPopulation;
					coreConfig.ShouldUnlockAllProtosOnInit = BetterMod.Config.StartSettings.UnlockAll;
				}
				if (BetterMod.Config.GameSettings.OverrideGameConfig)
				{
					coreConfig.BaseRoundsToEscape = BetterMod.Config.GameSettings.BattleRoundsToEscape;
					coreConfig.IsGodModeEnabled = BetterMod.Config.GameSettings.IsGodMode;
					coreConfig.IsInstaBuildEnabled = BetterMod.Config.GameSettings.IsInstaBuild;
					coreConfig.BaseRoundsToEscape = BetterMod.Config.GameSettings.BattleRoundsToEscape;
					coreConfig.FreeElectricityPerTick = BetterMod.Config.GameSettings.FreeElectricity.Kw();
				}
			}
			if (config is BaseModConfig baseConfig)
			{
				baseConfig.DisableFuelConsumption = BetterMod.Config.GameSettings.DisableFuelConsumption;
			}
		}

		private static void SetConfigValue(IConfig config, string fieldName, object value)
		{
			PropertyInfo property = config.GetType().GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance);
			if (property != null)
			{
				property.SetValue(config, value);
			}
		}

		private static void PrintConfig(IConfig config)
		{
			MyDebug.Info(config.GetType().Name);
			foreach (PropertyInfo field in ReflectionUtility.GetAllProperty(config.GetType()))
			{
				if (field == null)
					continue;

				var fieldValue = field.GetValue(config);
				Debug.Log(" - " + field.Name + ": " + (fieldValue ?? "Null").ToString());
			}
		}
	}
}