using CoI.Mod.Better.Extensions;
using CoI.Mod.Better.Utilities;
using Mafi;
using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core;
using Mafi.Core.Mods;
using Mafi.Core.Population.Edicts;
using Mafi.Core.Prototypes;
using Mafi.Core.Research;
using Mafi.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CoI.Mod.Better.Edicts
{
    internal partial class GenerelEdicts : IModData
    {
        private void AddReduceService(ProtoRegistrator registrator)
        {
            // Add Cheats
            if (!BetterMod.Config.Systems.Cheats) return;

            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.ReduceServiceT1_CHEAT, CategoryCheats, "reduce_service_t1", _cheatUpkeepEdicts, IdsCore.PropertyIds.FoodConsumptionMultiplier, 20, null, Mafi.Base.Assets.Base.Icons.Edicts.FoodReduced_png);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.ReduceServiceT2_CHEAT, CategoryCheats, "reduce_service_t2", _cheatUpkeepEdicts, IdsCore.PropertyIds.FoodConsumptionMultiplier, 40, MyIDs.Eticts.Generell.ReduceServiceT1_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FoodReduced_png);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.ReduceServiceT3_CHEAT, CategoryCheats, "reduce_service_t3", _cheatUpkeepEdicts, IdsCore.PropertyIds.FoodConsumptionMultiplier, 60, MyIDs.Eticts.Generell.ReduceServiceT2_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FoodReduced_png);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.ReduceServiceT4_CHEAT, CategoryCheats, "reduce_service_t4", _cheatUpkeepEdicts, IdsCore.PropertyIds.FoodConsumptionMultiplier, 80, MyIDs.Eticts.Generell.ReduceServiceT3_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FoodReduced_png);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.ReduceServiceT5_CHEAT, CategoryCheats, "reduce_service_t5", _cheatUpkeepEdicts, IdsCore.PropertyIds.FoodConsumptionMultiplier, 100, MyIDs.Eticts.Generell.ReduceServiceT4_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FoodReduced_png);
        }
    }
}
