using CoI.Mod.Better.Extensions;
using CoI.Mod.Better.lang;
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
    internal partial class BigStorages : IModData
    {
        private void GenerateResearch(ProtoRegistrator registrator)
        {
            if (!BetterMod.Config.Storage.OverrideVanilla)
            {
                Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> BigStorages >> GenerateResearches...");
                string Name = LangManager.Instance.Get("research_storage");

                ResearchNodeProto parent = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.VehicleCapIncreaseID_ZERO);

                ResearchNodeProto research_t1 = GenerateResearchBuildings(registrator, MyIDs.Research.StorageResearchT1, Name + " I", "", 1, false, new ResearchNodeUIData(parent, false, 0, (BetterMod.UIStepSize * 2)), MyIDs.Buildings.StorageFluidT1, MyIDs.Buildings.StorageLooseT1, MyIDs.Buildings.StorageUnitT1);
                ResearchNodeProto research_t2 = GenerateResearchBuildings(registrator, MyIDs.Research.StorageResearchT2, Name + " II", "", 4, false, new ResearchNodeUIData(research_t1, false), MyIDs.Buildings.StorageFluidT2, MyIDs.Buildings.StorageLooseT2, MyIDs.Buildings.StorageUnitT2);
                ResearchNodeProto research_t3 = GenerateResearchBuildings(registrator, MyIDs.Research.StorageResearchT3, Name + " III", "", 8, false, new ResearchNodeUIData(research_t2, false), MyIDs.Buildings.StorageFluidT3, MyIDs.Buildings.StorageLooseT3, MyIDs.Buildings.StorageUnitT3);
                GenerateResearchBuildings(registrator, MyIDs.Research.StorageResearchT4, Name + " IV", "", 16, false, new ResearchNodeUIData(research_t3, false), MyIDs.Buildings.StorageFluidT4, MyIDs.Buildings.StorageLooseT4, MyIDs.Buildings.StorageUnitT4);

                Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> BigStorages >> GenerateResearches... done.");
            }
        }

        private void GenerateResearchT5(ProtoRegistrator registrator)
        {
            ResearchNodeProto.ID storageResearch = MyIDs.Research.StorageResearchT5;

            LocStr1 locStr = Loc.Str1(storageResearch.ToString() + "__desc", "Unlock Storages T5", "");
            LocStr desc = LocalizationManager.CreateAlreadyLocalizedStr(storageResearch.ToString() + "_formatted", "");

            ResearchNodeProtoBuilder.State result = registrator.ResearchNodeProtoBuilder
                .Start("Storage T5", storageResearch)
                .Description(desc)
                .AddLayoutEntityToUnlock(MyIDs.Buildings.NuclearWasteStorage);

            if (BetterMod.Config.Default.UnlockAllCheatsResearches)
            {
                result.SetCostsFree();
            }
            else
            {
                result.SetCosts(20);
            }

            result
                .BuildAndAdd()
                .AddParentPlusGridPos(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.StorageResearchT4));

            Debug.Log("BigStorages >> GenerateResearchT5 >> created!");
        }

    }
}