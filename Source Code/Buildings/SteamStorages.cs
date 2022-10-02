using CoI.Mod.Better.Shared;
using Mafi.Core.Mods;
using UnityEngine;

namespace CoI.Mod.Better.Buildings
{
	public partial class SteamStorages : IModData
	{
		private int capacity_steam_T1;
		private int capacity_steam_T2;
		private int capacity_steam_T3;
		private int capacity_steam_T4;

		public void RegisterData(ProtoRegistrator registrator)
		{
			if (!BetterMod.Config.Systems.SteamStorage) return;

			LoadData();

			SteamStoragesT2(registrator);
			SteamStoragesT1(registrator);
			SteamStoragesT4(registrator);
			SteamStoragesT3(registrator);

			GenerateResearch(registrator);
		}

		private void LoadData()
		{
			float steamStorageCapacityMultiplier = BetterMod.Config.Storage.SteamCapacityMultiplier;

			if (BetterMod.Config.Systems.BigStorage)
			{
				capacity_steam_T1 = (int)(BetterMod.Config.Storage.CapacityT1 * steamStorageCapacityMultiplier);
				capacity_steam_T2 = (int)(BetterMod.Config.Storage.CapacityT2 * steamStorageCapacityMultiplier);
				capacity_steam_T3 = (int)(BetterMod.Config.Storage.CapacityT3 * steamStorageCapacityMultiplier);
				capacity_steam_T4 = (int)(BetterMod.Config.Storage.CapacityT4 * steamStorageCapacityMultiplier);
			}
			else
			{
				capacity_steam_T1 = VanillaConstants.StorageCapacityT1;
				capacity_steam_T2 = VanillaConstants.StorageCapacityT2;
				capacity_steam_T3 = VanillaConstants.StorageCapacityT3;
				capacity_steam_T4 = VanillaConstants.StorageCapacityT4;
			}

			capacity_steam_T1 = Mathf.Clamp(capacity_steam_T1, VanillaConstants.StorageCapacityT1, int.MaxValue);
			capacity_steam_T2 = Mathf.Clamp(capacity_steam_T2, VanillaConstants.StorageCapacityT2, int.MaxValue);
			capacity_steam_T3 = Mathf.Clamp(capacity_steam_T3, VanillaConstants.StorageCapacityT3, int.MaxValue);
			capacity_steam_T4 = Mathf.Clamp(capacity_steam_T4, VanillaConstants.StorageCapacityT4, int.MaxValue);
		}
	}
}