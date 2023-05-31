using System;
using System.Collections.Generic;
using CoI.Mod.Better.MyIDs;
using CoI.Mod.Better.Shared;
using CoI.Mod.Better.Shared.Utilities;
using Mafi;
using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Entities.Static;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;

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
				category = ToolbarCategories.Storages;
			}

			// Generate new proto
			CustomLayoutToken[] customTokens = new CustomLayoutToken[2]
			{
				new CustomLayoutToken("-0]",  ((p, h) => new LayoutTokenSpec(-h, 4, LayoutTileConstraint.Ground, new int?(-h)))),
				new CustomLayoutToken("-0|",  ((p, h) => new LayoutTokenSpec(-h, 6, LayoutTileConstraint.Ground, new int?(-h))))
            };
			EntityLayout layout = registrator.LayoutParser.ParseLayoutOrThrow(
				new EntityLayoutParams(customTokens: customTokens), 
				"   [4][4][4][4][4][4][4][4][4][4][4][4][4][4][4][4]", "   [4][4][4][4][4][4][4][4][4]-3]-3]-3]-3]-3]-3][4]", "   [4]-4]-4]-4]-4]-4][4][4][4]-3]-3]-3]-3]-3]-3][4]", "   [4]-4]-4]-4]-4]-4][4][4][4][4][4][4][4]-3]-3][4]", "   [4]-4]-4]-4]-4]-4][4][4][4][4][4][4][4]-3]-3][4]", "   [4]-4]-4]-4]-4]-4][4][4][4][4][4][4][4]-3]-3][4]", "   [4]-4]-4]-4]-4]-4][4][4][4][4][4][4][4]-3]-3][4]", "   [4]-4]-4]-4]-4]-4][4][4][4][4][4]-3]-3]-3]-3][4]", "   [4][6][6][6][6][6][6][4][4][4][4]-3]-3]-3]-3][4]", "   [4][6][6][6][6][6][6][4][4][4][4]-3]-3]-3]-3][4]", "   [4][6][6][6][6][6][6][4][4][4][4][4][4][4][4][4]", "A#>[4][6][6]-3|-3|-3|-3|-3]-2]-2][4][4][4][4][4][4]", "   [4][6][6]-3|-3|-3|-3|-3]-2]-2][4][4][4][4][4][4]", "X#<[4][6][6]-3|-3|-3|-3|-3]-2]-2][4][4][4][4][4][4]", "   [4][6][6][6][6][6][6][4][4][4][4][4][4][4][4][4]", "   [4][4][4][4][4][4][4][4][4][4][4][4][4][4][4][4]");

			NuclearWasteStorageProto overrideStorage = new NuclearWasteStorageProto(
				id: protoID,
				Proto.CreateStr(protoID, "Spent fuel storage", "A special underground storage facility that can safely manage any radioactive waste without causing any danger to the island’s population. Leaving a legacy for the next generations to come."),
				layout,
				productsFilter: ProductUtility.RadioactiveProductFilter,
				productType: CountableProductProto.ProductType,
				capacity: capacity_nuclear.Quantity(),
				retiredWasteCapacity: capacity_retired_waste_capacity.Quantity(),
				costs: Costs.Buildings.NuclearWasteStorage.MapToEntityCosts(registrator),
				nextTier: Option.None,
				graphics: new LayoutEntityProto.Gfx("Assets/Base/Buildings/WasteStorage.prefab", customIconPath: Option.Some(iconPath), categories: new ImmutableArray<ToolbarCategoryProto>?(registrator.GetCategoriesProtos(category))),
				emissionIntensity: 5,
				powerConsumedForProductsExchange: 120.Kw());


			// Add new to Database
			registrator.PrototypesDb.Add(overrideStorage, true);
			
			BetterDebug.Info("NuclearWasteStorage (override:" + BetterMod.Config.Storage.OverrideVanilla.ToString() + ") >> created!");
		}
	}
}