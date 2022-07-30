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
        private void AddUnityPoints(ProtoRegistrator registrator)
        {
            // Add Cheats
            if (!BetterMod.Config.Systems.Cheats) return;

            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.UnityPointsT1_CHEAT, CategoryCheats, "unity_points_t1", -5, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, 0, null, Mafi.Base.Assets.Base.Icons.Edicts.UnityIncreased_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.UnityPointsT2_CHEAT, CategoryCheats, "unity_points_t2", -10, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, 0, MyIDs.Eticts.Generell.UnityPointsT1_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.UnityIncreased_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.UnityPointsT3_CHEAT, CategoryCheats, "unity_points_t3", -20, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, 0, MyIDs.Eticts.Generell.UnityPointsT2_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.UnityIncreased_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.UnityPointsT4_CHEAT, CategoryCheats, "unity_points_t4", -50, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, 0, MyIDs.Eticts.Generell.UnityPointsT3_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.UnityIncreased_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Generell.UnityPointsT5_CHEAT, CategoryCheats, "unity_points_t5", -100, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, 0, MyIDs.Eticts.Generell.UnityPointsT4_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.UnityIncreased_svg);
        }

    }
}
