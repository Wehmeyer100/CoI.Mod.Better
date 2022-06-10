using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CoI.Mod.Better
{
    [Serializable]
    public class ModConfig : IModConfig
    {
        public bool DisableBigStorage = false;
        public bool DisableVehicleEdicts = false;
        public bool DisableGenerellEdicts = false;
        public bool DisableNewRefugeesSystem = false;
        public bool DisableExtentedMineTowerRange = false;
        public bool DisableVehicleCapIncrease = false;
        public bool DisableVoidCrusher = false;
        public bool DisableVoidProducer = false;
        public bool DisableCheats = false;
        public bool DisableDieselGeneators = false;
        public bool DisablePowerGeneators = false;
        public bool DisableCustoms = false;

        public int BeaconRefugeesMin = 1;
        public int BeaconRefugeesMax = 20;

        public int BeaconDurationMin = 2;
        public int BeaconDurationMax = 6;

        public float BeaconRewardBaseValueMultiplier = 1f;

        public float BeaconRewardIronBaseValue = 1.5f;
        public float BeaconRewardIronChance = 1f;

        public float BeaconRewardCopperBaseValue = 1f;
        public float BeaconRewardCopperChance = 0.9f;

        public float BeaconRewardRubberBaseValue = 0.75f;
        public float BeaconRewardRubberChance = 0.8f;

        public float BeaconRewardOilBaseValue = 0.5f;
        public float BeaconRewardOilChance = 0.4f;

        public float BeaconRewardDieselBaseValue = 0.25f;
        public float BeaconRewardDieselChance = 0.4f;

        public float BeaconRewardFoodBaseValue = 1f;
        public float BeaconRewardFoodChance = 0.7f;

        public bool OverrideBaseGameTower = true;
        public float TowerAreaMultiplier = 1.5f;

        public bool OverrideVanillaStorages = true;
        public int StorageCapacityT1 = 540; // vanilla * 3
        public int StorageTransferLimitT1Count = 2; // vanilla
        public int StorageTransferLimitT1Duration = 5; // vanilla

        public int StorageCapacityT2 = 1080; // vanilla * 3
        public int StorageTransferLimitT2Count = 4; // vanilla
        public int StorageTransferLimitT2Duration = 5; // vanilla

        public int StorageCapacityT3 = 6480; // vanilla * 3
        public int StorageTransferLimitT3Count = 8; // vanilla
        public int StorageTransferLimitT3Duration = 5; // vanilla

        public int StorageCapacityT4 = 12960; // vanilla * 3
        public int StorageTransferLimitT4Count = 10; // vanilla
        public int StorageTransferLimitT4Duration = 5; // vanilla

        public bool UnlimitedTransferLimit = false;

        public int FluidStorageCapacityMultiplier = 10; // vanilla * 10
        public int NuclearWasteStorageCapacityMultiplier = 5; // = Vanilla * 5

        public float CheatUpkeepEdicts = -2.0f;

        public int VehicleEdictsResearchCostT1 = 2;
        public int VehicleEdictsResearchCostT2 = 4;
        public int VehicleEdictsResearchCostT3 = 6;
        public int VehicleEdictsResearchCostT4 = 8;
        public int VehicleEdictsResearchCostT5 = 10;

        public int GenerellEdictsResearchCostT1 = 2;
        public int GenerellEdictsResearchCostT2 = 4;
        public int GenerellEdictsResearchCostT3 = 6;
        public int GenerellEdictsResearchCostT4 = 8;
        public int GenerellEdictsResearchCostT5 = 10;

        public int VoidDestroyCheatPowerConsume = 75;
        public int VoidDestroyCheatAmountInput = 40;
        public int VoidDestroyCheatDuration = 20;
        public int VoidDestroyCheatEmission = 0;

        public int VoidProducerCheatPowerConsume = 75;
        public int VoidProducerCheatAmountInput = 40;
        public int VoidProducerCheatDuration = 20;

        public int VoidDieselEnergyInputType = 1; // 1 = Diesel, 2 = Water, 3 = Rohöl

        public int VoidDieselEnergy10CheatInKW = 10000;
        public int VoidDieselEnergy50CheatInKW = 50000;
        public int VoidDieselEnergy100CheatInKW = 100000;
        public int VoidDieselEnergy200CheatInKW = 200000;
        public int VoidDieselEnergy1000CheatInKW = 1000000;

        public int VoidDieselEnergy10CheatBufferCapactiy = 540;
        public int VoidDieselEnergy50CheatBufferCapactiy = 540 * 2;
        public int VoidDieselEnergy100CheatBufferCapactiy = 540 * 3;
        public int VoidDieselEnergy200CheatBufferCapactiy = 540 * 4;
        public int VoidDieselEnergy1000CheatBufferCapactiy = 540 * 5;

        public int VoidPowerEnergyT1InputMechPower = 500; // vanilla
        public int VoidPowerEnergyT2InputMechPower = 500; // vanilla
        public int VoidPowerEnergyT3InputMechPower = 500; // vanilla
        public int VoidPowerEnergyT4InputMechPower = 500; // vanilla
        public int VoidPowerEnergyT5InputMechPower = 500; // vanilla

        public int VoidPowerEnergyT1OutputPower = 900;   // vanilla * 3
        public int VoidPowerEnergyT2OutputPower = 1800;  // vanilla * 6
        public int VoidPowerEnergyT3OutputPower = 2700;  // vanilla * 9
        public int VoidPowerEnergyT4OutputPower = 3600;  // vanilla * 12
        public int VoidPowerEnergyT5OutputPower = 4500;   // vanilla * 15
    }
}
