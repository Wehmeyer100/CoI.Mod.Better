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

        public float TowerAreaMultiplier = 1.5f;

        public int StorageCapacitySmall = 540; // vanilla * 3
        public int StorageCapacityLarge = 1080; // vanilla * 3

        public int FluidStorageCapacityMultiplier = 10; // vanilla * 10
        public int NuclearWasteStorageCapacityMultiplier = 5; // = Vanilla

        public float CheatUpkeepEdicts = -2.0f;

        public int VehicleEdictsResearchCostT1 = 9;
        public int VehicleEdictsResearchCostT2 = 12;
        public int VehicleEdictsResearchCostT3 = 15;
        public int VehicleEdictsResearchCostT4 = 18;
        public int VehicleEdictsResearchCostT5 = 21;

        public int GenerellEdictsResearchCostT1 = 9;
        public int GenerellEdictsResearchCostT2 = 12;
        public int GenerellEdictsResearchCostT3 = 15;
        public int GenerellEdictsResearchCostT4 = 18;
        public int GenerellEdictsResearchCostT5 = 21;

        public int VoidDestroyCheatPowerConsume = 75;
        public int VoidDestroyCheatAmountInput = 40;
        public int VoidDestroyCheatDuration = 20;
        public int VoidDestroyCheatEmission = 0;

        public int VoidProducerCheatPowerConsume = 75;
        public int VoidProducerCheatAmountInput = 40;
        public int VoidProducerCheatDuration = 20;

        public int VoidProducerEnergyInputType = 1; // 1 = Diesel, 2 = Water, 3 = Rohöl

        public int VoidProducerEnergy10CheatInKW = 10000;
        public int VoidProducerEnergy50CheatInKW = 50000;
        public int VoidProducerEnergy100CheatInKW = 100000;
        public int VoidProducerEnergy200CheatInKW = 200000;
        public int VoidProducerEnergy1000CheatInKW = 1000000;

        public int VoidProducerEnergy10CheatBufferCapactiy = 540;
        public int VoidProducerEnergy50CheatBufferCapactiy = 540 * 2;
        public int VoidProducerEnergy100CheatBufferCapactiy = 540 * 3;
        public int VoidProducerEnergy200CheatBufferCapactiy = 540 * 4;
        public int VoidProducerEnergy1000CheatBufferCapactiy = 540 * 5;
    }
}
