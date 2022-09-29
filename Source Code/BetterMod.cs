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
			MyDebug.Info("Mod was created!");
		}
		
		public void ChangeConfigs(Lyst<IConfig> configs)
		{			
			ConfigManager.Load(configs);
		}

		public void RegisterPrototypes(ProtoRegistrator registrator)
		{
			if (!IsCompatibility)
			{
				Debug.LogWarning("###############################################################");
				Debug.LogWarning("####################### WARNING ###############################");
				Debug.LogWarning("###############################################################");
				MyDebug.Warning("This mod is not compatible with the current game version and can cause problems!!!");
				MyDebug.Warning("CurrentGameVersion: " + CurrentGameVersion.ToString());
				MyDebug.Warning("CompatibilityVersion: " + CompatibilityVersion.ToString());
				MyDebug.Warning("Check for updates: https://github.com/Wehmeyer100/CoI.Mod.Better/releases");
				Debug.LogWarning("###############################################################");
			}

			MyDebug.Info("Directories ..");
			Debug.Log(" - MOD_ROOT_DIR_PATH: " + ModRootDirPath);
			Debug.Log(" - MOD_DIR_PATH: " + ModDirPath);
			Debug.Log(" - CUSTOMS_DIR_PATH: " + CustomsDirPath);
			Debug.Log(" - LANG_DIR_PATH: " + LangDirPath);
			
			// Init LangManager
			LangManager.Instance.Load();

			MyDebug.Info("RegisterPrototypes..");
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

			FixResearchWindow(registrator);
		}

		public void RegisterDependencies(DependencyResolverBuilder depBuilder, ProtosDb protosDb, bool gameWasLoaded)
		{

		}

		private void FixResearchWindow(ProtoRegistrator registrator)
		{
			// https://github.com/Wehmeyer100/CoI.Mod.Better/issues/22
			int offsetY = 50;
			foreach (ResearchNodeProto result in registrator.PrototypesDb.All<ResearchNodeProto>()) 
			{
				result.GridPosition += new Vector2i(0, offsetY);
			}
		}
	}
}