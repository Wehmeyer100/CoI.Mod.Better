using System.Reflection;
using CoI.Mod.Better.ModConfigs;
using CoI.Mod.Better.Shared;
using CoI.Mod.Better.Shared.Utilities;
using Mafi;
using Mafi.Base;
using Mafi.Collections;
using Mafi.Core;
using Mafi.Core.Game;
using UnityEngine;

namespace CoI.Mod.Better
{
	public class ConfigManager
	{
		public static void Load(Lyst<IConfig> configs)
		{
			BetterDebug.Info("Config loading..");
			LoadModConfig();

			BetterDebug.Info("Mafi config loading..");
			foreach (IConfig config in configs)
			{
				LoadChanges(config);
				PrintConfig(config);
			}
		}

		private static void LoadModConfig()
		{
			BetterMod.Config = Shared.Config.ConfigManager.LoadOrCreate<BetterModConfig>("globalconfig5.json", true);
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
			BetterDebug.Info(config.GetType().Name);
			foreach (PropertyInfo field in ReflectionUtility.GetAllProperty(config.GetType()))
			{
				if (field == null)
					continue;

				object fieldValue = field.GetValue(config);
				Debug.Log(" - " + field.Name + ": " + (fieldValue ?? "Null"));
			}
		}
	}
}