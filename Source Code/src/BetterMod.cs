using CoI.Mod.Better.Buildings;
using CoI.Mod.Better.Custom;
using CoI.Mod.Better.Edicts;
using CoI.Mod.Better.Extensions;
using CoI.Mod.Better.ModConfigs;
using CoI.Mod.Better.Research;
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

        public static readonly string COI_FOLDER = "Captain of Industry";
        public static readonly string DOCUMENTS_ROOT_DIR_PATH = Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), COI_FOLDER));
        public static readonly string MOD_ROOT_DIR_PATH = Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), COI_FOLDER + "/Mods/CoI.Mod.Better"));

        public static bool gameWasLoaded = false;
        public static int OldConfigVersion = 4;
        public static int CurrentConfigVersion = 5;

        public const int UI_StepSize = 4;
        public static string MyVersion = "0.1.8.4";


        public void Initialize(DependencyResolver resolver, bool gameWasLoaded)
        {
            Debug.Log("BetterMod(V: " + MyVersion + ") mod was created!");
            BetterMod.gameWasLoaded = gameWasLoaded;

            Option<CoreModConfig> result = resolver.GetResolvedInstance<CoreModConfig>();
            Debug.Log("test: result.Value.StartingPopulation: " + result.Value.StartingPopulation);
        }

        public void RegisterPrototypes(ProtoRegistrator registrator)
        {
            Debug.Log("BetterMod Version: " + MyVersion);
            // Registers all products from this assembly. See MyIds.Products.cs for examples.
            registrator.RegisterAllProducts();

            LoadModConfig();
            Debug.Log("BetterMod(V: " + MyVersion + ") Config loaded..");

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

            Debug.Log("BetterMod(V: " + MyVersion + ") RegisterPrototypes..");
        }

        private static void LoadModConfig()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings() { Formatting = Formatting.Indented, MaxDepth = 500, MissingMemberHandling = MissingMemberHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Serialize };

            string oldConfigFile = MOD_ROOT_DIR_PATH + "/globalconfig" + OldConfigVersion + ".json";
            string newConfigFile = MOD_ROOT_DIR_PATH + "/globalconfig" + CurrentConfigVersion + ".json";

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

                    Config.Default.UnlockAllCheatsResearches = oldConfig.UnlockAllCheatsResearches;
                    Config.Default.CheatUpkeepEdicts = oldConfig.CheatUpkeepEdicts;

                    Config.Systems.Cheats = !oldConfig.DisableCheats;
                    Config.Systems.BigStorage = !oldConfig.DisableBigStorage;
                    Config.Systems.Customs = !oldConfig.DisableCustoms;
                    Config.Systems.DieselGeneators = !oldConfig.DisableDieselGeneators;
                    Config.Systems.MineTower = !oldConfig.DisableExtentedMineTowerRange;
                    Config.Systems.GenerellEdicts = !oldConfig.DisableGenerellEdicts;
                    Config.Systems.RefugeesSystem = !oldConfig.DisableNewRefugeesSystem;
                    Config.Systems.PowerGeneators = !oldConfig.DisablePowerGeneators;
                    Config.Systems.VehicleCapIncrease = !oldConfig.DisableVehicleCapIncrease;
                    Config.Systems.VehicleEdicts = !oldConfig.DisableVehicleEdicts;
                    Config.Systems.VoidCrusher = !oldConfig.DisableVoidCrusher;
                    Config.Systems.VoidProducer = !oldConfig.DisableVoidProducer;

                    Config.Beacon.DurationMax = oldConfig.BeaconDurationMax;
                    Config.Beacon.DurationMin = oldConfig.BeaconDurationMin;
                    Config.Beacon.RefugeesMax = oldConfig.BeaconRefugeesMax;
                    Config.Beacon.RefugeesMin = oldConfig.BeaconRefugeesMin;
                    Config.Beacon.RewardBaseValueMultiplier = oldConfig.BeaconRewardBaseValueMultiplier;
                    Config.Beacon.RewardCopperBaseValue = oldConfig.BeaconRewardCopperBaseValue;
                    Config.Beacon.RewardCopperChance = oldConfig.BeaconRewardCopperChance;
                    Config.Beacon.RewardDieselBaseValue = oldConfig.BeaconRewardDieselBaseValue;
                    Config.Beacon.RewardDieselChance = oldConfig.BeaconRewardDieselChance;
                    Config.Beacon.RewardFoodBaseValue = oldConfig.BeaconRewardFoodBaseValue;
                    Config.Beacon.RewardFoodChance = oldConfig.BeaconRewardFoodChance;
                    Config.Beacon.RewardIronBaseValue = oldConfig.BeaconRewardIronBaseValue;
                    Config.Beacon.RewardIronChance = oldConfig.BeaconRewardIronChance;
                    Config.Beacon.RewardOilBaseValue = oldConfig.BeaconRewardOilBaseValue;
                    Config.Beacon.RewardOilChance = oldConfig.BeaconRewardOilChance;
                    Config.Beacon.RewardRubberBaseValue = oldConfig.BeaconRewardRubberBaseValue;
                    Config.Beacon.RewardRubberChance = oldConfig.BeaconRewardRubberChance;

                    Config.Storage.OverrideVanilla = oldConfig.OverrideVanillaStorages;
                    Config.Storage.CapacityT1 = oldConfig.StorageCapacityT1;
                    Config.Storage.TransferLimitT1Count = oldConfig.StorageTransferLimitT1Count;
                    Config.Storage.TransferLimitT1Duration = oldConfig.StorageTransferLimitT1Duration;

                    Config.Storage.CapacityT2 = oldConfig.StorageCapacityT2;
                    Config.Storage.TransferLimitT2Count = oldConfig.StorageTransferLimitT2Count;
                    Config.Storage.TransferLimitT2Duration = oldConfig.StorageTransferLimitT2Duration;

                    Config.Storage.CapacityT3 = oldConfig.StorageCapacityT3;
                    Config.Storage.TransferLimitT3Count = oldConfig.StorageTransferLimitT3Count;
                    Config.Storage.TransferLimitT3Duration = oldConfig.StorageTransferLimitT3Duration;

                    Config.Storage.CapacityT4 = oldConfig.StorageCapacityT4;
                    Config.Storage.TransferLimitT4Count = oldConfig.StorageTransferLimitT4Count;
                    Config.Storage.TransferLimitT4Duration = oldConfig.StorageTransferLimitT4Duration;

                    Config.Storage.FluidCapacityMultiplier = oldConfig.FluidStorageCapacityMultiplier;
                    Config.Storage.NuclearWasteCapacityMultiplier = oldConfig.NuclearWasteStorageCapacityMultiplier;

                    Config.GenerellEdicts.ResearchCostT1 = oldConfig.GenerellEdictsResearchCostT1;
                    Config.GenerellEdicts.ResearchCostT2 = oldConfig.GenerellEdictsResearchCostT2;
                    Config.GenerellEdicts.ResearchCostT3 = oldConfig.GenerellEdictsResearchCostT3;
                    Config.GenerellEdicts.ResearchCostT4 = oldConfig.GenerellEdictsResearchCostT4;
                    Config.GenerellEdicts.ResearchCostT5 = oldConfig.GenerellEdictsResearchCostT5;

                    Config.VehicleEdicts.ResearchCostT1 = oldConfig.VehicleEdictsResearchCostT1;
                    Config.VehicleEdicts.ResearchCostT2 = oldConfig.VehicleEdictsResearchCostT2;
                    Config.VehicleEdicts.ResearchCostT3 = oldConfig.VehicleEdictsResearchCostT3;
                    Config.VehicleEdicts.ResearchCostT4 = oldConfig.VehicleEdictsResearchCostT4;
                    Config.VehicleEdicts.ResearchCostT5 = oldConfig.VehicleEdictsResearchCostT5;

                    Config.Tower.AreaMultiplier = oldConfig.TowerAreaMultiplier;
                    Config.Tower.OverrideVanilla = oldConfig.OverrideBaseGameTower;

                    Config.VoidDestroy.AmountInput = oldConfig.VoidDestroyCheatAmountInput;
                    Config.VoidDestroy.Duration = oldConfig.VoidDestroyCheatDuration;
                    Config.VoidDestroy.Emission = oldConfig.VoidDestroyCheatEmission;
                    Config.VoidDestroy.PowerConsume = oldConfig.VoidDestroyCheatPowerConsume;

                    Config.VoidProducer.AmountInput = oldConfig.VoidProducerCheatAmountInput;
                    Config.VoidProducer.Duration = oldConfig.VoidProducerCheatDuration;
                    Config.VoidProducer.PowerConsume = oldConfig.VoidProducerCheatPowerConsume;

                    Config.VoidDiesel.EnergyInputType = oldConfig.VoidDieselEnergyInputType;
                    Config.VoidDiesel.EnergyT1ProduceInKw = oldConfig.VoidDieselEnergy10CheatInKW;
                    Config.VoidDiesel.EnergyT1BufferCapactiy = oldConfig.VoidDieselEnergy10CheatBufferCapactiy;
                    Config.VoidDiesel.EnergyT2ProduceInKw = oldConfig.VoidDieselEnergy50CheatInKW;
                    Config.VoidDiesel.EnergyT2BufferCapactiy = oldConfig.VoidDieselEnergy50CheatBufferCapactiy;
                    Config.VoidDiesel.EnergyT3ProduceInKw = oldConfig.VoidDieselEnergy100CheatInKW;
                    Config.VoidDiesel.EnergyT3BufferCapactiy = oldConfig.VoidDieselEnergy100CheatBufferCapactiy;
                    Config.VoidDiesel.EnergyT4ProduceInKw = oldConfig.VoidDieselEnergy200CheatInKW;
                    Config.VoidDiesel.EnergyT4BufferCapactiy = oldConfig.VoidDieselEnergy200CheatBufferCapactiy;
                    Config.VoidDiesel.EnergyT5ProduceInKw = oldConfig.VoidDieselEnergy1000CheatInKW;
                    Config.VoidDiesel.EnergyT5BufferCapactiy = oldConfig.VoidDieselEnergy1000CheatBufferCapactiy;

                    Config.VoidPower.EnergyT1InputMechPower = oldConfig.VoidPowerEnergyT1InputMechPower;
                    Config.VoidPower.EnergyT1OutputPower = oldConfig.VoidPowerEnergyT1OutputPower;
                    Config.VoidPower.EnergyT2InputMechPower = oldConfig.VoidPowerEnergyT2InputMechPower;
                    Config.VoidPower.EnergyT2OutputPower = oldConfig.VoidPowerEnergyT2OutputPower;
                    Config.VoidPower.EnergyT3InputMechPower = oldConfig.VoidPowerEnergyT3InputMechPower;
                    Config.VoidPower.EnergyT3OutputPower = oldConfig.VoidPowerEnergyT3OutputPower;
                    Config.VoidPower.EnergyT4InputMechPower = oldConfig.VoidPowerEnergyT4InputMechPower;
                    Config.VoidPower.EnergyT4OutputPower = oldConfig.VoidPowerEnergyT4OutputPower;
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

        public static IEnumerable<PropertyInfo> GetAllProperty(Type type)
        {
            if (type == null)
            {
                return Enumerable.Empty<PropertyInfo>();
            }

            BindingFlags flags = BindingFlags.Public |
                                 BindingFlags.NonPublic |
                                 BindingFlags.Static |
                                 BindingFlags.Instance |
                                 BindingFlags.DeclaredOnly;

            return type.GetProperties(flags).Union(GetAllProperty(type.BaseType));
        }
    }
}
