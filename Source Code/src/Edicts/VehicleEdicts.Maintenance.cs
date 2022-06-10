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
    internal partial class VehicleEdicts : IModData
    {

        private void AddMaintenance(ProtoRegistrator registrator)
        {
            // Generate Edicts
            GenerateMaintenance(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT2, 30, 2f, null);
            GenerateMaintenance(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT3, 40, 2.7f, MyIDs.Eticts.Trucks.MaintenanceReductionT2);
            GenerateMaintenance(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT4, 50, 3.3f, MyIDs.Eticts.Trucks.MaintenanceReductionT3);
            GenerateMaintenance(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT5, 60, 4f, MyIDs.Eticts.Trucks.MaintenanceReductionT4);


            if (!BetterMod.Config.Systems.Cheats) return;

            // Add Cheats

            GenerateMaintenance(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT1_CHEAT, 30, CheatUpkeepEdicts, null, true);
            GenerateMaintenance(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT2_CHEAT, 50, CheatUpkeepEdicts, MyIDs.Eticts.Trucks.MaintenanceReductionT1_CHEAT, true);
            GenerateMaintenance(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT3_CHEAT, 75, CheatUpkeepEdicts, MyIDs.Eticts.Trucks.MaintenanceReductionT2_CHEAT, true);
            GenerateMaintenance(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT4_CHEAT, 95, CheatUpkeepEdicts, MyIDs.Eticts.Trucks.MaintenanceReductionT3_CHEAT, true);
        }

        private void GenerateMaintenance(ProtoRegistrator registrator, Proto.ID protoID, int maintenance, float monthlyUpointsCost, Proto.ID? previusEdict, bool cheat = false)
        {
            countMaintenanceEdicts++;
            Percent maintenanceMultiplierReduction = maintenance.Percent();

            LocStr1 locStr8 = Loc.Str1(
                protoID.ToString() + "__desc",
                "Maintenance reduced by {0}",
                "policy / edict which can enabled by the player in their Captain's office. {0}=" + maintenanceMultiplierReduction + "%"
            );

            LocStr descShort8 = LocalizationManager.CreateAlreadyLocalizedStr(
                protoID.ToString() + "_formatted",
                locStr8.Format(maintenanceMultiplierReduction.ToString()).Value
            );

            Option<EdictProto> previousTier = Option<EdictProto>.None;
            if (previusEdict.HasValue)
            {
                previousTier = registrator.PrototypesDb.GetOrThrow<EdictProto>(previusEdict.Value);
            }

            registrator.PrototypesDb.Add(new EdictWithPropertiesProto(
                protoID,
                Proto.CreateStr(protoID, "Maintenance reducer T" + countMaintenanceEdicts.ToString(), descShort8, translationComment),
                (cheat ? GenerellEdicts.categoryCheats : GenerellEdicts.category),
                monthlyUpointsCost.Upoints(),
                ImmutableArray.Create(Make.Kvp(IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, -(maintenance).Percent())),
                previousTier,
                new EdictProto.Gfx("Assets/Base/Icons/Edicts/MaintenanceReduced.svg"))
            );
        }

    }
}
