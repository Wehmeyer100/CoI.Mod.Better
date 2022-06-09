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

namespace CoI.Mod.Better
{
    internal partial class GenerellEdicts : IModData
    {
        private void AddReduceService(ProtoRegistrator registrator)
        {
            // Add Cheats
            if (BetterMod.Config.DisableCheats) return;

            // Add Cheats
            GenerateReduceService(registrator, MyIDs.Eticts.Generell.ReduceServiceT1_CHEAT, 30, null, true);
            GenerateReduceService(registrator, MyIDs.Eticts.Generell.ReduceServiceT2_CHEAT, 40, MyIDs.Eticts.Generell.ReduceServiceT1_CHEAT, true);
            GenerateReduceService(registrator, MyIDs.Eticts.Generell.ReduceServiceT3_CHEAT, 50, MyIDs.Eticts.Generell.ReduceServiceT2_CHEAT, true);
            GenerateReduceService(registrator, MyIDs.Eticts.Generell.ReduceServiceT4_CHEAT, 60, MyIDs.Eticts.Generell.ReduceServiceT3_CHEAT, true);
            GenerateReduceService(registrator, MyIDs.Eticts.Generell.ReduceServiceT5_CHEAT, 75, MyIDs.Eticts.Generell.ReduceServiceT4_CHEAT, true);
        }

        private void GenerateReduceService(ProtoRegistrator registrator, Proto.ID protoID, int reduceServiceConsum, Proto.ID? previusEdict, bool cheat = false)
        {
            countReduceServiceEdicts++;

            LocStr1 locStr = Loc.Str1(
                protoID.ToString() + "__desc",
                "All settlement services consumption increased by {0}",
                "policy / edict which can enabled by the player in their Captain's office. {0}=" + reduceServiceConsum + "%"
            );

            LocStr descShort = LocalizationManager.CreateAlreadyLocalizedStr(
                protoID.ToString() + "_formatted",
                locStr.Format(reduceServiceConsum.ToString()).Value
            );

            Option<EdictProto> previousTier = Option<EdictProto>.None;
            if (previusEdict.HasValue)
            {
                previousTier = registrator.PrototypesDb.GetOrThrow<EdictProto>(previusEdict.Value);
            }

            registrator.PrototypesDb.Add(new EdictWithPropertiesProto(
                protoID,
                Proto.CreateStr(protoID, "Settlement Consumption T" + countReduceServiceEdicts.ToString(), descShort, translationComment),
                (cheat ? categoryCheats : category),
                CheatUpkeepEdicts.Upoints(),
                ImmutableArray.Create(Make.Kvp(IdsCore.PropertyIds.FoodConsumptionMultiplier, reduceServiceConsum.Percent())),
                previousTier,
                new EdictProto.Gfx("Assets/Base/Icons/Edicts/FoodReduced.svg"))
            );
        }
    }
}
