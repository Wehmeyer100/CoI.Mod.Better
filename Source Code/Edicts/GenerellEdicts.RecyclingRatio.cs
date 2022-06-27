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
        private void AddRecyclingRatioDiff(ProtoRegistrator registrator)
        {
            if (!BetterMod.Config.Systems.Cheats) return;

            // Add Cheats
            GenerateRecyclingRatioDiff(registrator, MyIDs.Eticts.Generell.RecyclingRatioDiffT1_CHEAT, 25, null, true);
            GenerateRecyclingRatioDiff(registrator, MyIDs.Eticts.Generell.RecyclingRatioDiffT2_CHEAT, 50, MyIDs.Eticts.Generell.RecyclingRatioDiffT1_CHEAT, true);
            GenerateRecyclingRatioDiff(registrator, MyIDs.Eticts.Generell.RecyclingRatioDiffT3_CHEAT, 100, MyIDs.Eticts.Generell.RecyclingRatioDiffT2_CHEAT, true);
            GenerateRecyclingRatioDiff(registrator, MyIDs.Eticts.Generell.RecyclingRatioDiffT4_CHEAT, 200, MyIDs.Eticts.Generell.RecyclingRatioDiffT3_CHEAT, true);
            GenerateRecyclingRatioDiff(registrator, MyIDs.Eticts.Generell.RecyclingRatioDiffT5_CHEAT, 300, MyIDs.Eticts.Generell.RecyclingRatioDiffT4_CHEAT, true);
        }

        private int countRecyclingRatioDiffEdicts = 1;
        private void GenerateRecyclingRatioDiff(ProtoRegistrator registrator, Proto.ID protoID, int multiplier, Proto.ID? previusEdict, bool cheat = false)
        {
            countRecyclingRatioDiffEdicts++;

            LocStr1 locStr = Loc.Str1(
                protoID.ToString() + "__desc",
                "Recycling ratio increased by {0}",
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
                Proto.CreateStr(protoID, "Recycling Ratio T" + countRecyclingRatioDiffEdicts.ToString(), descShort, translationComment),
                (cheat ? categoryCheats : category),
                CheatUpkeepEdicts.Upoints(),
                ImmutableArray.Create(Make.Kvp(IdsCore.PropertyIds.RecyclingRatioDiff, multiplier.Percent())),
                previousTier,
                new EdictProto.Gfx("Assets/Base/Icons/Edicts/FoodReduced.svg"))
            );
        }
    }
}
