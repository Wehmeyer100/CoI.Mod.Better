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
    internal partial class GenerellEdicts : IModData
    {
        private void AddReduceService(ProtoRegistrator registrator)
        {
            // Add Cheats
            if (!BetterMod.Config.Systems.Cheats) return;

            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.ReduceServiceT1_CHEAT, categoryCheats, "reduce_service_t1", CheatUpkeepEdicts, IdsCore.PropertyIds.FoodConsumptionMultiplier, 20, null, Mafi.Base.Assets.Base.Icons.Edicts.FoodReduced_png);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.ReduceServiceT2_CHEAT, categoryCheats, "reduce_service_t2", CheatUpkeepEdicts, IdsCore.PropertyIds.FoodConsumptionMultiplier, 40, MyIDs.Eticts.Generell.ReduceServiceT1_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FoodReduced_png);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.ReduceServiceT3_CHEAT, categoryCheats, "reduce_service_t3", CheatUpkeepEdicts, IdsCore.PropertyIds.FoodConsumptionMultiplier, 60, MyIDs.Eticts.Generell.ReduceServiceT2_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FoodReduced_png);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.ReduceServiceT4_CHEAT, categoryCheats, "reduce_service_t4", CheatUpkeepEdicts, IdsCore.PropertyIds.FoodConsumptionMultiplier, 80, MyIDs.Eticts.Generell.ReduceServiceT3_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FoodReduced_png);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.ReduceServiceT5_CHEAT, categoryCheats, "reduce_service_t5", CheatUpkeepEdicts, IdsCore.PropertyIds.FoodConsumptionMultiplier, 100, MyIDs.Eticts.Generell.ReduceServiceT4_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FoodReduced_png);
        }
    }
}
