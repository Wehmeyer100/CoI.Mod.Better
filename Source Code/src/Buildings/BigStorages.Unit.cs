using Mafi;
using Mafi.Base;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Entities.Static;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Factory;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using Mafi.Localization;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CoI.Mod.Better
{
    internal partial class BigStorages : IModData
    {

        #region Unit Storages Override

        private void UnitStoragesT1(ProtoRegistrator registrator)
        {
            // Set proto ids
            StaticEntityProto.ID protoID = Ids.Buildings.StorageUnit;
            StaticEntityProto.ID protoNextTier = Ids.Buildings.StorageUnitT2;

            if (BetterMod.Config.OverrideVanillaStorages)
            {
                // Remove from Database
                registrator.PrototypesDb.RemoveOrThrow(protoID);
            }
            else
            {
                protoID = MyIDs.Buildings.StorageUnitT1;
                protoNextTier = MyIDs.Buildings.StorageUnitT2;
            }

            // Generate LocStr
            LocStr1 locStr = Loc.Str1("StorageSolidFormattedBase__desc", "Stores up to {0} units of a solid product.", "description for storage");

            // Add new to Database
            var creator = registrator.StorageProtoBuilder.Start("Unit storage", protoID)
                .Description(LocalizationManager.CreateAlreadyLocalizedStr(protoID.Value + "__desc", locStr.Format(capacity_T1.ToString()).Value))
                .SetCost(Costs.Buildings.StorageUnit)
                .SetNextTier(registrator.PrototypesDb.GetOrThrow<StorageProto>(protoNextTier))
                .SetCapacity(capacity_T1)
                .SetProductsFilter(ProductFilter)
                .SetLayout("   [4][4][4][4][4]   ", " # >4A[4][4][4]X4> # ", "   [4][4][4][4][4]   ", " # >4B[4][4][4]Y4> # ", "   [4][4][4][4][4]   ")
                .SetPrefabPath("Assets/Base/Buildings/Storages/UnitT1.prefab");

            if (!BetterMod.Config.OverrideVanillaStorages)
            {
                creator.SetCustomIconPath(registrator.PrototypesDb.GetOrThrow<StorageProto>(Ids.Buildings.StorageUnit).Graphics.IconPath);
            }
            creator = SetCategory(creator);
            SetTransferLimitByT(creator, 1).BuildAndAdd(CountableProductProto.ProductType);
            Debug.Log("BigStorages >> UnitStoragesT1 (override:" + BetterMod.Config.OverrideVanillaStorages + ") >> created!");
        }

        private void UnitStoragesT2(ProtoRegistrator registrator)
        {
            // Set proto ids
            StaticEntityProto.ID protoID = Ids.Buildings.StorageUnitT2;

            if (BetterMod.Config.OverrideVanillaStorages)
            {
                // Remove from Database
                registrator.PrototypesDb.RemoveOrThrow(protoID);
            }
            else
            {
                protoID = MyIDs.Buildings.StorageUnitT2;
            }

            // Generate LocStr
            LocStr1 locStr = Loc.Str1("StorageSolidFormattedBase__desc", "Stores up to {0} units of a solid product.", "description for storage");

            // Add new to Database
            var creator = registrator.StorageProtoBuilder.Start("Unit storage II", protoID)
                .Description(LocalizationManager.CreateAlreadyLocalizedStr(protoID.Value + "__desc", locStr.Format(capacity_T2.ToString()).Value))
                .SetCost(Costs.Buildings.StorageUnitT2)
                .SetCapacity(capacity_T2)
                .SetProductsFilter(ProductFilter)
                .SetLayout("   [5][5][5][5][5]   ", " # >5A[5][5][5]X5> # ", "   [5][5][5][5][5]   ", " # >5B[5][5][5]Y5> # ", "   [5][5][5][5][5]   ")
                .SetPrefabPath("Assets/Base/Buildings/Storages/UnitT2.prefab");
            
            if (!BetterMod.Config.OverrideVanillaStorages)
            {
                creator.SetCustomIconPath(registrator.PrototypesDb.GetOrThrow<StorageProto>(Ids.Buildings.StorageUnitT2).Graphics.IconPath);
            }
            creator = SetCategory(creator);
            SetTransferLimitByT(creator, 2).BuildAndAdd(CountableProductProto.ProductType);
            Debug.Log("BigStorages >> UnitStoragesT2 (override:" + BetterMod.Config.OverrideVanillaStorages + ") >> created!");
        }

        private void UnitStoragesT3(ProtoRegistrator registrator)
        {
            // Set proto ids
            StaticEntityProto.ID protoID = Ids.Buildings.StorageUnitT3;
            StaticEntityProto.ID protoNextTier = Ids.Buildings.StorageUnitT4;

            if (BetterMod.Config.OverrideVanillaStorages)
            {
                // Remove from Database
                registrator.PrototypesDb.RemoveOrThrow(protoID);
            }
            else
            {
                protoID = MyIDs.Buildings.StorageUnitT3;
                protoNextTier = MyIDs.Buildings.StorageUnitT4;
            }

            // Generate LocStr
            LocStr1 locStr = Loc.Str1("StorageSolidFormattedBase__desc", "Stores up to {0} units of a solid product.", "description for storage");

            // Add new to Database
            var creator = registrator.StorageProtoBuilder.Start("Unit storage III", protoID)
                .Description(LocalizationManager.CreateAlreadyLocalizedStr(protoID.Value + "__desc", locStr.Format(capacity_T3.ToString()).Value))
                .SetCost(Costs.Buildings.StorageUnitT3)
                .SetNextTier(registrator.PrototypesDb.GetOrThrow<StorageProto>(protoNextTier))
                .SetCapacity(capacity_T3)
                .SetProductsFilter(ProductFilter)
                .SetLayout("   [6][6][6][6][6][6][6][6][6][6]   ", " # >6A[6][6][6][6][6][6][6][6]X6> # ", "   [6][6][6][6][6][6][6][6][6][6]   ", " # >6B[6][6][6][6][6][6][6][6]Y6> # ", "   [6][6][6][6][6][6][6][6][6][6]   ", "   [6][6][6][6][6][6][6][6][6][6]   ", " # >6C[6][6][6][6][6][6][6][6]Z6> # ", "   [6][6][6][6][6][6][6][6][6][6]   ", " # >6D[6][6][6][6][6][6][6][6]W6> # ", "   [6][6][6][6][6][6][6][6][6][6]   ")
                .SetPrefabPath("Assets/Base/Buildings/Storages/UnitT3.prefab");


            if (!BetterMod.Config.OverrideVanillaStorages)
            {
                creator.SetCustomIconPath(registrator.PrototypesDb.GetOrThrow<StorageProto>(Ids.Buildings.StorageUnitT3).Graphics.IconPath);
            }
            creator = SetCategory(creator);
            SetTransferLimitByT(creator, 3).BuildAndAdd(CountableProductProto.ProductType);
            Debug.Log("BigStorages >> UnitStoragesT3 (override:" + BetterMod.Config.OverrideVanillaStorages + ") >> created!");
        }

        private void UnitStoragesT4(ProtoRegistrator registrator)
        {
            // Set proto ids
            StaticEntityProto.ID protoID = Ids.Buildings.StorageUnitT4;

            if (BetterMod.Config.OverrideVanillaStorages)
            {
                // Remove from Database
                registrator.PrototypesDb.RemoveOrThrow(protoID);
            }
            else
            {
                protoID = MyIDs.Buildings.StorageUnitT4;
            }

            // Generate LocStr
            LocStr1 locStr = Loc.Str1("StorageSolidFormattedBase__desc", "Stores up to {0} units of a solid product.", "description for storage");

            // Add new to Database
            var creator = registrator.StorageProtoBuilder.Start("Unit storage IV", protoID)
                .Description(LocalizationManager.CreateAlreadyLocalizedStr(protoID.Value + "__desc", locStr.Format(capacity_T4.ToString()).Value))
                .SetCost(Costs.Buildings.StorageUnitT4)
                .SetCapacity(capacity_T4)
                .SetProductsFilter(ProductFilter)
                .SetLayout("   [8][8][8][8][8][8][8][8][8][8]   ", " # >8A[8][8][8][8][8][8][8][8]X8> # ", "   [8][8][8][8][8][8][8][8][8][8]   ", " # >8B[8][8][8][8][8][8][8][8]Y8> # ", "   [8][8][8][8][8][8][8][8][8][8]   ", "   [8][8][8][8][8][8][8][8][8][8]   ", " # >8C[8][8][8][8][8][8][8][8]Z8> # ", "   [8][8][8][8][8][8][8][8][8][8]   ", " # >8D[8][8][8][8][8][8][8][8]W8> # ", "   [8][8][8][8][8][8][8][8][8][8]   ")
                .SetPrefabPath("Assets/Base/Buildings/Storages/UnitT4.prefab");

            if (!BetterMod.Config.OverrideVanillaStorages)
            {
                creator.SetCustomIconPath(registrator.PrototypesDb.GetOrThrow<StorageProto>(Ids.Buildings.StorageUnitT4).Graphics.IconPath);
            }
            creator = SetCategory(creator);
            SetTransferLimitByT(creator, 4).BuildAndAdd(CountableProductProto.ProductType);
            Debug.Log("BigStorages >> UnitStoragesT4 (override:" + BetterMod.Config.OverrideVanillaStorages + ") >> created!");
        }

        #endregion


    }
}