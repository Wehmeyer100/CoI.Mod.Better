using CoI.Mod.Better.Shared;
using CoI.Mod.Better.Shared.Lang;
using CoI.Mod.Better.Shared.Utilities;
using Mafi.Base;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Entities.Static;
using Mafi.Core.Mods;
using Mafi.Core.Products;

namespace CoI.Mod.Better.Buildings
{
	internal partial class BigStorages : IModData
	{
        #region Unit Storages Override

		private void UnitStoragesT1(ProtoRegistrator registrator)
		{
			// Set proto ids
			StaticEntityProto.ID protoID = Ids.Buildings.StorageUnit;
			StaticEntityProto.ID protoNextTier = Ids.Buildings.StorageUnitT2;

			if (BetterMod.Config.Storage.OverrideVanilla)
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
			string Name = LangManager.Instance.Get("unit_storage");
			string desc = LangManager.Instance.Get("unit_storage_desc", capacity_T1.ToString());

			// Add new to Database
			StorageProtoBuilder.State creator = registrator.StorageProtoBuilder.Start(Name + "I", protoID)
				.Description(desc)
				.SetCost(Costs.Buildings.StorageUnit)
				.SetNextTier(registrator.PrototypesDb.GetOrThrow<StorageProto>(protoNextTier))
				.SetCapacity(capacity_T1)
				.SetProductsFilter(ProductUtility.ProductFilter)
				.SetLayout("   [4][4][4][4][4]   ", "A#>[4][4][4][4][4]>#X", "   [4][4][4][4][4]   ", "B#>[4][4][4][4][4]>#Y", "   [4][4][4][4][4]   ")
				.SetPrefabPath("Assets/Base/Buildings/Storages/UnitT1.prefab");

			if (!BetterMod.Config.Storage.OverrideVanilla)
			{
				creator.SetCustomIconPath(registrator.PrototypesDb.GetOrThrow<StorageProto>(Ids.Buildings.StorageUnit).Graphics.IconPath);
			}
			creator = SetCategory(creator);
			Utilities.ProductUtility.SetTransferLimitByT(creator, 1).BuildAndAdd(CountableProductProto.ProductType);
			BetterDebug.Info("UnitStoragesT1 (override:" + BetterMod.Config.Storage.OverrideVanilla + ") >> created!");
		}

		private void UnitStoragesT2(ProtoRegistrator registrator)
		{
			// Set proto ids
			StaticEntityProto.ID protoID = Ids.Buildings.StorageUnitT2;

			if (BetterMod.Config.Storage.OverrideVanilla)
			{
				// Remove from Database
				registrator.PrototypesDb.RemoveOrThrow(protoID);
			}
			else
			{
				protoID = MyIDs.Buildings.StorageUnitT2;
			}
//
			// Generate LocStr
			string Name = LangManager.Instance.Get("unit_storage");
			string desc = LangManager.Instance.Get("unit_storage_desc", capacity_T2.ToString());

			// Add new to Database
			StorageProtoBuilder.State creator = registrator.StorageProtoBuilder.Start(Name + " II", protoID)
				.Description(desc)
				.SetCost(Costs.Buildings.StorageUnitT2)
				.SetCapacity(capacity_T2)
				.SetProductsFilter(ProductUtility.ProductFilter)
				.SetLayout("   [5][5][5][5][5]   ", "A#>[5][5][5][5][5]>#X", "   [5][5][5][5][5]   ", "B#>[5][5][5][5][5]>#Y", "   [5][5][5][5][5]   ")
				.SetPrefabPath("Assets/Base/Buildings/Storages/UnitT2.prefab");

			if (!BetterMod.Config.Storage.OverrideVanilla)
			{
				creator.SetCustomIconPath(registrator.PrototypesDb.GetOrThrow<StorageProto>(Ids.Buildings.StorageUnitT2).Graphics.IconPath);
			}
			creator = SetCategory(creator);
			Utilities.ProductUtility.SetTransferLimitByT(creator, 2).BuildAndAdd(CountableProductProto.ProductType);
			BetterDebug.Info("UnitStoragesT2 (override:" + BetterMod.Config.Storage.OverrideVanilla + ") >> created!");
		}

