using Mafi;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CoI.Mod.Better
{
    [Serializable]
    public class ModConfigV2 : IModConfig
    {
        public List<ModConfigItem> Items = new List<ModConfigItem>();

        public const string DisableBigStorage = "DisableBigStorage";
        public const string DisableVehicleEdicts = "DisableVehicleEdicts";
        public const string DisableNewRefugeesSystem = "DisableNewRefugeesSystem";
        public const string DisableExtentedMineTowerRange = "DisableExtentedMineTowerRange";
        public const string DisableVehicleCapIncrease = "DisableVehicleCapIncrease";
        public const string DisableVoidCrusher = "DisableVoidCrusher";
        public const string DisableCheats = "DisableCheats";
        public const string DisableGenerellEdicts = "DisableGenerellEdicts";

        public const string BeaconRefugeesMin = "BeaconRefugeesMin";
        public const string BeaconRefugeesMax = "BeaconRefugeesMax";

        public const string BeaconDurationMin = "BeaconDurationMin";
        public const string BeaconDurationMax = "BeaconDurationMax";

        public const string BeaconRewardBaseValueMultiplier = "BeaconRewardBaseValueMultiplier";

        public const string BeaconRewardIronBaseValue = "BeaconRewardIronBaseValue";
        public const string BeaconRewardCopperBaseValue = "BeaconRewardCopperBaseValue";
        public const string BeaconRewardRubberBaseValue = "BeaconRewardRubberBaseValue";
        public const string BeaconRewardOilBaseValue = "BeaconRewardOilBaseValue";
        public const string BeaconRewardDieselBaseValue = "BeaconRewardDieselBaseValue";
        public const string BeaconRewardFoodBaseValue = "BeaconRewardFoodBaseValue";

        public const string BeaconRewardIronChance = "BeaconRewardIronChance";
        public const string BeaconRewardCopperChance = "BeaconRewardCopperChance";
        public const string BeaconRewardRubberChance = "BeaconRewardRubberChance";
        public const string BeaconRewardOilChance = "BeaconRewardOilChance";
        public const string BeaconRewardDieselChance = "BeaconRewardDieselChance";
        public const string BeaconRewardFoodChance = "BeaconRewardFoodChance";

        public const string TowerAreaMultiplier = "TowerAreaMultiplier";

        public const string StorageCapacitySmall = "StorageCapacitySmall";
        public const string StorageCapacityLarge = "StorageCapacityLarge";

        public const string FluidStorageCapacityMultiplier = "FluidStorageCapacityMultiplier";
        public const string NuclearWasteStorageCapacityMultiplier = "NuclearWasteStorageCapacityMultiplier";

        public const string CheatUpkeepEdicts = "CheatUpkeepEdicts";

        public const string VehicleEdictsResearchCostT1 = "VehicleEdictsResearchCostT1";
        public const string VehicleEdictsResearchCostT2 = "VehicleEdictsResearchCostT2";
        public const string VehicleEdictsResearchCostT3 = "VehicleEdictsResearchCostT3";
        public const string VehicleEdictsResearchCostT4 = "VehicleEdictsResearchCostT4";
        public const string VehicleEdictsResearchCostT5 = "VehicleEdictsResearchCostT5";

        public const string GenerellEdictsResearchCostT1 = "GenerellEdictsResearchCostT1";
        public const string GenerellEdictsResearchCostT2 = "GenerellEdictsResearchCostT2";
        public const string GenerellEdictsResearchCostT3 = "GenerellEdictsResearchCostT3";
        public const string GenerellEdictsResearchCostT4 = "GenerellEdictsResearchCostT4";
        public const string GenerellEdictsResearchCostT5 = "GenerellEdictsResearchCostT5";

        public const string VoidDestroyCheatPowerConsume = "VoidDestroyCheatPowerConsume";
        public const string VoidDestroyCheatAmountInput = "VoidDestroyCheatAmountInput";
        public const string VoidDestroyCheatDuration = "VoidDestroyCheatDuration";
        public const string VoidDestroyCheatEmission = "VoidDestroyCheatEmission";

        public void LoadDefaultTable()
        {
            this.TryAdd(DisableBigStorage, false);
            this.TryAdd(DisableVehicleEdicts, false);
            this.TryAdd(DisableNewRefugeesSystem, false);
            this.TryAdd(DisableExtentedMineTowerRange, false);
            this.TryAdd(DisableVehicleCapIncrease, false);
            this.TryAdd(DisableVoidCrusher, false);
            this.TryAdd(DisableCheats, false);

            this.TryAdd(BeaconRefugeesMin, 1);
            this.TryAdd(BeaconRefugeesMax, 20);

            this.TryAdd(BeaconDurationMin, 2);
            this.TryAdd(BeaconDurationMax, 6);


            this.TryAdd(BeaconRewardBaseValueMultiplier, 1f);

            this.TryAdd(BeaconRewardIronBaseValue, 1.5f);
            this.TryAdd(BeaconRewardIronChance, 1f);

            this.TryAdd(BeaconRewardCopperBaseValue, 1.0f);
            this.TryAdd(BeaconRewardCopperChance, 0.9f);

            this.TryAdd(BeaconRewardRubberBaseValue, 0.75f);
            this.TryAdd(BeaconRewardRubberChance, 0.8f);

            this.TryAdd(BeaconRewardOilBaseValue, 0.5f);
            this.TryAdd(BeaconRewardOilChance, 0.4f);

            this.TryAdd(BeaconRewardDieselBaseValue, 0.25f);
            this.TryAdd(BeaconRewardDieselChance, 0.4f);

            this.TryAdd(BeaconRewardFoodBaseValue, 1f);
            this.TryAdd(BeaconRewardFoodChance, 0.7f);

            this.TryAdd(TowerAreaMultiplier, 1.5f);

            this.TryAdd(StorageCapacitySmall, 540); // vanilla * 3
            this.TryAdd(StorageCapacityLarge, 1080); // vanilla * 3

            this.TryAdd(FluidStorageCapacityMultiplier, 10);
            this.TryAdd(NuclearWasteStorageCapacityMultiplier, 5);

            this.TryAdd(CheatUpkeepEdicts, -0.5f);

            this.TryAdd(VehicleEdictsResearchCostT1, 9);
            this.TryAdd(VehicleEdictsResearchCostT2, 12);
            this.TryAdd(VehicleEdictsResearchCostT3, 15);
            this.TryAdd(VehicleEdictsResearchCostT4, 18);
            this.TryAdd(VehicleEdictsResearchCostT5, 21);

            this.TryAdd(GenerellEdictsResearchCostT1, 9);
            this.TryAdd(GenerellEdictsResearchCostT2, 12);
            this.TryAdd(GenerellEdictsResearchCostT3, 15);
            this.TryAdd(GenerellEdictsResearchCostT4, 18);
            this.TryAdd(GenerellEdictsResearchCostT5, 21);

            this.TryAdd(VoidDestroyCheatPowerConsume, 75);
            this.TryAdd(VoidDestroyCheatAmountInput, 40);
            this.TryAdd(VoidDestroyCheatDuration, 20);
            this.TryAdd(VoidDestroyCheatEmission, 0);
        }

        public T TryGetOrDefault<T>(string key)
        {
            int indexOf = Items.IndexOf(new ModConfigItem() { Key = key });
            if (indexOf == -1)
            {
                return default;
            }
            object value = Items[indexOf].Value;
            return (T)value;
        }

        public T TryGetOrAddDefault<T>(string key, T defaultValue)
        {
            int indexOf = Items.IndexOf(new ModConfigItem() { Key = key });
            if (indexOf == -1)
            {
                Items.Add(new ModConfigItem() { Key = key, Value = defaultValue });
                return defaultValue;
            }
            object value = Items[indexOf].Value;
            return (T)value;
        }

        public bool TryRemove(string Key)
        {
            return Items.Remove(new ModConfigItem() { Key = Key });
        }

        public bool TryHas(string Key)
        {
            return Items.Contains(new ModConfigItem() { Key = Key });
        }

        public bool TryUpdateOrAdd(string Key, object value)
        {
            int indexOf = Items.IndexOf(new ModConfigItem() { Key = Key });
            if (indexOf == -1)
            {
                Items.Add(new ModConfigItem() { Key = Key, Value = value });
                return true;
            }

            Items[indexOf].Value = value;
            return true;
        }

        public bool TryAdd(string Key, object value)
        {
            int indexOf = Items.IndexOf(new ModConfigItem() { Key = Key });
            if (indexOf == -1)
            {
                Items.Add(new ModConfigItem() { Key = Key, Value = value });
                return true;
            }
            return false;
        }
    }
}
