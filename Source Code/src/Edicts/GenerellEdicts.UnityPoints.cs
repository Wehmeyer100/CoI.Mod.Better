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
        private void AddUnityPoints(ProtoRegistrator registrator)
        {
            // Add Cheats
            if (BetterMod.Config.DisableCheats) return;

            // Add Cheats
            GenerateUnityPoints(registrator, MyIDs.Eticts.Generell.UnityPointsT1_CHEAT, 5.0f, null, true);
            GenerateUnityPoints(registrator, MyIDs.Eticts.Generell.UnityPointsT2_CHEAT, 10.0f, MyIDs.Eticts.Generell.UnityPointsT1_CHEAT, true);
            GenerateUnityPoints(registrator, MyIDs.Eticts.Generell.UnityPointsT3_CHEAT, 20.0f, MyIDs.Eticts.Generell.UnityPointsT2_CHEAT, true);
            GenerateUnityPoints(registrator, MyIDs.Eticts.Generell.UnityPointsT4_CHEAT, 50.0f, MyIDs.Eticts.Generell.UnityPointsT3_CHEAT, true);
            GenerateUnityPoints(registrator, MyIDs.Eticts.Generell.UnityPointsT5_CHEAT, 100.0f, MyIDs.Eticts.Generell.UnityPointsT4_CHEAT, true);
        }

        private void GenerateUnityPoints(ProtoRegistrator registrator, Proto.ID protoID, float monthlyUpointsCost, Proto.ID? previusEdict, bool cheat = false)
        {
            countUnityPointsEdicts++;

            LocStr1 locStr = Loc.Str1(
                protoID.ToString() + "__desc",
                "Unity increase by {0}",
                "policy / edict which can enabled by the player in their Captain's office. {0}=" + monthlyUpointsCost + "%"
            );

            LocStr descShort = LocalizationManager.CreateAlreadyLocalizedStr(
                protoID.ToString() + "_formatted",
                locStr.Format(monthlyUpointsCost.ToString()).Value
            );

            Option<EdictProto> previousTier = Option<EdictProto>.None;
            if (previusEdict.HasValue)
            {
                previousTier = registrator.PrototypesDb.GetOrThrow<EdictProto>(previusEdict.Value);
            }

            registrator.PrototypesDb.Add(new EdictWithPropertiesProto(
                protoID,
                Proto.CreateStr(protoID, "Unity Plus T" + countUnityPointsEdicts.ToString(), descShort, translationComment),
                (cheat ? categoryCheats : category),
                (-monthlyUpointsCost).Upoints(),
                ImmutableArray.Create(Make.Kvp(IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, 0.Percent())),
                previousTier,
                new EdictProto.Gfx("Assets/Base/Icons/Edicts/UnityIncreased.svg"))
            );
        }
    }
}
