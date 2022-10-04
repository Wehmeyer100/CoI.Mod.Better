using System.IO;
using CoI.Mod.Better.Buildings;
using CoI.Mod.Better.Custom;
using CoI.Mod.Better.Edicts;
using CoI.Mod.Better.ModConfigs;
using CoI.Mod.Better.Research;
using CoI.Mod.Better.Shared;
using CoI.Mod.Better.Shared.Lang;
using CoI.Mod.Better.Toolbars;
using Mafi;
using Mafi.Collections;
using Mafi.Core.Game;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Core.Research;
using UnityEngine;

namespace CoI.Mod.Better
{
	public sealed class BetterMod : IMod
	{
		public static BetterModConfig Config = new BetterModConfig();

		public static readonly string ModName        = "CoI.Mod.Better";
		public static string CustomsDirPath => Path.Combine(Constants.ModDirPath, "Customs");

		public string Name => "BetterMod";

		public int Version => 1;

		public bool IsUiOnly => false;

		public BetterMod()
		{
			ModInfo.Name = ModName;
			ModInfo.Directory = ModName;
			ModInfo.Version = "0.2.0";
			ModInfo.TargetVersion = Constants.TargetSharedGameVersion;
			ModInfo.GithubUrl = "https://github.com/Wehmeyer100/CoI.Mod.Better/releases";
		}

		public void ChangeConfigs(Lyst<IConfig> configs)
		{
			ConfigManager.Load(configs);
		}

		public void Initialize(DependencyResolver resolver, bool gameWasLoaded)
		{
			BetterDebug.Info("Mod was created!");
		}

		public void RegisterPrototypes(ProtoRegistrator registrator)
		{
			BetterShared.PrintInit();
			Debug.Log(" - CUSTOMS_DIR_PATH: " + CustomsDirPath);

			// Init LangManager
			LangManager.Instance.Load();

			BetterDebug.Info("RegisterPrototypes..");
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
			registrator.RegisterData<SteamStorages>();
			registrator.RegisterData<Customs>();

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