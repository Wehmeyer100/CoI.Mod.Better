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
        private void AddFarmWaterConsumMultiplier(ProtoRegistrator registrator)
        {
            if (!BetterMod.Config.Systems.Cheats) return;

            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT1_CHEAT, CategoryCheats, "farm_consume_multiplier_t1", _cheatUpkeepEdicts, IdsCore.PropertyIds.FarmWaterConsumptionMultiplier, -20, null, Mafi.Base.Assets.Base.Icons.Edicts.FarmingBoost_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT2_CHEAT, CategoryCheats, "farm_consume_multiplier_t2", _cheatUpkeepEdicts, IdsCore.PropertyIds.FarmWaterConsumptionMultiplier, -40, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT1_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FarmingBoost_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT3_CHEAT, CategoryCheats, "farm_consume_multiplier_t3", _cheatUpkeepEdicts, IdsCore.PropertyIds.FarmWaterConsumptionMultiplier, -50, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT2_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FarmingBoost_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT4_CHEAT, CategoryCheats, "farm_consume_multiplier_t4", _cheatUpkeepEdicts, IdsCore.PropertyIds.FarmWaterConsumptionMultiplier, -75, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT3_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FarmingBoost_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT5_CHEAT, CategoryCheats, "farm_consume_multiplier_t5", _cheatUpkeepEdicts, IdsCore.PropertyIds.FarmWaterConsumptionMultiplier, -95, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT4_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FarmingBoost_svg);
        }
    }
}
