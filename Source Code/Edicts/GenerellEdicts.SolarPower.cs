using CoI.Mod.Better.Extensions;
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
        private void AddSolarPower(ProtoRegistrator registrator)
        {
            // Add Cheats
            if (!BetterMod.Config.Systems.Cheats) return;

            // Add Cheats
            GenerateSolarPower(registrator, MyIDs.Eticts.Generell.SolarPowerT1_CHEAT, 25, null, true);
            GenerateSolarPower(registrator, MyIDs.Eticts.Generell.SolarPowerT2_CHEAT, 50, MyIDs.Eticts.Generell.SolarPowerT1_CHEAT, true);
            GenerateSolarPower(registrator, MyIDs.Eticts.Generell.SolarPowerT3_CHEAT, 100, MyIDs.Eticts.Generell.SolarPowerT2_CHEAT, true);
            GenerateSolarPower(registrator, MyIDs.Eticts.Generell.SolarPowerT4_CHEAT, 200, MyIDs.Eticts.Generell.SolarPowerT3_CHEAT, true);
            GenerateSolarPower(registrator, MyIDs.Eticts.Generell.SolarPowerT5_CHEAT, 300, MyIDs.Eticts.Generell.SolarPowerT4_CHEAT, true);
        }

        private int countSolarPowerEdicts = 1;
        private void GenerateSolarPower(ProtoRegistrator registrator, Proto.ID protoID, int multiplier, Proto.ID? previusEdict, bool cheat = false)
        {
            countSolarPowerEdicts++;

            LocStr1 locStr = Loc.Str1(
                protoID.ToString() + "__desc",
                "Solar power increased by {0}%",
                "policy / edict which can enabled by the player in their Captain's office. {0}=" + multiplier + "%"
            );

            LocStr descShort = LocalizationManager.CreateAlreadyLocalizedStr(
                protoID.ToString() + "_formatted",
                locStr.Format(multiplier.ToString()).Value
            );

            Option<EdictProto> previousTier = Option<EdictProto>.None;
            if (previusEdict.HasValue)
            {
                previousTier = registrator.PrototypesDb.GetOrThrow<EdictProto>(previusEdict.Value);
            }

            registrator.PrototypesDb.Add(new EdictWithPropertiesProto(
                protoID,
                Proto.CreateStr(protoID, "Solar Power T" + countSolarPowerEdicts.ToString(), descShort, translationComment),
                (cheat ? categoryCheats : category),
                CheatUpkeepEdicts.Upoints(),
                ImmutableArray.Create(Make.Kvp(IdsCore.PropertyIds.SolarPowerMultiplier, multiplier.Percent())),
                previousTier,
                new EdictProto.Gfx(Mafi.Base.Assets.Base.Icons.Edicts.SolarBoost_svg))
            );
        }
    }
}
