using CoI.Mod.Better.ModConfigs.Configs;
using CoI.Mod.Better.Utilities;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CoI.Mod.Better.ModConfigs
{


    [Serializable]
    public class ModConfigV2
    {
        public DefaultConfig Default = new DefaultConfig();
        public EnableConfig Systems = new EnableConfig();

        public GameConfig GameSettings = new GameConfig();
        public NewGameConfig StartSettings = new NewGameConfig();

        public UIConfig UI = new UIConfig();

        public BeaconConfig Beacon = new BeaconConfig();
        public TowerConfig Tower = new TowerConfig();
        public StorageConfig Storage = new StorageConfig();

        public VehicleEdictsConfig VehicleEdicts = new VehicleEdictsConfig();
        public GenerellEdictsConfig GenerellEdicts = new GenerellEdictsConfig();

        public VoidDestroyConfig VoidDestroy = new VoidDestroyConfig();
        public VoidProducerConfig VoidProducer = new VoidProducerConfig();

        public VoidDieselConfig VoidDiesel = new VoidDieselConfig();
        public VoidPowerConfig VoidPower = new VoidPowerConfig();

        public ModConfigV2()
        {
        }

        public void Print()
        {
            Debug.Log("BetterMod(V: " + BetterMod.MyVersion + "): read ConfigV2 data");
            foreach (FieldInfo field in ReflectionUtility.GetAllFields(typeof(ModConfigV2)))
            {
                object result = field.GetValue(this);
                if (result is IConfigBase)
                {
                    ((IConfigBase)result).Print(result);
                }
                else
                {
                    Debug.Log(" - " + field.Name + ": " + field.GetValue(this));
                }
            }
        }



        public static ModConfigV2 ConvertConfig_4_to_5(ModConfig oldConfig)
        {
            ModConfigV2 Config = new ModConfigV2();
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

            return Config;
        }
    }
}
