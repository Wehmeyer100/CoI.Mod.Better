﻿using CoI.Mod.Better.Buildings;
using CoI.Mod.Better.Custom;
using CoI.Mod.Better.Edicts;
using CoI.Mod.Better.Extensions;
using CoI.Mod.Better.ModConfigs;
using CoI.Mod.Better.Research;
using CoI.Mod.Better.Toolbars;
using CoI.Mod.Better.Utilities;
using Mafi;
using Mafi.Base;
using Mafi.Core;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Game;
using Mafi.Core.Maintenance;
using Mafi.Core.Map;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Core.Research;
using Mafi.Core.Terrain.Generation;
using Mafi.Localization;
using Mafi.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace CoI.Mod.Better
{

    public sealed class BetterMod : IMod
    {
        public string Name => "BetterMod";

        public int Version => 1;

        public bool IsUiOnly => false;

        public static ModConfigV2 Config = new ModConfigV2();

        public static readonly string MOD_ROOT_DIR_PATH = new FileSystemHelper().GetDirPath(FileType.Mod, false);
        public static readonly string MOD_DIR_PATH = Path.Combine(MOD_ROOT_DIR_PATH, "CoI.Mod.Better");
        public static readonly string PLUGIN_DIR_PATH = Path.Combine(MOD_DIR_PATH, "Plugins");
        public static readonly string CUSTOMS_DIR_PATH = Path.Combine(MOD_DIR_PATH, "Customs");

        public const int OldConfigVersion = 4;
        public const int CurrentConfigVersion = 5;

        public const int UI_StepSize = 4;
        public const string MyVersion = "0.1.8.7";

        public static readonly GameVersion CurrentGameVersion = new GameVersion();
        public static readonly GameVersion CompatibilityVersion = new GameVersion("Early Access", "0", "4", "5", "b");
        public static bool IsCompatibility => CurrentGameVersion.Equals(CompatibilityVersion, true);

        public BetterMod()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string assemblyPath = Path.Combine(PLUGIN_DIR_PATH, new AssemblyName(args.Name).Name + ".dll");
            if (!File.Exists(assemblyPath))
            {
                Debug.Log("BetterMod(V: " + MyVersion + ") Assembly cannot loaded from Plugins, Assembly not found >> " + assemblyPath);
                return null;
            }

            try
            {
                Assembly assembly = Assembly.LoadFrom(assemblyPath);
                Debug.Log("BetterMod(V: " + MyVersion + ") Assembly loaded from Plugins >> " + assembly.FullName + " >> " + assemblyPath);
                return assembly;
            }
            catch (Exception e)
            {
                Debug.Log("BetterMod(V: " + MyVersion + ") Assembly cannot loaded from Plugins, by exception >> " + assemblyPath);
                Debug.LogException(e);
            }
            return null;
        }


        public void Initialize(DependencyResolver resolver, bool gameWasLoaded)
        {
            Debug.Log("BetterMod(V: " + MyVersion + ") mod was created!");

            ReflectionUtility.PrintAllProperties<CoreModConfig>(resolver);
            ReflectionUtility.PrintAllProperties<GameDifficultyConfig>(resolver);
            ReflectionUtility.PrintAllProperties<MaintenanceConfig>(resolver);
            ReflectionUtility.PrintAllProperties<IslandMapDifficultyConfig>(resolver);
            ReflectionUtility.PrintAllProperties<StaticIslandMapProviderConfig>(resolver);
            ReflectionUtility.PrintAllProperties<RandomSeedConfig>(resolver);
            ReflectionUtility.PrintAllProperties<TerrainGeneratorConfig>(resolver);
            ReflectionUtility.PrintAllProperties<IslandMapGeneratorConfig>(resolver);
            ReflectionUtility.PrintAllProperties<BaseModConfig>(resolver);
            ReflectionUtility.PrintAllProperties<StartingFactoryConfig>(resolver);
            ReflectionUtility.PrintAllProperties<UnityModConfig>(resolver);
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
            Debug.Log(" - MOD_ROOT_DIR_PATH: " + MOD_ROOT_DIR_PATH);
            Debug.Log(" - MOD_DIR_PATH: " + MOD_DIR_PATH);
            Debug.Log(" - PLUGIN_DIR_PATH: " + PLUGIN_DIR_PATH);
            Debug.Log(" - CUSTOMS_DIR_PATH: " + CUSTOMS_DIR_PATH);

            // Registers all products from this assembly. See MyIds.Products.cs for examples.
            registrator.RegisterAllProducts();

            Debug.Log("BetterMod(V: " + MyVersion + ") Config loading..");
            LoadModConfig();

            Debug.Log("BetterMod(V: " + MyVersion + ") RegisterPrototypes..");
            // Use data class registration to register other protos such as machines, recipes, etc.
            registrator.RegisterData<MyToolbars>();
            registrator.RegisterData<MyVehicleCapIncrease>();
            registrator.RegisterData<MineTower>();
            registrator.RegisterData<BigStorages>();
            registrator.RegisterData<Beacon>();
            registrator.RegisterData<GenerellEdicts>();
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
            JsonSerializerSettings settings = new JsonSerializerSettings() { Formatting = Formatting.Indented, MaxDepth = 500, MissingMemberHandling = MissingMemberHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Serialize };

            string oldConfigFile = MOD_DIR_PATH + "/globalconfig" + OldConfigVersion + ".json";
            string newConfigFile = MOD_DIR_PATH + "/globalconfig" + CurrentConfigVersion + ".json";

            if (File.Exists(oldConfigFile) && !File.Exists(newConfigFile))
            {
                if (OldConfigVersion == 4)
                {
                    Debug.Log("BetterMod(V: " + MyVersion + ") Config converting..");

                    Config = new ModConfigV2();
                    ModConfig oldConfig = new ModConfig();

                    string content = File.ReadAllText(oldConfigFile);
                    JsonUtility.FromJsonOverwrite(content, oldConfig);

                    File.Delete(oldConfigFile);
                    Config = ModConfigV2.ConvertConfig_4_to_5(oldConfig);
                    Debug.Log("BetterMod(V: " + MyVersion + ") Config converted.");
                }
                else
                {
                    string content = File.ReadAllText(newConfigFile, Encoding.UTF8);
                    Config = JsonConvert.DeserializeObject<ModConfigV2>(content, settings);
                }
            }
            else if (File.Exists(newConfigFile))
            {
                string content = File.ReadAllText(newConfigFile, Encoding.UTF8);
                Config = JsonConvert.DeserializeObject<ModConfigV2>(content, settings);
            }

            File.WriteAllText(newConfigFile, JsonConvert.SerializeObject(Config, settings));
            Config.Print();
        }

        public void RegisterDependencies(DependencyResolverBuilder depBuilder, ProtosDb protosDb, bool gameWasLoaded)
        {

        }
    }
}