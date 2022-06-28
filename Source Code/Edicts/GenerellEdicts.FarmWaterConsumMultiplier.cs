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
        private void AddFarmWaterConsumMultiplier(ProtoRegistrator registrator)
        {
            if (!BetterMod.Config.Systems.Cheats) return;

            // Add Cheats
            GenerateFarmWaterConsumMultiplier(registrator, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT1_CHEAT, 20, null, true);
            GenerateFarmWaterConsumMultiplier(registrator, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT2_CHEAT, 40, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT1_CHEAT, true);
            GenerateFarmWaterConsumMultiplier(registrator, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT3_CHEAT, 50, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT2_CHEAT, true);
            GenerateFarmWaterConsumMultiplier(registrator, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT4_CHEAT, 75, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT3_CHEAT, true);
            GenerateFarmWaterConsumMultiplier(registrator, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT5_CHEAT, 95, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT4_CHEAT, true);
        }

        private int countFarmWaterConsumMultiplierEdicts = 1;
        private void GenerateFarmWaterConsumMultiplier(ProtoRegistrator registrator, Proto.ID protoID, int multiplier, Proto.ID? previusEdict, bool cheat = false)
        {
            countFarmWaterConsumMultiplierEdicts++;

            LocStr1 locStr = Loc.Str1(
                protoID.ToString() + "__desc",
                "All Farms consume decreased by {0}%",
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
                Proto.CreateStr(protoID, "Farm Water Consumption Multiplier T" + countFarmWaterConsumMultiplierEdicts.ToString(), descShort, translationComment),
                (cheat ? categoryCheats : category),
                CheatUpkeepEdicts.Upoints(),
                ImmutableArray.Create(Make.Kvp(IdsCore.PropertyIds.FarmWaterConsumptionMultiplier, (-multiplier).Percent())),
                previousTier,
                new EdictProto.Gfx(Mafi.Base.Assets.Base.Icons.Edicts.FarmingBoost_svg))
            );
        }
    }
}
