using CoI.Mod.Better.Buildings;
using CoI.Mod.Better.Edicts;
using CoI.Mod.Better.Extensions;
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
using UnityEngine;

namespace CoI.Mod.Better
{
    public sealed class MoreRecipes : IMod
    {

        public string Name => "MoreRecipes";

        public int Version => 1;

        public bool IsUiOnly => false;

        public static ModConfig Config = new ModConfig();

        private static readonly string DOCUMENTS_ROOT_DIR_PATH = Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CaptainOfIndustry"));

        public static bool gameWasLoaded = false;
        public static int OldConfigVersion = 1;
        public static int CurrentConfigVersion = 2;

        public const int UI_StepSize = 4;


        public void Initialize(DependencyResolver resolver, bool gameWasLoaded)
        {
            Log.Info("MoreRecipes mod was created!");
            MoreRecipes.gameWasLoaded = gameWasLoaded;
        }

        public void RegisterPrototypes(ProtoRegistrator registrator)
        {
            // Registers all products from this assembly. See MyIds.Products.cs for examples.
            registrator.RegisterAllProducts();

            LoadModConfig();

            // Use data class registration to register other protos such as machines, recipes, etc.
            registrator.RegisterData<MineTower>();
            registrator.RegisterData<BigStorages>();
            registrator.RegisterData<Beacon>();
            registrator.RegisterData<MyVehicleCapIncrease>();
            registrator.RegisterData<GenerellEdicts>();
            registrator.RegisterData<VehicleEdicts>();
            registrator.RegisterData<VoidCrusher>();
            registrator.RegisterData<VoidProducer>();
            registrator.RegisterData<DieselGeneators>();

            Log.Info("MoreRecipes RegisterPrototypes..");
        }

        private static void LoadModConfig()
        {
            string modFolder = DOCUMENTS_ROOT_DIR_PATH + "/Mods/CoI.Mod.Better";
            string oldConfigFile = modFolder + "/globalconfig" + OldConfigVersion + ".v" + OldConfigVersion + ".json";
            string newConfigFile = modFolder + "/globalconfig" + CurrentConfigVersion + ".json";

            if (File.Exists(oldConfigFile) && !File.Exists(newConfigFile))
            {
                Config = new ModConfig();

                string content = File.ReadAllText(oldConfigFile);
                JsonUtility.FromJsonOverwrite(content, Config);

                File.Delete(oldConfigFile);
            }
            else if(File.Exists(newConfigFile))
            {
                string content = File.ReadAllText(newConfigFile);
                JsonUtility.FromJsonOverwrite(content, Config);
            }
            File.WriteAllText(newConfigFile, JsonUtility.ToJson(Config, true));
        }

        public void RegisterDependencies(DependencyResolverBuilder depBuilder, ProtosDb protosDb, bool gameWasLoaded)
        {

        }

        public static string GetIconPath<T>(ProtoRegistrator registrator, Proto.ID id) where T : LayoutEntityProto
        {
            Option<T> proto = registrator.PrototypesDb.Get<T>(id);
            return (proto.Value as LayoutEntityProto).Graphics.IconPath;
        }

        public static EntityLayout GenerateLayout(ProtosDb protosDb, EntityLayoutParams layoutParams, params string[] layout)
        {
            EntityLayout result;
            try
            {
                result = new EntityLayoutParser(protosDb).ParseLayoutOrThrow(layoutParams, layout);
            }
            catch (InvalidEntityLayoutException inner)
            {
                throw new ProtoBuilderException($"Invalid layout of entity", inner);
            }

            return result;
        }
    }
}
