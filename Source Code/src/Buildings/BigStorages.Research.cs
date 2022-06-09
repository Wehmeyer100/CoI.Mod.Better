using CoI.Mod.Better.Extensions;
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

namespace CoI.Mod.Better
{
    internal partial class BigStorages : IModData
    {
        private static void GenerateResearch(ProtoRegistrator registrator)
        {
            GenerateResearchT1(registrator);
            GenerateResearchT2(registrator);
            GenerateResearchT3(registrator);
            GenerateResearchT4(registrator);
        }

        private static void GenerateResearchT1(ProtoRegistrator registrator)
        {
            ResearchNodeProto.ID storageResearch = MyIDs.Research.StorageResearchT1;

            LocStr1 locStr = Loc.Str1(storageResearch.ToString() + "__desc", "Unlock Storages T1", "");
            LocStr desc = LocalizationManager.CreateAlreadyLocalizedStr(storageResearch.ToString() + "_formatted", "");

            ResearchNodeProto result = registrator.ResearchNodeProtoBuilder
                .Start("Storage T1", storageResearch)
                .Description(desc)
                .SetCostsFree()
                .AddLayoutEntityToUnlock(MyIDs.Buildings.StorageFluidT1)
                .AddLayoutEntityToUnlock(MyIDs.Buildings.StorageLooseT1)
                .AddLayoutEntityToUnlock(MyIDs.Buildings.StorageUnitT1)
                .BuildAndAdd()
                .AddParentPlusGridPos(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.VehicleCapIncreaseID_ZERO), 0, BetterMod.UI_StepSize);

            Debug.Log("BigStorages >> GenerateResearchT1 >> created!");
        }

        private static void GenerateResearchT2(ProtoRegistrator registrator)
        {
            ResearchNodeProto.ID storageResearch = MyIDs.Research.StorageResearchT2;

            LocStr1 locStr = Loc.Str1(storageResearch.ToString() + "__desc", "Unlock Storages T2", "");
            LocStr desc = LocalizationManager.CreateAlreadyLocalizedStr(storageResearch.ToString() + "_formatted", "");

            ResearchNodeProto result = registrator.ResearchNodeProtoBuilder
                .Start("Storage T2", storageResearch)
                .Description(desc)
                .SetCosts(4)
                .AddLayoutEntityToUnlock(MyIDs.Buildings.StorageFluidT2)
                .AddLayoutEntityToUnlock(MyIDs.Buildings.StorageLooseT2)
                .AddLayoutEntityToUnlock(MyIDs.Buildings.StorageUnitT2)
                .BuildAndAdd()
                .AddParentPlusGridPos(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.StorageResearchT1));

            Debug.Log("BigStorages >> GenerateResearchT2 >> created!");
        }

        private static void GenerateResearchT3(ProtoRegistrator registrator)
        {
            ResearchNodeProto.ID storageResearch = MyIDs.Research.StorageResearchT3;

            LocStr1 locStr = Loc.Str1(storageResearch.ToString() + "__desc", "Unlock Storages T3", "");
            LocStr desc = LocalizationManager.CreateAlreadyLocalizedStr(storageResearch.ToString() + "_formatted", "");

            ResearchNodeProto result = registrator.ResearchNodeProtoBuilder
                .Start("Storage T3", storageResearch)
                .Description(desc)
                .SetCosts(8)
                .AddLayoutEntityToUnlock(MyIDs.Buildings.StorageFluidT3)
                .AddLayoutEntityToUnlock(MyIDs.Buildings.StorageLooseT3)
                .AddLayoutEntityToUnlock(MyIDs.Buildings.StorageUnitT3)
                .BuildAndAdd()
                .AddParentPlusGridPos(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.StorageResearchT2));

            Debug.Log("BigStorages >> GenerateResearchT3 >> created!");
        }

        private static void GenerateResearchT4(ProtoRegistrator registrator)
        {
            ResearchNodeProto.ID storageResearch = MyIDs.Research.StorageResearchT4;

            LocStr1 locStr = Loc.Str1(storageResearch.ToString() + "__desc", "Unlock Storages T4", "");
            LocStr desc = LocalizationManager.CreateAlreadyLocalizedStr(storageResearch.ToString() + "_formatted", "");

            ResearchNodeProto result = registrator.ResearchNodeProtoBuilder
                .Start("Storage T4", storageResearch)
                .Description(desc)
                .SetCosts(16)
                .AddLayoutEntityToUnlock(MyIDs.Buildings.StorageFluidT4)
                .AddLayoutEntityToUnlock(MyIDs.Buildings.StorageLooseT4)
                .AddLayoutEntityToUnlock(MyIDs.Buildings.StorageUnitT4)
                .BuildAndAdd()
                .AddParentPlusGridPos(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.StorageResearchT3));

            Debug.Log("BigStorages >> GenerateResearchT4 >> created!");
        }

        private static void GenerateResearchT5(ProtoRegistrator registrator)
        {
            ResearchNodeProto.ID storageResearch = MyIDs.Research.StorageResearchT5;

            LocStr1 locStr = Loc.Str1(storageResearch.ToString() + "__desc", "Unlock Storages T5", "");
            LocStr desc = LocalizationManager.CreateAlreadyLocalizedStr(storageResearch.ToString() + "_formatted", "");

            ResearchNodeProto result = registrator.ResearchNodeProtoBuilder
                .Start("Storage T5", storageResearch)
                .Description(desc)
                .SetCosts(20)
                .AddLayoutEntityToUnlock(MyIDs.Buildings.NuclearWasteStorage)
                .BuildAndAdd()
                .AddParentPlusGridPos(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.StorageResearchT4));

            Debug.Log("BigStorages >> GenerateResearchT5 >> created!");
        }

    }
}