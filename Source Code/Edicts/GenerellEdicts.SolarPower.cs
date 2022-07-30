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
        private void AddSolarPower(ProtoRegistrator registrator)
        {
            // Add Cheats
            if (!BetterMod.Config.Systems.Cheats) return;

            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.SolarPowerT1_CHEAT, CategoryCheats, "solar_power_t1", _cheatUpkeepEdicts, IdsCore.PropertyIds.SolarPowerMultiplier, 25, null, Mafi.Base.Assets.Base.Icons.Edicts.SolarBoost_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.SolarPowerT2_CHEAT, CategoryCheats, "solar_power_t2", _cheatUpkeepEdicts, IdsCore.PropertyIds.SolarPowerMultiplier, 50, MyIDs.Eticts.Generell.SolarPowerT1_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.SolarBoost_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.SolarPowerT3_CHEAT, CategoryCheats, "solar_power_t3", _cheatUpkeepEdicts, IdsCore.PropertyIds.SolarPowerMultiplier, 100, MyIDs.Eticts.Generell.SolarPowerT2_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.SolarBoost_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.SolarPowerT4_CHEAT, CategoryCheats, "solar_power_t4", _cheatUpkeepEdicts, IdsCore.PropertyIds.SolarPowerMultiplier, 200, MyIDs.Eticts.Generell.SolarPowerT3_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.SolarBoost_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.SolarPowerT5_CHEAT, CategoryCheats, "solar_power_t5", _cheatUpkeepEdicts, IdsCore.PropertyIds.SolarPowerMultiplier, 300, MyIDs.Eticts.Generell.SolarPowerT4_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.SolarBoost_svg);
        }
    }
}
