using CoI.Mod.Better.Utilities;
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

namespace CoI.Mod.Better.Buildings
{
    internal partial class BigStorages : IModData
    {

        #region Loose Storages Override
        private void LooseStoragesT1(ProtoRegistrator registrator)
        {            
            // Set proto ids
            StaticEntityProto.ID protoID = Ids.Buildings.StorageLoose;
            StaticEntityProto.ID protoNextTier = Ids.Buildings.StorageLooseT2;

            if (BetterMod.Config.Storage.OverrideVanilla)
            {
                // Remove from Database
                registrator.PrototypesDb.RemoveOrThrow(protoID);
            }
            else
            {
                protoID = MyIDs.Buildings.StorageLooseT1;
                protoNextTier = MyIDs.Buildings.StorageLooseT2;
            }

            // Generate LocStr
            LocStr1 locStr = Loc.Str1("StorageLoose1FormattedBase__desc", "Stores up to {0} units of a loose product.", "description for storage");

            // Add new to Database
            var creator = registrator.StorageProtoBuilder.Start("Loose storage", protoID)
                .Description(LocalizationManager.CreateAlreadyLocalizedStr(protoID.Value + "__desc", locStr.Format(capacity_T1.ToString()).Value))
                .SetCost(Costs.Buildings.StorageLoose)
                .SetNextTier(registrator.PrototypesDb.GetOrThrow<StorageProto>(protoNextTier))
                .SetCapacity(capacity_T1)
                .SetProductsFilter(ProductUtility.ProductFilter)
                .SetLayout("   [5][5][5][5][5]   ", " ~ >5A[5][5][5]X5> ~ ", "   [5][5][5][5][5]   ", " ~ >5B[5][5][5]Y5> ~ ", "   [5][5][5][5][5]   ")
                .SetPrefabPath("Assets/Base/Buildings/Storages/LooseT1.prefab")
                .SetPileGfxParams("Pile_Soft", "Pile_Soft", new LoosePileTextureParams(1.4f));

            if (!BetterMod.Config.Storage.OverrideVanilla)
            {
                creator.SetCustomIconPath(registrator.PrototypesDb.GetOrThrow<StorageProto>(Ids.Buildings.StorageLoose).Graphics.IconPath);
            }
            creator = SetCategory(creator);
            ProductUtility.SetTransferLimitByT(creator, 1).BuildAsLooseAndAdd();

            Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> BigStorages >> LooseStoragesT1 (override:" + BetterMod.Config.Storage.OverrideVanilla + ") >> created!");
        }

        private void LooseStoragesT2(ProtoRegistrator registrator)
        {            
            // Set proto ids
            StaticEntityProto.ID protoID = Ids.Buildings.StorageLooseT2;

            if (BetterMod.Config.Storage.OverrideVanilla)
            {
                // Remove from Database
                registrator.PrototypesDb.RemoveOrThrow(protoID);
            }
            else
            {
                protoID = MyIDs.Buildings.StorageLooseT2;
            }

            // Generate LocStr
            LocStr1 locStr = Loc.Str1("StorageLoose2FormattedBase__desc", "Stores up to {0} units of a loose product.", "description for storage");

            // Add new to Database
            var creator = registrator.StorageProtoBuilder.Start("Loose storage II", protoID)
                .Description(LocalizationManager.CreateAlreadyLocalizedStr(protoID.Value + "__desc", locStr.Format(capacity_T2.ToString()).Value))
                .SetCost(Costs.Buildings.StorageLooseT2)
                .SetCapacity(capacity_T2)
                .SetProductsFilter(ProductUtility.ProductFilter)
                .SetLayout("   [6][6][6][6][6]   ", " ~ >6A[6][6][6]X6> ~ ", "   [6][6][6][6][6]   ", " ~ >6B[6][6][6]Y6> ~ ", "   [6][6][6][6][6]   ")
                .SetPrefabPath("Assets/Base/Buildings/Storages/LooseT2.prefab")
                .SetPileGfxParams("Pile_Soft", "Pile_Soft", new LoosePileTextureParams(0.3f));

            if (!BetterMod.Config.Storage.OverrideVanilla)
            {
                creator.SetCustomIconPath(registrator.PrototypesDb.GetOrThrow<StorageProto>(Ids.Buildings.StorageLooseT2).Graphics.IconPath);
            }
            creator = SetCategory(creator);
            ProductUtility.SetTransferLimitByT(creator, 2).BuildAsLooseAndAdd();
            Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> BigStorages >> LooseStoragesT2 (override:" + BetterMod.Config.Storage.OverrideVanilla + ") >> created!");
        }

