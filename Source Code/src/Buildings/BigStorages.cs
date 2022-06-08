using Mafi;
using Mafi.Base;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Entities.Static;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using Mafi.Localization;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CoI.Mod.Better
{
    internal class BigStorages : IModData
    {
        private int capacity_small = 0;
        private int capacity_large = 0;

        private int capacity_fluid_small = 0;
        private int capacity_fluid_large = 0;

        private int capacity_nuclear = 0;

        public void RegisterData(ProtoRegistrator registrator)
        {
            if (MoreRecipes.Config.DisableBigStorage) return;

            LoadData();

            UnitStoragesT2(registrator);
            UnitStoragesT1(registrator);
            LooseStoragesT2(registrator);
            LooseStoragesT1(registrator);
            FluidStoragesT2(registrator);
            FluidStoragesT1(registrator);
            NuclearWasteStorage(registrator);
        }

        private void LoadData()
        {
            capacity_small = (int)MoreRecipes.Config.StorageCapacitySmall;
            capacity_small = Mathf.Clamp(capacity_small, 180, int.MaxValue);

            capacity_large = (int)MoreRecipes.Config.StorageCapacityLarge;
            capacity_large = Mathf.Clamp(capacity_large, 360, int.MaxValue);

            float fluidStorageCapacityMultiplier = MoreRecipes.Config.FluidStorageCapacityMultiplier;

            capacity_fluid_small = (int)(capacity_small * fluidStorageCapacityMultiplier);
            capacity_fluid_small = Mathf.Clamp(capacity_fluid_small, 1, int.MaxValue);

            capacity_fluid_large = (int)(capacity_large * fluidStorageCapacityMultiplier);
            capacity_fluid_large = Mathf.Clamp(capacity_fluid_large, 1, int.MaxValue);

            float nuclearWasteStorageCapacityMultiplier = MoreRecipes.Config.NuclearWasteStorageCapacityMultiplier;
            capacity_nuclear = (int)(capacity_large * nuclearWasteStorageCapacityMultiplier);
            capacity_nuclear = Mathf.Clamp(capacity_nuclear, 1, int.MaxValue);
        }

        private void NuclearWasteStorage(ProtoRegistrator registrator)
        {
            // Set proto ids
            StaticEntityProto.ID protoID = Ids.Buildings.NuclearWasteStorage;

            // Get vanilla proto data
            NuclearWasteStorageProto vanilla_storage = (NuclearWasteStorageProto)registrator.PrototypesDb.RemoveOrThrow(protoID);

            // Generate new proto
            IEnumerable<KeyValuePair<string, Func<int, LayoutTokenSpec>>> customTokens = new KeyValuePair<string, Func<int, LayoutTokenSpec>>[1] { Make.Kvp<string, Func<int, LayoutTokenSpec>>("-0]", (int i) => new LayoutTokenSpec(-i, 6, LayoutTileConstraint.Any, -i)) };
            EntityLayout layout = registrator.LayoutParser.ParseLayoutOrThrow(new EntityLayoutParams(null, LayoutTileConstraint.None, useNewLayoutSyntax: true, customTokens), "   [4][4][4][4][4][4][4][4][4][4][4][4][4][4][4][4]", "   [4][4][4][4][4][4][4][4][4][4][4][4][4][4][4][4]", "   [4][4][-4[-4[-4[-4[4][4][4][4][4][-3[-3[-3[4][4]", "   [4][4][-4[-4[-4[-4[4][4][4][4][4][4][4][-3[4][4]", "   [4][4][-4[-4[-4[-4[4][4][4][4][4][4][4][-3[4][4]", "   [4][4][-4[-4[-4[-4[4][4][4][4][4][4][4][-3[4][4]", "   [4][4][-4[-4[-4[-4[4][4][4][4][4][4][4][-3[4][4]", "   [4][4][-4[-4[-4[-4[4][4][4][4][4][4][-1[-3[4][4]", "   [4][6][6][6][6][6][6][4][4][4][4][4][-1[-3[4][4]", "   [4][6][6][6][6][6][6][4][4][4][4][4][4][4][4][4]", "   [4][6][6][6][6][6][6][4][4][4][4][4][4][4][4][4]", "A#>[4][6][6][6]-3]-3]-3][-2[-2[-2[4][4][4][4][4][4]", "   [4][6][6][6]-3]-3]-3][-2[-2[-2[4][4][4][4][4][4]", "X#<[4][6][6][6]-3]-3]-3][-2[-2[-2[4][4][4][4][4][4]", "   [4][6][6][6][6][6][6][4][4][4][4][4][4][4][4][4]", "   [4][4][4][4][4][4][4][4][4][4][4][4][4][4][4][4]");

            NuclearWasteStorageProto override_storage = new NuclearWasteStorageProto(
                    id: protoID,
                    Proto.CreateStr(protoID, "Spent fuel storage", "A special underground storage facility that can safely manage any radioactive waste without causing any danger to the island’s population. Leaving a legacy for the next generations to come."),
                    layout,
                    productsFilter: NuclearFilter,
                    capacity: capacity_nuclear.Quantity(),
                    costs: Costs.Buildings.NuclearWasteStorage.MapToEntityCosts(registrator.PrototypesDb),
                    nextTier: Option.None,
                    graphics: new LayoutEntityProto.Gfx("Assets/Base/Buildings/WasteStorage.prefab", default, Option<string>.None, default, hideBlockedPortsIcon: false, null, registrator.GetCategoriesProtos(Ids.ToolbarCategories.Storages)),
                    emissionIntensity: 5,
                    powerConsumed: 30.Kw());

            // Add new to Database
            registrator.PrototypesDb.Add(override_storage, true);
        }

        private void FluidStoragesT1(ProtoRegistrator registrator)
        {
            // Set proto ids
            StaticEntityProto.ID protoID = Ids.Buildings.StorageFluid;
            StaticEntityProto.ID protoT2ID = Ids.Buildings.StorageFluidT2;

            // Remove from Database
            registrator.PrototypesDb.RemoveOrThrow(protoID);

            // Generate LocStr
            LocStr1 locStr = Loc.Str1("StorageSolidFormattedBase__desc", "Stores up to {0} units of a solid product.", "description for storage");

            // Add new to Database
            registrator.StorageProtoBuilder.Start("Fluid storage", protoID)
                .Description(LocalizationManager.CreateAlreadyLocalizedStr(protoID.Value + "__desc", locStr.Format(capacity_fluid_small.ToString()).Value))
                .SetCost(Costs.Buildings.StorageFluid)
                .SetNextTier(registrator.PrototypesDb.GetOrThrow<StorageProto>(protoT2ID))
                .SetCapacity(capacity_fluid_small)
                .SetProductsFilter(FluidFilter)
                .SetLayout("      [5][5][5]      ", " @ >5A[6][6][6]X5> @ ", "   [5][6][6][6][5]   ", " @ >5B[6][6][6]Y5> @ ", "      [5][5][5]      ")
                .SetCategories(Ids.ToolbarCategories.Storages)
                .SetPrefabPath("Assets/Base/Buildings/Storages/Gas.prefab")
                .BuildAndAdd();
        }

        private void FluidStoragesT2(ProtoRegistrator registrator)
        {
            // Set proto ids
            StaticEntityProto.ID protoID = Ids.Buildings.StorageFluidT2;

            // Remove from Database
            registrator.PrototypesDb.RemoveOrThrow(protoID);

            // Generate LocStr
            LocStr1 locStr = Loc.Str1("StorageFluidFormattedBase__desc", "Stores up to {0} units of a liquid or gas product.", "description for storage");

            // Add new to Database
            registrator.StorageProtoBuilder.Start("Fluid storage II", protoID)
                .Description(LocalizationManager.CreateAlreadyLocalizedStr(protoID.Value + "__desc", locStr.Format(capacity_fluid_large.ToString()).Value))
                .SetCost(Costs.Buildings.StorageFluidT2)
                .SetCapacity(capacity_fluid_large)
                .SetProductsFilter(FluidFilter)
                .SetLayout("      [5][5][5]      ", " @ >5A[6][6][6]X5> @ ", "   [5][6][6][6][5]   ", " @ >5B[6][6][6]Y5> @ ", "      [5][5][5]      ")
                .SetCategories(Ids.ToolbarCategories.Storages)
                .SetPrefabPath("Assets/Base/Buildings/Storages/Gas.prefab")
                .BuildAndAdd();
        }

        private void UnitStoragesT1(ProtoRegistrator registrator)
        {
            // Set proto ids
            StaticEntityProto.ID protoID = Ids.Buildings.StorageUnit;
            StaticEntityProto.ID protoT2ID = Ids.Buildings.StorageUnitT2;

            // Remove from Database
            registrator.PrototypesDb.RemoveOrThrow(protoID);

            // Generate LocStr
            LocStr1 locStr = Loc.Str1("StorageSolidFormattedBase__desc", "Stores up to {0} units of a solid product.", "description for storage");

            // Add new to Database
            registrator.StorageProtoBuilder.Start("Unit storage", protoID)
                .Description(LocalizationManager.CreateAlreadyLocalizedStr(protoID.Value + "__desc", locStr.Format(capacity_small.ToString()).Value))
                .SetCost(Costs.Buildings.StorageUnit)
                .SetNextTier(registrator.PrototypesDb.GetOrThrow<StorageProto>(protoT2ID))
                .SetCapacity(capacity_small)
                .SetProductsFilter(ProductFilter)
                .SetLayout("   [3][3][3][3][3]   ", " # >3A[3][3][3]X3> # ", " # >3B[3][3][3]Y3> # ", "   [3][3][3][3][3]   ")
                .SetCategories(Ids.ToolbarCategories.Storages)
                .SetPrefabPath("Assets/Base/Buildings/Storages/Unit.prefab")
                .BuildAndAdd();
        }

        private void UnitStoragesT2(ProtoRegistrator registrator)
        {
            // Set proto ids
            StaticEntityProto.ID protoID = Ids.Buildings.StorageUnitT2;

            // Remove from Database
            registrator.PrototypesDb.RemoveOrThrow(protoID);

            // Generate LocStr
            LocStr1 locStr = Loc.Str1("StorageSolidFormattedBase__desc", "Stores up to {0} units of a solid product.", "description for storage");

            // Add new to Database
            registrator.StorageProtoBuilder.Start("Unit storage II", protoID)
                .Description(LocalizationManager.CreateAlreadyLocalizedStr(protoID.Value + "__desc", locStr.Format(capacity_large.ToString()).Value))
                .SetCost(Costs.Buildings.StorageUnitT2)
                .SetCapacity(capacity_large)
                .SetProductsFilter(ProductFilter)
                .SetLayout("   [3][3][3][3][3]   ", " # >3A[3][3][3]X3> # ", " # >3B[3][3][3]Y3> # ", "   [3][3][3][3][3]   ")
                .SetCategories(Ids.ToolbarCategories.Storages)
                .SetPrefabPath("Assets/Base/Buildings/Storages/Unit.prefab")
                .BuildAndAdd();
        }

        private void LooseStoragesT1(ProtoRegistrator registrator)
        {
            // Set proto ids
            StaticEntityProto.ID protoID = Ids.Buildings.StorageLoose;
            StaticEntityProto.ID protoT2ID = Ids.Buildings.StorageLooseT2;

            // Remove from Database
            registrator.PrototypesDb.RemoveOrThrow(protoID);

            // Generate LocStr
            LocStr1 locStr = Loc.Str1("StorageLooseFormattedBase__desc", "Stores up to {0} units of a loose product.", "description for storage");

            // Add new to Database
            registrator.StorageProtoBuilder.Start("Loose storage", protoID)
                .Description(LocalizationManager.CreateAlreadyLocalizedStr(protoID.Value + "__desc", locStr.Format(capacity_small.ToString()).Value))
                .SetCost(Costs.Buildings.StorageLoose)
                .SetNextTier(registrator.PrototypesDb.GetOrThrow<StorageProto>(protoT2ID))
                .SetCapacity(capacity_small)
                .SetProductsFilter(LooseFilter)
                .SetLayout("      [6][6][6][6]   ", " ~ >7A[7][6][6]X6> ~ ", " ~ >7B[7][6][6]Y6> ~ ", "      [6][6][6][6]   ")
                .SetCategories(Ids.ToolbarCategories.Storages)
                .SetPrefabPath("Assets/Base/Buildings/Storages/Loose.prefab")
                .BuildAndAdd();
        }

        private void LooseStoragesT2(ProtoRegistrator registrator)
        {
            // Set proto ids
            StaticEntityProto.ID protoID = Ids.Buildings.StorageLooseT2;

            // Remove from Database
            registrator.PrototypesDb.RemoveOrThrow(protoID);

            // Generate LocStr
            LocStr1 locStr = Loc.Str1("StorageLooseFormattedBase__desc", "Stores up to {0} units of a loose product.", "description for storage");

            // Add new to Database
            registrator.StorageProtoBuilder.Start("Loose storage II", protoID)
                .Description(LocalizationManager.CreateAlreadyLocalizedStr(protoID.Value + "__desc", locStr.Format(capacity_large.ToString()).Value))
                .SetCost(Costs.Buildings.StorageLooseT2)
                .SetCapacity(capacity_large)
                .SetProductsFilter(LooseFilter)
                .SetLayout("      [6][6][6][6]   ", " ~ >7A[7][6][6]X6> ~ ", " ~ >7B[7][6][6]Y6> ~ ", "      [6][6][6][6]   ")
                .SetCategories(Ids.ToolbarCategories.Storages)
                .SetPrefabPath("Assets/Base/Buildings/Storages/Loose.prefab")
                .BuildAndAdd();
        }

        private static bool ProductFilter(ProductProto x)
        {
            if (x.Type == CountableProductProto.ProductType && x.IsStorable)
            {
                return x.Radioactivity == 0;
            }
            return false;
        }

        private static bool LooseFilter(ProductProto x)
        {
            if (x.Type == LooseProductProto.ProductType)
            {
                return x.IsStorable;
            }
            return false;
        }

        private static bool FluidFilter(ProductProto x)
        {
            if (x.Type == FluidProductProto.ProductType)
            {
                return x.IsStorable;
            }
            return false;
        }

        private static bool NuclearFilter(ProductProto x)
        {
            if (x.Type == CountableProductProto.ProductType && x.IsStorable)
            {
                return x.Radioactivity > 0;
            }
            return false;
        }
    }
}