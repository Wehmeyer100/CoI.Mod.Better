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
        private int countTruckCapEdicts = 1;
        private int countTruckFuelConsEdicts = 1;
        private int countMaintenanceEdicts = 1;

        private readonly string translationComment = "policy / edict which can enabled by the player in their Captain's office.";
        private bool DisableCheats = true;
        private float CheatUpkeepEdicts = -0.5f;

        public void RegisterData(ProtoRegistrator registrator)
        {
            if (BetterMod.Config.DisableVehicleEdicts) return;

            DisableCheats = BetterMod.Config.DisableCheats;
            CheatUpkeepEdicts = BetterMod.Config.CheatUpkeepEdicts;

            AddTruckCap(registrator);
            AddTruckFuelCons(registrator);
            AddMaintenance(registrator);

            GenerateResearch(registrator);
        }

        private void GenerateResearch(ProtoRegistrator registrator)
        {
            // Generate Research T1
            ResearchNodeProtoBuilder.State research_state_t1 = registrator.ResearchNodeProtoBuilder
                .Start("Vehicle Edict Plus", MyIDs.Research.VehicleEdictsResearchT1)
                .SetCosts(BetterMod.Config.VehicleEdictsResearchCostT1)
                .AddEdictToUnlock(
                    MyIDs.Eticts.Trucks.CapacityIncT2,
                    MyIDs.Eticts.Trucks.FuelReductionT2,
                    MyIDs.Eticts.Trucks.MaintenanceReductionT2)
                .AddParent(Ids.Research.CaptainsOffice);

            ResearchNodeProto research_t1 = research_state_t1.BuildAndAdd();

            // Add parent to my research T1
            ResearchNodeProto master_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.CaptainsOffice);
            research_t1.AddParentPlusGridPos(master_research, BetterMod.UI_StepSize, (BetterMod.UI_StepSize * 3));


            // Generate Research T2
            ResearchNodeProtoBuilder.State research_state_t2 = registrator.ResearchNodeProtoBuilder
                .Start("Vehicle Edict Plus II", MyIDs.Research.VehicleEdictsResearchT2)
                .SetCosts(BetterMod.Config.VehicleEdictsResearchCostT2)
                .AddEdictToUnlock(
                    MyIDs.Eticts.Trucks.CapacityIncT3,
                    MyIDs.Eticts.Trucks.FuelReductionT3,
                    MyIDs.Eticts.Trucks.MaintenanceReductionT3);

            ResearchNodeProto research_t2 = research_state_t2.BuildAndAdd();
            // Add parent to my research T2
            research_t2.AddParentPlusGridPos(research_t1);



            // Generate Research T3
            ResearchNodeProtoBuilder.State research_state_t3 = registrator.ResearchNodeProtoBuilder
               .Start("Vehicle Edict Plus III", MyIDs.Research.VehicleEdictsResearchT3)
               .SetCosts(BetterMod.Config.VehicleEdictsResearchCostT3)
               .AddEdictToUnlock(
                    MyIDs.Eticts.Trucks.CapacityIncT4,
                    MyIDs.Eticts.Trucks.FuelReductionT4,
                    MyIDs.Eticts.Trucks.MaintenanceReductionT4);

            ResearchNodeProto research_t3 = research_state_t3.BuildAndAdd();

            // Add parent to my research T3
            research_t3.AddParentPlusGridPos(research_t2);



            // Generate Research T4
            ResearchNodeProtoBuilder.State research_state_t4 = registrator.ResearchNodeProtoBuilder
               .Start("Vehicle Edict Plus IV", MyIDs.Research.VehicleEdictsResearchT4)
               .SetCosts(BetterMod.Config.VehicleEdictsResearchCostT4)
               .AddEdictToUnlock(
                    MyIDs.Eticts.Trucks.CapacityIncT5,
                    MyIDs.Eticts.Trucks.FuelReductionT5,
                    MyIDs.Eticts.Trucks.MaintenanceReductionT5);

            ResearchNodeProto research_t4 = research_state_t4.BuildAndAdd();

            // Add parent to my research T4
            research_t4.AddParentPlusGridPos(research_t3);



            // Generate Research T5
            ResearchNodeProtoBuilder.State research_state_t5 = registrator.ResearchNodeProtoBuilder
               .Start("Vehicle Edict Plus V", MyIDs.Research.VehicleEdictsResearchT5)
               .SetCosts(BetterMod.Config.VehicleEdictsResearchCostT5);

            ResearchNodeProto research_t5 = research_state_t5.BuildAndAdd();

            // Add parent to my research T5
            research_t5.AddParentPlusGridPos(research_t4);

            Debug.Log("VehicleEdicts >> Vehicle Edict created!");

            if (!DisableCheats)
            {
                Cheats(registrator);
            }
        }
    }
}
