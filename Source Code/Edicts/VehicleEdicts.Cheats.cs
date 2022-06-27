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
using static CoI.Mod.Better.Utilities.ResearchProtoUtility;

namespace CoI.Mod.Better.Edicts
{
    internal partial class VehicleEdicts : IModData
    {
        private static void Cheats(ProtoRegistrator registrator)
        {
            ResearchNodeProto master_cheat_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.VehicleCapIncreaseID_CHEAT);

            ResearchNodeProto research_cheat_t1 = ResearchProtoUtility.GenerateResearchEdict(registrator, MyIDs.Research.VehicleEdictsResearchCheat_T1, "Vehicle Edict I Cheat", BetterMod.Config.Default.CheatResearchCosts, true, new ResearchNodeUIData(master_cheat_research, true, BetterMod.UI_StepSize * 2, 0), MyIDs.Eticts.Trucks.CapacityIncT1_CHEAT, MyIDs.Eticts.Trucks.FuelReductionT1_CHEAT, MyIDs.Eticts.Trucks.MaintenanceReductionT1_CHEAT);
            ResearchNodeProto research_cheat_t2 = ResearchProtoUtility.GenerateResearchEdict(registrator, MyIDs.Research.VehicleEdictsResearchCheat_T2, "Vehicle Edict II Cheat", BetterMod.Config.Default.CheatResearchCosts, true, research_cheat_t1, true, MyIDs.Eticts.Trucks.CapacityIncT2_CHEAT, MyIDs.Eticts.Trucks.FuelReductionT2_CHEAT, MyIDs.Eticts.Trucks.MaintenanceReductionT2_CHEAT);
            ResearchNodeProto research_cheat_t3 = ResearchProtoUtility.GenerateResearchEdict(registrator, MyIDs.Research.VehicleEdictsResearchCheat_T3, "Vehicle Edict III Cheat", BetterMod.Config.Default.CheatResearchCosts, true, research_cheat_t2, true, MyIDs.Eticts.Trucks.CapacityIncT3_CHEAT, MyIDs.Eticts.Trucks.FuelReductionT3_CHEAT, MyIDs.Eticts.Trucks.MaintenanceReductionT3_CHEAT);
            ResearchNodeProto research_cheat_t4 = ResearchProtoUtility.GenerateResearchEdict(registrator, MyIDs.Research.VehicleEdictsResearchCheat_T4, "Vehicle Edict IV Cheat", BetterMod.Config.Default.CheatResearchCosts, true, research_cheat_t3, true, MyIDs.Eticts.Trucks.CapacityIncT4_CHEAT, MyIDs.Eticts.Trucks.FuelReductionT4_CHEAT, MyIDs.Eticts.Trucks.MaintenanceReductionT4_CHEAT);
            ResearchProtoUtility.GenerateResearchEdict(registrator, MyIDs.Research.VehicleEdictsResearchCheat_T5, "Vehicle Edict V Cheat", BetterMod.Config.Default.CheatResearchCosts, true, research_cheat_t4, true, MyIDs.Eticts.Trucks.CapacityIncT5_CHEAT);

            Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> VehicleEdicts >> Vehicle Edict cheats created!");
        }
    }
}
