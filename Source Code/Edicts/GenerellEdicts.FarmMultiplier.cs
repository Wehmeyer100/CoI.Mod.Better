using CoI.Mod.Better.Extensions;
using CoI.Mod.Better.lang;
using CoI.Mod.Better.Utilities;
using Mafi;
using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core;
using Mafi.Core.Mods;
using Mafi.Core.Population.Edicts;
using Mafi.Core.PropertiesDb;
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
        private void AddFarmMultiplier(ProtoRegistrator registrator)
        {
            if (!BetterMod.Config.Systems.Cheats) return;

            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.FarmMultiplierT1_CHEAT, categoryCheats, "farm_multiplier_t1", CheatUpkeepEdicts, IdsCore.PropertyIds.FarmYieldMultiplier, 25, null, Mafi.Base.Assets.Base.Icons.Edicts.FarmingBoost_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.FarmMultiplierT2_CHEAT, categoryCheats, "farm_multiplier_t2", CheatUpkeepEdicts, IdsCore.PropertyIds.FarmYieldMultiplier, 50, MyIDs.Eticts.Generell.FarmMultiplierT1_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FarmingBoost_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.FarmMultiplierT3_CHEAT, categoryCheats, "farm_multiplier_t3", CheatUpkeepEdicts, IdsCore.PropertyIds.FarmYieldMultiplier, 100, MyIDs.Eticts.Generell.FarmMultiplierT2_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FarmingBoost_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.FarmMultiplierT4_CHEAT, categoryCheats, "farm_multiplier_t4", CheatUpkeepEdicts, IdsCore.PropertyIds.FarmYieldMultiplier, 200, MyIDs.Eticts.Generell.FarmMultiplierT3_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FarmingBoost_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.FarmMultiplierT5_CHEAT, categoryCheats, "farm_multiplier_t5", CheatUpkeepEdicts, IdsCore.PropertyIds.FarmYieldMultiplier, 300, MyIDs.Eticts.Generell.FarmMultiplierT4_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FarmingBoost_svg);
        }

    }
}
