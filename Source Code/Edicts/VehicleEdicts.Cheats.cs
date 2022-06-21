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
        private static void Cheats(ProtoRegistrator registrator)
        {
            // Generate Cheat Research
            ResearchNodeProtoBuilder.State research_state_cheat_t1 = registrator.ResearchNodeProtoBuilder
                .Start("Vehicle Edict Cheat", MyIDs.Research.VehicleEdictsResearchCheat_T1)
                .AddEdictToUnlock(
                    MyIDs.Eticts.Trucks.CapacityIncT1_CHEAT,
                    MyIDs.Eticts.Trucks.FuelReductionT1_CHEAT,
                    MyIDs.Eticts.Trucks.MaintenanceReductionT1_CHEAT);

            if (BetterMod.Config.Default.UnlockAllCheatsResearches)
            {
                research_state_cheat_t1.SetCostsFree();
            }
            else
            {
                research_state_cheat_t1.SetCostsOne();
            }
            ResearchNodeProto research_cheat_t1 = research_state_cheat_t1.BuildAndAdd();

            // Add parent to my research CHEAT
            ResearchNodeProto master_cheat_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.VehicleCapIncreaseID_CHEAT);
            research_cheat_t1.AddGridPos(master_cheat_research);


            // Generate Cheat Research
            ResearchNodeProtoBuilder.State research_state_cheat_t2 = registrator.ResearchNodeProtoBuilder
                .Start("Vehicle Edict II Cheat", MyIDs.Research.VehicleEdictsResearchCheat_T2)
                .AddEdictToUnlock(
                    MyIDs.Eticts.Trucks.CapacityIncT2_CHEAT,
                    MyIDs.Eticts.Trucks.FuelReductionT2_CHEAT,
                    MyIDs.Eticts.Trucks.MaintenanceReductionT2_CHEAT);

            if (BetterMod.Config.Default.UnlockAllCheatsResearches)
            {
                research_state_cheat_t2.SetCostsFree();
            }
            else
            {
                research_state_cheat_t2.SetCostsOne();
            }
            ResearchNodeProto research_cheat_t2 = research_state_cheat_t2.BuildAndAdd();
            research_cheat_t2.AddGridPos(research_cheat_t1);


            // Generate Cheat Research
            ResearchNodeProtoBuilder.State research_state_cheat_t3 = registrator.ResearchNodeProtoBuilder
                .Start("Vehicle Edict III Cheat", MyIDs.Research.VehicleEdictsResearchCheat_T3)
                .AddEdictToUnlock(
                    MyIDs.Eticts.Trucks.CapacityIncT3_CHEAT,
                    MyIDs.Eticts.Trucks.FuelReductionT3_CHEAT,
                    MyIDs.Eticts.Trucks.MaintenanceReductionT3_CHEAT);

            if (BetterMod.Config.Default.UnlockAllCheatsResearches)
            {
                research_state_cheat_t3.SetCostsFree();
            }
            else
            {
                research_state_cheat_t3.SetCostsOne();
            }

            ResearchNodeProto research_cheat_t3 = research_state_cheat_t3.BuildAndAdd();
            research_cheat_t3.AddGridPos(research_cheat_t2);

            // Generate Cheat Research
            ResearchNodeProtoBuilder.State research_state_cheat_t4 = registrator.ResearchNodeProtoBuilder
                .Start("Vehicle Edict IV Cheat", MyIDs.Research.VehicleEdictsResearchCheat_T4)
                .AddEdictToUnlock(
                    MyIDs.Eticts.Trucks.CapacityIncT4_CHEAT,
                    MyIDs.Eticts.Trucks.FuelReductionT4_CHEAT,
                    MyIDs.Eticts.Trucks.MaintenanceReductionT4_CHEAT);

            if (BetterMod.Config.Default.UnlockAllCheatsResearches)
            {
                research_state_cheat_t4.SetCostsFree();
            }
            else
            {
                research_state_cheat_t4.SetCostsOne();
            }

            ResearchNodeProto research_cheat_t4 = research_state_cheat_t4.BuildAndAdd();
            research_cheat_t4.AddGridPos(research_cheat_t3);

            // Generate Cheat Research
            ResearchNodeProtoBuilder.State research_state_cheat_t5 = registrator.ResearchNodeProtoBuilder
                .Start("Vehicle Edict V Cheat", MyIDs.Research.VehicleEdictsResearchCheat_T5)
                .AddEdictToUnlock(MyIDs.Eticts.Trucks.CapacityIncT5_CHEAT);


            if (BetterMod.Config.Default.UnlockAllCheatsResearches)
            {
                research_state_cheat_t5.SetCostsFree();
            }
            else
            {
                research_state_cheat_t5.SetCostsOne();
            }
            ResearchNodeProto research_cheat_t5 = research_state_cheat_t5.BuildAndAdd();
            research_cheat_t5.AddGridPos(research_cheat_t4);

            Debug.Log("VehicleEdicts >> Vehicle Edict cheats created!");
        }
    }
}