		private void UnitStoragesT3(ProtoRegistrator registrator)
		{
			// Set proto ids
			StaticEntityProto.ID protoID = Ids.Buildings.StorageUnitT3;
			StaticEntityProto.ID protoNextTier = Ids.Buildings.StorageUnitT4;

			if (BetterMod.Config.Storage.OverrideVanilla)
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
			string Name = LangManager.Instance.Get("unit_storage");
			string desc = LangManager.Instance.Get("unit_storage_desc", capacity_T3.ToString());

			// Add new to Database
			StorageProtoBuilder.State creator = registrator.StorageProtoBuilder.Start(Name + " III", protoID)
				.Description(desc)
				.SetCost(Costs.Buildings.StorageUnitT3)
				.SetNextTier(registrator.PrototypesDb.GetOrThrow<StorageProto>(protoNextTier))
				.SetCapacity(capacity_T3)
				.SetProductsFilter(ProductUtility.ProductFilter)
				.SetLayout("   [6][6][6][6][6][6][6][6][6][6]   ", "A#>[6][6][6][6][6][6][6][6][6][6]>#X", "   [6][6][6][6][6][6][6][6][6][6]   ", "B#>[6][6][6][6][6][6][6][6][6][6]>#Y", "   [6][6][6][6][6][6][6][6][6][6]   ", "   [6][6][6][6][6][6][6][6][6][6]   ", "C#>[6][6][6][6][6][6][6][6][6][6]>#Z", "   [6][6][6][6][6][6][6][6][6][6]   ", "D#>[6][6][6][6][6][6][6][6][6][6]>#W", "   [6][6][6][6][6][6][6][6][6][6]   ")
				.SetPrefabPath("Assets/Base/Buildings/Storages/UnitT3.prefab");


			if (!BetterMod.Config.Storage.OverrideVanilla)
			{
				creator.SetCustomIconPath(registrator.PrototypesDb.GetOrThrow<StorageProto>(Ids.Buildings.StorageUnitT3).Graphics.IconPath);
			}
			creator = SetCategory(creator);
			Utilities.ProductUtility.SetTransferLimitByT(creator, 3).BuildAndAdd(CountableProductProto.ProductType);
			BetterDebug.Info("UnitStoragesT3 (override:" + BetterMod.Config.Storage.OverrideVanilla + ") >> created!");
		}

		private void UnitStoragesT4(ProtoRegistrator registrator)
		{
			// Set proto ids
			StaticEntityProto.ID protoID = Ids.Buildings.StorageUnitT4;

			if (BetterMod.Config.Storage.OverrideVanilla)
			{
				// Remove from Database
				registrator.PrototypesDb.RemoveOrThrow(protoID);
			}
			else
			{
				protoID = MyIDs.Buildings.StorageUnitT4;
			}

			// Generate LocStr
			string Name = LangManager.Instance.Get("unit_storage");
			string desc = LangManager.Instance.Get("unit_storage_desc", capacity_T4.ToString());

			// Add new to Database
			StorageProtoBuilder.State creator = registrator.StorageProtoBuilder.Start(Name + " IV", protoID)
				.Description(desc)
				.SetCost(Costs.Buildings.StorageUnitT4)
				.SetCapacity(capacity_T4)
				.SetProductsFilter(ProductUtility.ProductFilter)
				.SetLayout("   [8][8][8][8][8][8][8][8][8][8]   ", "A#>[8][8][8][8][8][8][8][8][8][8]>#X", "   [8][8][8][8][8][8][8][8][8][8]   ", "B#>[8][8][8][8][8][8][8][8][8][8]>#Y", "   [8][8][8][8][8][8][8][8][8][8]   ", "   [8][8][8][8][8][8][8][8][8][8]   ", "C#>[8][8][8][8][8][8][8][8][8][8]>#Z", "   [8][8][8][8][8][8][8][8][8][8]   ", "D#>[8][8][8][8][8][8][8][8][8][8]>#W", "   [8][8][8][8][8][8][8][8][8][8]   ")
				.SetPrefabPath("Assets/Base/Buildings/Storages/UnitT4.prefab");

			if (!BetterMod.Config.Storage.OverrideVanilla)
			{
				creator.SetCustomIconPath(registrator.PrototypesDb.GetOrThrow<StorageProto>(Ids.Buildings.StorageUnitT4).Graphics.IconPath);
			}
			creator = SetCategory(creator);
			Utilities.ProductUtility.SetTransferLimitByT(creator, 4).BuildAndAdd(CountableProductProto.ProductType);
			BetterDebug.Info("UnitStoragesT4 (override:" + BetterMod.Config.Storage.OverrideVanilla + ") >> created!");
		}

        #endregion
	}
}