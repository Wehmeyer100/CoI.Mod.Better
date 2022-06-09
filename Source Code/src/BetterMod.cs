using CoI.Mod.Better.Buildings;
using CoI.Mod.Better.Edicts;
using CoI.Mod.Better.Extensions;
using CoI.Mod.Better.Toolbars;
using Mafi;
using Mafi.Base;
using Mafi.Core;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Core.Research;
using Mafi.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace CoI.Mod.Better
{
    public sealed class BetterMod : IMod
    {

        public string Name => "BetterMod";

        public int Version => 1;

        public bool IsUiOnly => false;

        public static ModConfig Config = new ModConfig();

        private static readonly string DOCUMENTS_ROOT_DIR_PATH = Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Captain of Industry"));

        public static bool gameWasLoaded = false;
        public static int OldConfigVersion = 2;
        public static int CurrentConfigVersion = 3;

        public const int UI_StepSize = 4;
        public static string MyVersion = "0.1.8.2";


        public void Initialize(DependencyResolver resolver, bool gameWasLoaded)
        {
            Log.Info("BetterMod(Version: " + MyVersion + ") mod was init!");
            BetterMod.gameWasLoaded = gameWasLoaded;
        }

        public void RegisterPrototypes(ProtoRegistrator registrator)
        {
            // Registers all products from this assembly. See MyIds.Products.cs for examples.
            registrator.RegisterAllProducts();

            LoadModConfig();
            Log.Info("BetterMod(Version: " + MyVersion + ") Config loaded..");

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

            Log.Info("BetterMod(Version: " + MyVersion + ") RegisterPrototypes..");
        }

        private static void LoadModConfig()
        {
            string modFolder = DOCUMENTS_ROOT_DIR_PATH + "/Mods/CoI.Mod.Better";
            string oldConfigFile = modFolder + "/globalconfig" + OldConfigVersion + ".json";
            string newConfigFile = modFolder + "/globalconfig" + CurrentConfigVersion + ".json";

            if (File.Exists(oldConfigFile) && !File.Exists(newConfigFile))
            {
                Log.Info("BetterMod(Version: " + MyVersion + ") Config converting..");

                Config = new ModConfig();

                string content = File.ReadAllText(oldConfigFile);
                JsonUtility.FromJsonOverwrite(content, Config);

                Log.Info("BetterMod(Version: " + MyVersion + ") Config converted.");

                File.Delete(oldConfigFile);
            }
            else if (File.Exists(newConfigFile))
            {
                string content = File.ReadAllText(newConfigFile);
                JsonUtility.FromJsonOverwrite(content, Config);
            }

            File.WriteAllText(newConfigFile, JsonUtility.ToJson(Config, true));

            Debug.Log("BetterMod(Version: " + MyVersion + "): Config data");
            foreach (FieldInfo field in GetAllFields(typeof(ModConfig)))
            {
                Debug.Log(" - " + field.Name + ": " + field.GetValue(Config));
            }
        }

        public void RegisterDependencies(DependencyResolverBuilder depBuilder, ProtosDb protosDb, bool gameWasLoaded)
        {

        }

        public static string GetIconPath<T>(ProtoRegistrator registrator, Proto.ID id) where T : LayoutEntityProto
        {
            Option<T> proto = registrator.PrototypesDb.Get<T>(id);
            return (proto.Value as LayoutEntityProto).Graphics.IconPath;
        }

        public static IEnumerable<FieldInfo> GetAllFields(Type type)
        {
            if (type == null)
            {
                return Enumerable.Empty<FieldInfo>();
            }

            BindingFlags flags = BindingFlags.Public |
                                 BindingFlags.NonPublic |
                                 BindingFlags.Static |
                                 BindingFlags.Instance |
                                 BindingFlags.DeclaredOnly;

            return type.GetFields(flags).Union(GetAllFields(type.BaseType));
        }
    }
}