        private void LooseStoragesT3(ProtoRegistrator registrator)
        {
            // Set proto ids
            StaticEntityProto.ID protoID = Ids.Buildings.StorageLooseT3;
            StaticEntityProto.ID protoNextTier = Ids.Buildings.StorageLooseT4;

            if (BetterMod.Config.Storage.OverrideVanilla)
            {
                // Remove from Database
                registrator.PrototypesDb.RemoveOrThrow(protoID);
            }
            else
            {
                protoID = MyIDs.Buildings.StorageLooseT3;
                protoNextTier = MyIDs.Buildings.StorageLooseT4;
            }

            // Generate LocStr
            LocStr1 locStr = Loc.Str1("StorageLoose3FormattedBase__desc", "Stores up to {0} units of a loose product.", "description for storage");

            // Add new to Database
            var creator = registrator.StorageProtoBuilder.Start("Loose storage III", protoID)
                .Description(LocalizationManager.CreateAlreadyLocalizedStr(protoID.Value + "__desc", locStr.Format(capacity_T3.ToString()).Value))
                .SetCost(Costs.Buildings.StorageLooseT3)
                .SetNextTier(registrator.PrototypesDb.GetOrThrow<StorageProto>(protoNextTier))
                .SetCapacity(capacity_T3)
                .SetProductsFilter(ProductUtility.ProductFilter)
                .SetLayout("      [6][6][6][6][6][6][6][6]      ", " ~ >6A[6][6][6][6][6][6][6][6]X6> ~ ", "   [6][6][6][6][6][6][6][6][6][6]   ", " ~ >6B[6][6][6][6][6][6][6][6]Y6> ~ ", "   [7][7][7][7][6][6][6][6][6][6]   ", "   [7][7][7][7][6][6][6][6][6][6]   ", " ~ >6C[6][6][6][6][6][6][6][6]Z6> ~ ", "   [6][6][6][6][6][6][6][6][6][6]   ", " ~ >6D[6][6][6][6][6][6][6][6]W6> ~ ", "      [6][6][6][6][6][6][6][6]      ")
                .SetPrefabPath("Assets/Base/Buildings/Storages/LooseT3.prefab")
                .SetPileGfxParams("Pile_Soft", "Pile_Soft", new LoosePileTextureParams(0.2f));

            if (!BetterMod.Config.Storage.OverrideVanilla)
            {
                creator.SetCustomIconPath(registrator.PrototypesDb.GetOrThrow<StorageProto>(Ids.Buildings.StorageLooseT3).Graphics.IconPath);
            }
            creator = SetCategory(creator);
            ProductUtility.SetTransferLimitByT(creator, 3).BuildAsLooseAndAdd();
            Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> BigStorages >> LooseStoragesT3 (override:" + BetterMod.Config.Storage.OverrideVanilla + ") >> created!");
        }

        private void LooseStoragesT4(ProtoRegistrator registrator)
        {            
            // Set proto ids
            StaticEntityProto.ID protoID = Ids.Buildings.StorageLooseT4;

            if (BetterMod.Config.Storage.OverrideVanilla)
            {
                // Remove from Database
                registrator.PrototypesDb.RemoveOrThrow(protoID);
            }
            else
            {
                protoID = MyIDs.Buildings.StorageLooseT4;
            }

            // Generate LocStr
            LocStr1 locStr = Loc.Str1("StorageLoose4FormattedBase__desc", "Stores up to {0} units of a loose product.", "description for storage");

            // Add new to Database
            var creator = registrator.StorageProtoBuilder.Start("Loose storage IV", protoID)
                .Description(LocalizationManager.CreateAlreadyLocalizedStr(protoID.Value + "__desc", locStr.Format(capacity_T4.ToString()).Value))
                .SetCost(Costs.Buildings.StorageLooseT4)
                .SetCapacity(capacity_T4)
                .SetProductsFilter(ProductUtility.ProductFilter)
                .SetLayout(new EntityLayoutParams(null, useNewLayoutSyntax: false, new CustomLayoutToken[1]
                {
                    new CustomLayoutToken("[0!", delegate(EntityLayoutParams p, int h)
                    {
                        int heightToExcl = h + 1;
                        int? terrainSurfaceHeight = 0;
                        Proto.ID? surfaceId = p.HardenedFloorSurfaceId;
                        return new LayoutTokenSpec(0, heightToExcl, LayoutTileConstraint.None, terrainSurfaceHeight, null, null, null, null, surfaceId);
                    })
                }), "      [9][9][9][9][9][9][9][9]      ", " ~ >9A[9][9][9][9][9][9][9][9]X9> ~ ", "   [9][9][9][9][9][9][9][9][9][9]   ", " ~ >9B[9][9][9][9][9][9][9][9]Y9> ~ ", "   [9![9![9![9![9][9][9][9][9][9]   ", "   [9![9![9![9![9][9][9][9][9][9]   ", " ~ >9C[9][9][9][9][9][9][9][9]Z9> ~ ", "   [9][9][9][9][9][9][9][9][9][9]   ", " ~ >9D[9][9][9][9][9][9][9][9]W9> ~ ", "      [9][9][9][9][9][9][9][9]      ")
                .SetPrefabPath("Assets/Base/Buildings/Storages/LooseT4.prefab")
                .SetPileGfxParams("Pile_Soft", "Pile_Soft", new LoosePileTextureParams(0.2f));

            if (!BetterMod.Config.Storage.OverrideVanilla)
            {
                creator.SetCustomIconPath(registrator.PrototypesDb.GetOrThrow<StorageProto>(Ids.Buildings.StorageLooseT4).Graphics.IconPath);
            }
            creator = SetCategory(creator);
            ProductUtility.SetTransferLimitByT(creator, 4).BuildAsLooseAndAdd();
            Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> BigStorages >> LooseStoragesT4 (override:" + BetterMod.Config.Storage.OverrideVanilla + ") >> created!");
        }

        #endregion

    }
}