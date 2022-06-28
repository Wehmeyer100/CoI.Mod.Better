using CoI.Mod.Better.Extensions;
using CoI.Mod.Better.lang;
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
using static CoI.Mod.Better.Utilities.ResearchProtoUtility;

namespace CoI.Mod.Better.Buildings
{
    public partial class SteamStorages : IModData
    {
        private void GenerateResearch(ProtoRegistrator registrator)
        {
            string Name = LangManager.Instance.Get("research_steam_storage");

            ResearchNodeProto parent_t1 = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.PowerGeneration2);
            ResearchNodeProto research_t1 = GenerateResearchBuildings(registrator, MyIDs.Research.SteamStorageResearchT1, Name + " I", "", 5, false, new ResearchNodeUIData(parent_t1, false, BetterMod.UI_StepSize, BetterMod.UI_StepSize * 5), MyIDs.Buildings.StorageSteamT1);
            GenerateResearchBuildings(registrator, MyIDs.Research.SteamStorageResearchT2, Name + " II", "", 7, false, new ResearchNodeUIData(research_t1, false, BetterMod.UI_StepSize * 2, 0), MyIDs.Buildings.StorageSteamT2);

            ResearchNodeProto parent_t2 = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.PowerGeneration3);
            ResearchNodeProto research_t3 = GenerateResearchBuildings(registrator, MyIDs.Research.SteamStorageResearchT3, Name + " III", "", 14, false, new ResearchNodeUIData(parent_t2, false, BetterMod.UI_StepSize, BetterMod.UI_StepSize * 5), MyIDs.Buildings.StorageSteamT3);
            GenerateResearchBuildings(registrator, MyIDs.Research.SteamStorageResearchT4, Name + " IV", "", 20, false, new ResearchNodeUIData(research_t3, false, BetterMod.UI_StepSize * 6, 0), MyIDs.Buildings.StorageSteamT4);
        }
    }
}