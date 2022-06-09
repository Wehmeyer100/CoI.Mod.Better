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
        private void AddTruckCap(ProtoRegistrator registrator)
        {
            GenerateTruckCap(registrator, MyIDs.Eticts.Trucks.CapacityIncT2, 50, 1, null);
            GenerateTruckCap(registrator, MyIDs.Eticts.Trucks.CapacityIncT3, 75, 2, MyIDs.Eticts.Trucks.CapacityIncT2);
            GenerateTruckCap(registrator, MyIDs.Eticts.Trucks.CapacityIncT4, 100, 3, MyIDs.Eticts.Trucks.CapacityIncT3);
            GenerateTruckCap(registrator, MyIDs.Eticts.Trucks.CapacityIncT5, 200, 4, MyIDs.Eticts.Trucks.CapacityIncT4);

            if (DisableCheats) return;

            // Add Cheats
            GenerateTruckCap(registrator, MyIDs.Eticts.Trucks.CapacityIncT1_CHEAT, 100, CheatUpkeepEdicts, null, true);
            GenerateTruckCap(registrator, MyIDs.Eticts.Trucks.CapacityIncT2_CHEAT, 200, CheatUpkeepEdicts, MyIDs.Eticts.Trucks.CapacityIncT1_CHEAT, true);
            GenerateTruckCap(registrator, MyIDs.Eticts.Trucks.CapacityIncT3_CHEAT, 300, CheatUpkeepEdicts, MyIDs.Eticts.Trucks.CapacityIncT2_CHEAT, true);
            GenerateTruckCap(registrator, MyIDs.Eticts.Trucks.CapacityIncT4_CHEAT, 400, CheatUpkeepEdicts, MyIDs.Eticts.Trucks.CapacityIncT3_CHEAT, true);
            GenerateTruckCap(registrator, MyIDs.Eticts.Trucks.CapacityIncT5_CHEAT, 500, CheatUpkeepEdicts, MyIDs.Eticts.Trucks.CapacityIncT4_CHEAT, true);
        }

        private void GenerateTruckCap(ProtoRegistrator registrator, Proto.ID protoID, int Capacity, float monthlyUpointsCost, Proto.ID? previusEdict, bool cheat = false)
        {
            countTruckCapEdicts++;

            Percent trucksCapacityDiff = Capacity.Percent();

            LocStr2 locStr4 = Loc.Str2(
                protoID.ToString() + "__desc",
                "Trucks can get overloaded by {0} but they require extra {1} maintenance",
                "policy / edict which can enabled by the player in their Captain's office. {0}=" + trucksCapacityDiff + "%"
            );

            LocStr descShort4 = LocalizationManager.CreateAlreadyLocalizedStr(
                protoID.ToString() + "_formatted",
                locStr4.Format(trucksCapacityDiff.ToString(), trucksCapacityDiff.ToString()).Value
            );

            Option<EdictProto> previousTier = Option<EdictProto>.None;
            if (previusEdict.HasValue) 
            {
                previousTier = registrator.PrototypesDb.GetOrThrow<EdictProto>(previusEdict.Value);
            }

            registrator.PrototypesDb.Add(new EdictWithPropertiesProto(
                protoID,
                Proto.CreateStr(protoID, "Overloaded trucks T" + countTruckCapEdicts.ToString(), descShort4, translationComment),
                (cheat ? GenerellEdicts.categoryCheats : GenerellEdicts.category),
                monthlyUpointsCost.Upoints(),
                ImmutableArray.Create(Make.Kvp(IdsCore.PropertyIds.TrucksCapacityMultiplier, Capacity.Percent())),
                previousTier,
                new EdictProto.Gfx("Assets/Base/Icons/Edicts/TrucksCapacity.svg"))
            );
        }
    }
}
