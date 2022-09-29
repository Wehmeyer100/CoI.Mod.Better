using System.IO;
using System.Reflection;
using System.Text;
using CoI.Mod.Better.Buildings;
using CoI.Mod.Better.Custom;
using CoI.Mod.Better.Edicts;
using CoI.Mod.Better.lang;
using CoI.Mod.Better.ModConfigs;
using CoI.Mod.Better.Research;
using CoI.Mod.Better.Toolbars;
using CoI.Mod.Better.Utilities;
using Mafi;
using Mafi.Base;
using Mafi.Collections;
using Mafi.Core;
using Mafi.Core.Game;
using Mafi.Core.Maintenance;
using Mafi.Core.Map;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Core.Research;
using Mafi.Core.Terrain.Generation;
using Mafi.Unity;
using Newtonsoft.Json;
using UnityEngine;

namespace CoI.Mod.Better
{
	public sealed class BetterMod : IMod
	{
		public string Name => "BetterMod";

		public int Version => 1;

		public bool IsUiOnly => false;

		public static ModConfigV2 Config = new ModConfigV2();

		public static readonly string ModRootDirPath = new FileSystemHelper().GetDirPath(FileType.Mod, false);
		public static readonly string ModDirPath     = Path.Combine(ModRootDirPath, "CoI.Mod.Better");
		public static readonly string CustomsDirPath = Path.Combine(ModDirPath, "Customs");
		public static readonly string LangDirPath    = Path.Combine(ModDirPath, "Lang");

		public const int OldConfigVersion     = 4;
		public const int CurrentConfigVersion = 5;

		public const int    UIStepSize = 4;
		public const string MyVersion  = "0.1.9.5";


		public const string JsonExt = ".json";

		public static readonly GameVersion CurrentGameVersion   = new GameVersion();
		public static readonly GameVersion CompatibilityVersion = new GameVersion("Early Access", "0", "4", "12", "");

		public static bool IsCompatibility => CurrentGameVersion.Equals(CompatibilityVersion, true);

		public void Initialize(DependencyResolver resolver, bool gameWasLoaded)
		{
			Debug.Log("BetterMod(V: " + MyVersion + ") mod was created!");
		}

		public static void SetConfigValue(IConfig config, string fieldName, object value)
		{
			PropertyInfo property = config.GetType().GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance);
			if (property != null)
			{
				property.SetValue(config, value);
			}
		}

		public void ChangeConfigs(Lyst<IConfig> configs)
		{			
			Debug.Log("BetterMod(V: " + MyVersion + ") Config loading..");
			LoadModConfig();
			
			foreach (IConfig config in configs)
			{
				if (config is IStartingFactoryConfig)
				{
					if (Config.StartSettings.OverrideStartSettings)
					{
						//startConfig.InitialTrucks = Config.StartSettings.InitialTrucks;
						SetConfigValue(config, "InitialTrucks", Config.StartSettings.InitialTrucks);

						//startConfig.InitialExcavators = Config.StartSettings.InitialExcavators;
						SetConfigValue(config, "InitialExcavators", Config.StartSettings.InitialExcavators);

						//startConfig.InitialTreeHarvesters = Config.StartSettings.InitialTreeHarvesters;
						SetConfigValue(config, "InitialTreeHarvesters", Config.StartSettings.InitialTreeHarvesters);
						
						
						Debug.LogWarning("BetterMod(V: " + MyVersion + ") >> Override Initial Config");
					}
				}
				
				if (config is CoreModConfig coreConfig)
				{
					if (Config.StartSettings.OverrideStartSettings)
					{
						coreConfig.InitialVehiclesCap = Config.StartSettings.InitialVehiclesCap + 5;
						coreConfig.StartingPopulation = Config.StartSettings.StartingPopulation;
						coreConfig.ShouldUnlockAllProtosOnInit = Config.StartSettings.UnlockAll;
					}
					if (Config.GameSettings.OverrideGameConfig)
					{
						coreConfig.BaseRoundsToEscape = Config.GameSettings.BattleRoundsToEscape;
						coreConfig.IsGodModeEnabled = Config.GameSettings.IsGodMode;
						coreConfig.IsInstaBuildEnabled = Config.GameSettings.IsInstaBuild;
						coreConfig.BaseRoundsToEscape = Config.GameSettings.BattleRoundsToEscape;
						coreConfig.FreeElectricityPerTick = Config.GameSettings.FreeElectricity.Kw();
					}
				}
				if (config is BaseModConfig baseConfig)
				{
					baseConfig.DisableFuelConsumption = Config.GameSettings.DisableFuelConsumption;
				}
				
				
				Debug.Log("BetterMod(V: " + BetterMod.MyVersion + "): " + config.GetType().Name);
				foreach (PropertyInfo field in ReflectionUtility.GetAllProperty(config.GetType()))
				{
					if (field == null)
						continue;

					var fieldValue = field.GetValue(config);
					Debug.Log(" - " + field.Name + ": " + (fieldValue ?? "Null").ToString());
				}
			}
		}

