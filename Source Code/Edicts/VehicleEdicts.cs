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
        private int countTruckCapEdicts = 1;
        private int countTruckFuelConsEdicts = 1;
        private int countMaintenanceEdicts = 1;

        private const string translationComment = "policy / edict which can enabled by the player in their Captain's office.";
        private float CheatUpkeepEdicts = -0.5f;

        public void RegisterData(ProtoRegistrator registrator)
        {
            if (!BetterMod.Config.Systems.VehicleEdicts) return;

            CheatUpkeepEdicts = BetterMod.Config.Default.CheatUpkeepEdicts;

            AddTruckCap(registrator);
            AddTruckFuelCons(registrator);
            AddMaintenance(registrator);

            GenerateResearch(registrator);
        }

        private void GenerateResearch(ProtoRegistrator registrator)
        {
            ResearchNodeProto master_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.CaptainsOffice);

            ResearchNodeProto research_t1 = ResearchProtoUtility.GenerateResearchEdict(registrator, MyIDs.Research.VehicleEdictsResearchT1, "Vehicle Edict Plus I", BetterMod.Config.VehicleEdicts.ResearchCostT1, true, new ResearchNodeUIData(master_research, false, BetterMod.UI_StepSize, (BetterMod.UI_StepSize * 5)), MyIDs.Eticts.Trucks.CapacityIncT1, MyIDs.Eticts.Trucks.FuelReductionT1, MyIDs.Eticts.Trucks.MaintenanceReductionT1);
            ResearchNodeProto research_t2 = ResearchProtoUtility.GenerateResearchEdict(registrator, MyIDs.Research.VehicleEdictsResearchT2, "Vehicle Edict Plus II", BetterMod.Config.VehicleEdicts.ResearchCostT2, true, research_t1, false, MyIDs.Eticts.Trucks.CapacityIncT2, MyIDs.Eticts.Trucks.FuelReductionT2, MyIDs.Eticts.Trucks.MaintenanceReductionT2);
            ResearchNodeProto research_t3 = ResearchProtoUtility.GenerateResearchEdict(registrator, MyIDs.Research.VehicleEdictsResearchT3, "Vehicle Edict Plus III", BetterMod.Config.VehicleEdicts.ResearchCostT3, true, research_t2, false, MyIDs.Eticts.Trucks.CapacityIncT3, MyIDs.Eticts.Trucks.FuelReductionT3, MyIDs.Eticts.Trucks.MaintenanceReductionT3);
            ResearchProtoUtility.GenerateResearchEdict(registrator, MyIDs.Research.VehicleEdictsResearchT4, "Vehicle Edict Plus IV", BetterMod.Config.VehicleEdicts.ResearchCostT4, true, research_t3, false, MyIDs.Eticts.Trucks.CapacityIncT4, MyIDs.Eticts.Trucks.FuelReductionT4, MyIDs.Eticts.Trucks.MaintenanceReductionT4);


            Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> VehicleEdicts >> Vehicle Edict created!");

            if (BetterMod.Config.Systems.Cheats)
            {
                Cheats(registrator);
            }
        }
    }
}
