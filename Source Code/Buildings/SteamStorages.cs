using CoI.Mod.Better.Extensions;
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
using Mafi.Core.Research;
using Mafi.Localization;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CoI.Mod.Better.Buildings
{
    public partial class SteamStorages : IModData
    {

        private int capacity_steam_T1 = 0;
        private int capacity_steam_T2 = 0;
        private int capacity_steam_T3 = 0;
        private int capacity_steam_T4 = 0;

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
                capacity_steam_T1 = ProductUtility.Storage_Vanilla_Capacity_T1;
                capacity_steam_T2 = ProductUtility.Storage_Vanilla_Capacity_T2;
                capacity_steam_T3 = ProductUtility.Storage_Vanilla_Capacity_T3;
                capacity_steam_T4 = ProductUtility.Storage_Vanilla_Capacity_T4;
            }

            capacity_steam_T1 = Mathf.Clamp(capacity_steam_T1, ProductUtility.Storage_Vanilla_Capacity_T1, int.MaxValue);
            capacity_steam_T2 = Mathf.Clamp(capacity_steam_T2, ProductUtility.Storage_Vanilla_Capacity_T2, int.MaxValue);
            capacity_steam_T3 = Mathf.Clamp(capacity_steam_T3, ProductUtility.Storage_Vanilla_Capacity_T3, int.MaxValue);
            capacity_steam_T4 = Mathf.Clamp(capacity_steam_T4, ProductUtility.Storage_Vanilla_Capacity_T4, int.MaxValue);
        }

    }
}