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
        private void NuclearWasteStorage(ProtoRegistrator registrator)
        {
            // Set proto ids
            StaticEntityProto.ID protoID = Ids.Buildings.NuclearWasteStorage;
            Proto.ID category = Ids.ToolbarCategories.Storages;
            string iconPath = registrator.PrototypesDb.GetOrThrow<StorageProto>(Ids.Buildings.NuclearWasteStorage).Graphics.IconPath;

            if (BetterMod.Config.Storage.OverrideVanilla)
            {
                // Remove from Database
                registrator.PrototypesDb.RemoveOrThrow(protoID);
            }
            else
            {
                protoID = MyIDs.Buildings.NuclearWasteStorage;
                category = MyIDs.ToolbarCategories.Storages;
            }

            // Generate new proto
            CustomLayoutToken[] customTokens = new CustomLayoutToken[2]
            {
                new CustomLayoutToken("-0]", (EntityLayoutParams p, int h) => new LayoutTokenSpec(-h, 4, LayoutTileConstraint.Ground, -h)),
                new CustomLayoutToken("-0|", (EntityLayoutParams p, int h) => new LayoutTokenSpec(-h, 6, LayoutTileConstraint.Ground, -h))
            };
            EntityLayout layout = registrator.LayoutParser.ParseLayoutOrThrow(new EntityLayoutParams(null, useNewLayoutSyntax: true, customTokens), "   [4][4][4][4][4][4][4][4][4][4][4][4][4][4][4][4]", "   [4][4][4][4][4][4][4][4][4]-3]-3]-3]-3]-3]-3][4]", "   [4]-4]-4]-4]-4]-4][4][4][4]-3]-3]-3]-3]-3]-3][4]", "   [4]-4]-4]-4]-4]-4][4][4][4][4][4][4][4]-3]-3][4]", "   [4]-4]-4]-4]-4]-4][4][4][4][4][4][4][4]-3]-3][4]", "   [4]-4]-4]-4]-4]-4][4][4][4][4][4][4][4]-3]-3][4]", "   [4]-4]-4]-4]-4]-4][4][4][4][4][4][4][4]-3]-3][4]", "   [4]-4]-4]-4]-4]-4][4][4][4][4][4]-3]-3]-3]-3][4]", "   [4][6][6][6][6][6][6][4][4][4][4]-3]-3]-3]-3][4]", "   [4][6][6][6][6][6][6][4][4][4][4]-3]-3]-3]-3][4]", "   [4][6][6][6][6][6][6][4][4][4][4][4][4][4][4][4]", "A#>[4][6][6]-3|-3|-3|-3|-3]-2]-2][4][4][4][4][4][4]", "   [4][6][6]-3|-3|-3|-3|-3]-2]-2][4][4][4][4][4][4]", "X#<[4][6][6]-3|-3|-3|-3|-3]-2]-2][4][4][4][4][4][4]", "   [4][6][6][6][6][6][6][4][4][4][4][4][4][4][4][4]", "   [4][4][4][4][4][4][4][4][4][4][4][4][4][4][4][4]");

            NuclearWasteStorageProto override_storage = new NuclearWasteStorageProto(
                    id: protoID,
                    Proto.CreateStr(protoID, "Spent fuel storage", "A special underground storage facility that can safely manage any radioactive waste without causing any danger to the island’s population. Leaving a legacy for the next generations to come."),
                    layout,
                    productsFilter: ProductUtility.RadioactiveProductFilter,
                    productType: CountableProductProto.ProductType,
                    capacity: capacity_nuclear.Quantity(),
                    costs: Costs.Buildings.NuclearWasteStorage.MapToEntityCosts(registrator),
                    nextTier: Option.None,
                    graphics: new LayoutEntityProto.Gfx("Assets/Base/Buildings/WasteStorage.prefab", default(RelTile3f), Option<string>.Some(iconPath), default(ColorRgba), hideBlockedPortsIcon: false, null, registrator.GetCategoriesProtos(category)),
                    emissionIntensity: 5,
                    powerConsumed: 30.Kw());


            // Add new to Database
            registrator.PrototypesDb.Add(override_storage, true);

            Debug.Log("BigStorages >> NuclearWasteStorage (override:" + BetterMod.Config.Storage.OverrideVanilla + ") >> created!");
        }
    }
}