		public void RegisterPrototypes(ProtoRegistrator registrator)
		{
			if (!IsCompatibility)
			{
				Debug.LogWarning("###############################################################");
				Debug.LogWarning("####################### WARNING ###############################");
				Debug.LogWarning("###############################################################");
				Debug.LogWarning("BetterMod(V: " + MyVersion + ") >> This mod is not compatible with the current game version and can cause problems!!!");
				Debug.LogWarning("BetterMod(V: " + MyVersion + ") >> CurrentGameVersion: " + CurrentGameVersion.ToString());
				Debug.LogWarning("BetterMod(V: " + MyVersion + ") >> CompatibilityVersion: " + CompatibilityVersion.ToString());
				Debug.LogWarning("BetterMod(V: " + MyVersion + ") >> Check for updates: https://github.com/Wehmeyer100/CoI.Mod.Better/releases");
				Debug.LogWarning("###############################################################");
			}

			Debug.Log("BetterMod(V: " + MyVersion + ") Directories ..");
			Debug.Log(" - MOD_ROOT_DIR_PATH: " + ModRootDirPath);
			Debug.Log(" - MOD_DIR_PATH: " + ModDirPath);
			Debug.Log(" - CUSTOMS_DIR_PATH: " + CustomsDirPath);
			Debug.Log(" - LANG_DIR_PATH: " + LangDirPath);
			
			// Init LangManager
			LangManager.Instance.Load();

			Debug.Log("BetterMod(V: " + MyVersion + ") RegisterPrototypes..");
			// Use data class registration to register other protos such as machines, recipes, etc.
			registrator.RegisterData<MyToolbars>();
			registrator.RegisterData<MyVehicleCapIncrease>();
			registrator.RegisterData<MineTower>();
			registrator.RegisterData<BigStorages>();
			registrator.RegisterData<Beacon>();
			registrator.RegisterData<GenerelEdicts>();
			registrator.RegisterData<VehicleEdicts>();
			registrator.RegisterData<VoidCrusher>();
			registrator.RegisterData<VoidProducer>();
			registrator.RegisterData<DieselGenerator>();
			registrator.RegisterData<PowerGenerators>();
			registrator.RegisterData<Customs>();
			registrator.RegisterData<SteamStorages>();
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

			string configFile = ModDirPath + "/globalconfig" + CurrentConfigVersion + ".json";

			if (File.Exists(configFile))
			{
				string content = File.ReadAllText(configFile, Encoding.UTF8);
				Config = JsonConvert.DeserializeObject<ModConfigV2>(content, settings);
			}

			File.WriteAllText(configFile, JsonConvert.SerializeObject(Config, settings));
			Config.Print();
		}

		public void RegisterDependencies(DependencyResolverBuilder depBuilder, ProtosDb protosDb, bool gameWasLoaded)
		{

		}
	}
}