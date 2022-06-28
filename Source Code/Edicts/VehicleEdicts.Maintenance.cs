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
    internal partial class VehicleEdicts : IModData
    {

        private void AddMaintenance(ProtoRegistrator registrator)
        {
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT1, GenerellEdicts.category, "maintenance_reduction_t1", 2, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, -30, null, Mafi.Base.Assets.Base.Icons.Edicts.MaintenanceReduced_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT2, GenerellEdicts.category, "maintenance_reduction_t2", 2.7f, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, -40, MyIDs.Eticts.Trucks.MaintenanceReductionT1, Mafi.Base.Assets.Base.Icons.Edicts.MaintenanceReduced_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT3, GenerellEdicts.category, "maintenance_reduction_t3", 3.3f, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, -50, MyIDs.Eticts.Trucks.MaintenanceReductionT2, Mafi.Base.Assets.Base.Icons.Edicts.MaintenanceReduced_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT4, GenerellEdicts.category, "maintenance_reduction_t4", 4, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, -60, MyIDs.Eticts.Trucks.MaintenanceReductionT3, Mafi.Base.Assets.Base.Icons.Edicts.MaintenanceReduced_svg);


            if (!BetterMod.Config.Systems.Cheats) return;

            // Add Cheats
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT1_CHEAT, GenerellEdicts.categoryCheats, "maintenance_reduction_t1_cheat", CheatUpkeepEdicts, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, -30, null, Mafi.Base.Assets.Base.Icons.Edicts.MaintenanceReduced_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT2_CHEAT, GenerellEdicts.categoryCheats, "maintenance_reduction_t2_cheat", CheatUpkeepEdicts, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, -50, MyIDs.Eticts.Trucks.MaintenanceReductionT1_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.MaintenanceReduced_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT3_CHEAT, GenerellEdicts.categoryCheats, "maintenance_reduction_t3_cheat", CheatUpkeepEdicts, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, -75, MyIDs.Eticts.Trucks.MaintenanceReductionT2_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.MaintenanceReduced_svg);
            EdictUtility.GenerateEdict2(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT4_CHEAT, GenerellEdicts.categoryCheats, "maintenance_reduction_t4_cheat", CheatUpkeepEdicts, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, -100, MyIDs.Eticts.Trucks.MaintenanceReductionT3_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.MaintenanceReduced_svg);

        }
    }
}
