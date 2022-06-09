using CoI.Mod.Better.Extensions;
using Mafi;
using Mafi.Base;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Entities.Static;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Factory;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using Mafi.Core.Research;
using Mafi.Localization;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CoI.Mod.Better
{
    internal partial class BigStorages : IModData
    {
        private int capacity_T1 = 0;
        private int capacity_T2 = 0;
        private int capacity_T3 = 0;
        private int capacity_T4 = 0;

        private int capacity_fluid_T1 = 0;
        private int capacity_fluid_T2 = 0;
        private int capacity_fluid_T3 = 0;
        private int capacity_fluid_T4 = 0;

        private int capacity_nuclear = 0;

        public void RegisterData(ProtoRegistrator registrator)
        {
            if (BetterMod.Config.DisableBigStorage) return;

            LoadData();

            UnitStoragesT2(registrator);
            UnitStoragesT1(registrator);
            UnitStoragesT4(registrator);
            UnitStoragesT3(registrator);

            LooseStoragesT2(registrator);
            LooseStoragesT1(registrator);
            LooseStoragesT4(registrator);
            LooseStoragesT3(registrator);

            FluidStoragesT2(registrator);
            FluidStoragesT1(registrator);
            FluidStoragesT4(registrator);
            FluidStoragesT3(registrator);

            NuclearWasteStorage(registrator);

            if (!BetterMod.Config.OverrideVanillaStorages)
            {
                GenerateResearch(registrator);
            }
        }

        private void LoadData()
        {
            capacity_T1 = (int)BetterMod.Config.StorageCapacityT1;
            capacity_T1 = Mathf.Clamp(capacity_T1, 180, int.MaxValue);

            capacity_T2 = (int)BetterMod.Config.StorageCapacityT2;
            capacity_T2 = Mathf.Clamp(capacity_T2, 360, int.MaxValue);

            capacity_T3 = (int)BetterMod.Config.StorageCapacityT3;
            capacity_T3 = Mathf.Clamp(capacity_T3, 2160, int.MaxValue);

            capacity_T4 = (int)BetterMod.Config.StorageCapacityT4;
            capacity_T4 = Mathf.Clamp(capacity_T4, 4320, int.MaxValue);

            float fluidStorageCapacityMultiplier = BetterMod.Config.FluidStorageCapacityMultiplier;

            capacity_fluid_T1 = (int)(capacity_T1 * fluidStorageCapacityMultiplier);
            capacity_fluid_T1 = Mathf.Clamp(capacity_fluid_T1, 1, int.MaxValue);

            capacity_fluid_T2 = (int)(capacity_T2 * fluidStorageCapacityMultiplier);
            capacity_fluid_T2 = Mathf.Clamp(capacity_fluid_T2, 1, int.MaxValue);

            capacity_fluid_T3 = (int)(capacity_T3 * fluidStorageCapacityMultiplier);
            capacity_fluid_T3 = Mathf.Clamp(capacity_fluid_T3, 1, int.MaxValue);

            capacity_fluid_T4 = (int)(capacity_T4 * fluidStorageCapacityMultiplier);
            capacity_fluid_T4 = Mathf.Clamp(capacity_fluid_T4, 1, int.MaxValue);

            float nuclearWasteStorageCapacityMultiplier = BetterMod.Config.NuclearWasteStorageCapacityMultiplier;
            capacity_nuclear = (int)(5000 * nuclearWasteStorageCapacityMultiplier);
            capacity_nuclear = Mathf.Clamp(capacity_nuclear, 5000, int.MaxValue);
        }

        private static bool ProductFilter(ProductProto x)
        {
            return x.IsStorable ? x.Radioactivity == 0 : false;
        }

        private static bool radioactiveProductFilter(ProductProto x)
        {
            return x.IsStorable ? x.Radioactivity > 0 : false;
        }

        private static StorageProtoBuilder.State SetTransferLimitByT(StorageProtoBuilder.State creator, int TLevel)
        {
            if (BetterMod.Config.UnlimitedTransferLimit)
            {
                creator.SetNoTransferLimit();
            }
            else
            {
                var count = BetterMod.Config.StorageTransferLimitT1Count;
                var duration = BetterMod.Config.StorageTransferLimitT1Duration;

                switch (TLevel)
                {
                    case 2:
                        count = BetterMod.Config.StorageTransferLimitT2Count;
                        duration = BetterMod.Config.StorageTransferLimitT2Duration;
                        break;
                    case 3:
                        count = BetterMod.Config.StorageTransferLimitT3Count;
                        duration = BetterMod.Config.StorageTransferLimitT3Duration;
                        break;
                    case 4:
                        count = BetterMod.Config.StorageTransferLimitT4Count;
                        duration = BetterMod.Config.StorageTransferLimitT4Duration;
                        break;
                }

                creator.SetTransferLimit(count, 1.Seconds() / duration);
            }
            return creator;
        }



        private static StorageProtoBuilder.State SetCategory(StorageProtoBuilder.State creator)
        {
            if (BetterMod.Config.OverrideVanillaStorages)
            {
                creator.SetCategories(Ids.ToolbarCategories.Storages);
            }
            else
            {
                creator.SetCategories(MyIDs.ToolbarCategories.Storages);
            }
            return creator;
        }
    }
}