using CoI.Mod.Better.MyIDs;
using CoI.Mod.Better.Shared;
using Mafi.Base;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Mods;
using UnityEngine;

namespace CoI.Mod.Better.Buildings
{
	internal partial class BigStorages : IModData
	{
		private int capacity_fluid_T1;
		private int capacity_fluid_T2;
		private int capacity_fluid_T3;
		private int capacity_fluid_T4;

		private int capacity_nuclear;
		private int capacity_T1;
		private int capacity_T2;
		private int capacity_T3;
		private int capacity_T4;

		public void RegisterData(ProtoRegistrator registrator)
		{
			if (!BetterMod.Config.Systems.BigStorage) return;

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

			GenerateResearch(registrator);
		}

		private void LoadData()
		{
			capacity_T1 = BetterMod.Config.Storage.CapacityT1;
			capacity_T1 = Mathf.Clamp(capacity_T1, VanillaConstants.StorageCapacityT1, int.MaxValue);

			capacity_T2 = BetterMod.Config.Storage.CapacityT2;
			capacity_T2 = Mathf.Clamp(capacity_T2, VanillaConstants.StorageCapacityT2, int.MaxValue);

			capacity_T3 = BetterMod.Config.Storage.CapacityT3;
			capacity_T3 = Mathf.Clamp(capacity_T3, VanillaConstants.StorageCapacityT3, int.MaxValue);

			capacity_T4 = BetterMod.Config.Storage.CapacityT4;
			capacity_T4 = Mathf.Clamp(capacity_T4, VanillaConstants.StorageCapacityT4, int.MaxValue);

			float fluidStorageCapacityMultiplier = BetterMod.Config.Storage.FluidCapacityMultiplier;


			capacity_fluid_T1 = (int)(capacity_T1 * fluidStorageCapacityMultiplier);
			capacity_fluid_T1 = Mathf.Clamp(capacity_fluid_T1, VanillaConstants.StorageCapacityT1, int.MaxValue);

			capacity_fluid_T2 = (int)(capacity_T2 * fluidStorageCapacityMultiplier);
			capacity_fluid_T2 = Mathf.Clamp(capacity_fluid_T2, VanillaConstants.StorageCapacityT2, int.MaxValue);

			capacity_fluid_T3 = (int)(capacity_T3 * fluidStorageCapacityMultiplier);
			capacity_fluid_T3 = Mathf.Clamp(capacity_fluid_T3, VanillaConstants.StorageCapacityT3, int.MaxValue);

			capacity_fluid_T4 = (int)(capacity_T4 * fluidStorageCapacityMultiplier);
			capacity_fluid_T4 = Mathf.Clamp(capacity_fluid_T4, VanillaConstants.StorageCapacityT4, int.MaxValue);

			float nuclearWasteStorageCapacityMultiplier = BetterMod.Config.Storage.NuclearWasteCapacityMultiplier;
			capacity_nuclear = (int)(BetterMod.Config.Storage.CapacityNuclearWaste * nuclearWasteStorageCapacityMultiplier);
			capacity_nuclear = Mathf.Clamp(capacity_nuclear, BetterMod.Config.Storage.CapacityNuclearWaste, int.MaxValue);
		}


		private static StorageProtoBuilder.State SetCategory(StorageProtoBuilder.State creator)
		{
			if (BetterMod.Config.Storage.OverrideVanilla)
			{
				creator.SetCategories(Ids.ToolbarCategories.Storages);
			}
			else
			{
				creator.SetCategories(ToolbarCategories.Storages);
			}
			return creator;
		}
	}
}