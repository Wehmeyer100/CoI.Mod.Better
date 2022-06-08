using CoI.Mod.Better.Extensions;
using Mafi;
using Mafi.Base;
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

namespace CoI.Mod.Better.Edicts
{
    internal class VehicleEdicts : IModData
    {
        private int countTruckCapEdicts = 1;
        private int countTruckFuelConsEdicts = 1;
        private int countMaintenanceEdicts = 1;

        private readonly string translationComment = "policy / edict which can enabled by the player in their Captain's office.";
        private bool DisableCheats = true;
        private float CheatUpkeepEdicts = -0.5f;

        public void RegisterData(ProtoRegistrator registrator)
        {
            if (MoreRecipes.Config.DisableVehicleEdicts) return;

            DisableCheats = MoreRecipes.Config.DisableCheats;
            CheatUpkeepEdicts = MoreRecipes.Config.CheatUpkeepEdicts;

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
                .SetCostsWithDifficulty(MoreRecipes.Config.VehicleEdictsResearchCostT1)
                .AddEdictToUnlock(
                    MyIDs.Eticts.Trucks.CapacityIncT2,
                    MyIDs.Eticts.Trucks.FuelReductionT2,
                    MyIDs.Eticts.Trucks.MaintenanceReductionT2)
                .AddParent(Ids.Research.CaptainsOffice);

            ResearchNodeProto research_t1 = research_state_t1.BuildAndAdd();

            // Add parent to my research T1
            ResearchNodeProto master_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.CaptainsOffice);
            research_t1.AddParentPlusGridPos(master_research, MoreRecipes.UI_StepSize, (MoreRecipes.UI_StepSize * 3));


            // Generate Research T2
            ResearchNodeProtoBuilder.State research_state_t2 = registrator.ResearchNodeProtoBuilder
                .Start("Vehicle Edict Plus II", MyIDs.Research.VehicleEdictsResearchT2)
                .SetCostsWithDifficulty(MoreRecipes.Config.VehicleEdictsResearchCostT2)
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
               .SetCostsWithDifficulty(MoreRecipes.Config.VehicleEdictsResearchCostT3)
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
               .SetCostsWithDifficulty(MoreRecipes.Config.VehicleEdictsResearchCostT4)
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
               .SetCostsWithDifficulty(MoreRecipes.Config.VehicleEdictsResearchCostT5);

            ResearchNodeProto research_t5 = research_state_t5.BuildAndAdd();

            // Add parent to my research T5
            research_t5.AddParentPlusGridPos(research_t4);

            if (!DisableCheats)
            {
                Cheats(registrator);
            }
        }

        private static void Cheats(ProtoRegistrator registrator)
        {
            // Generate Cheat Research
            ResearchNodeProtoBuilder.State research_state_cheat_t1 = registrator.ResearchNodeProtoBuilder
                .Start("Vehicle Edict Cheat", MyIDs.Research.VehicleEdictsResearchCheat_T1)
                .SetCostsOne()
                .AddEdictToUnlock(
                    MyIDs.Eticts.Trucks.CapacityIncT1_CHEAT,
                    MyIDs.Eticts.Trucks.FuelReductionT1_CHEAT,
                    MyIDs.Eticts.Trucks.MaintenanceReductionT1_CHEAT);

            ResearchNodeProto research_cheat_t1 = research_state_cheat_t1.BuildAndAdd();

            // Add parent to my research CHEAT
            ResearchNodeProto master_cheat_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.VehicleCapIncreaseID_CHEAT);
            research_cheat_t1.AddGridPos(master_cheat_research);


            // Generate Cheat Research
            ResearchNodeProtoBuilder.State research_state_cheat_t2 = registrator.ResearchNodeProtoBuilder
                .Start("Vehicle Edict II Cheat", MyIDs.Research.VehicleEdictsResearchCheat_T2)
                .SetCostsOne()
                .AddEdictToUnlock(
                    MyIDs.Eticts.Trucks.CapacityIncT2_CHEAT,
                    MyIDs.Eticts.Trucks.FuelReductionT2_CHEAT,
                    MyIDs.Eticts.Trucks.MaintenanceReductionT2_CHEAT);

            ResearchNodeProto research_cheat_t2 = research_state_cheat_t2.BuildAndAdd();
            research_cheat_t2.AddGridPos(research_cheat_t1);


            // Generate Cheat Research
            ResearchNodeProtoBuilder.State research_state_cheat_t3 = registrator.ResearchNodeProtoBuilder
                .Start("Vehicle Edict III Cheat", MyIDs.Research.VehicleEdictsResearchCheat_T3)
                .SetCostsOne()
                .AddEdictToUnlock(
                    MyIDs.Eticts.Trucks.CapacityIncT3_CHEAT,
                    MyIDs.Eticts.Trucks.FuelReductionT3_CHEAT,
                    MyIDs.Eticts.Trucks.MaintenanceReductionT3_CHEAT);

            ResearchNodeProto research_cheat_t3 = research_state_cheat_t3.BuildAndAdd();
            research_cheat_t3.AddGridPos(research_cheat_t2);

            // Generate Cheat Research
            ResearchNodeProtoBuilder.State research_state_cheat_t4 = registrator.ResearchNodeProtoBuilder
                .Start("Vehicle Edict IV Cheat", MyIDs.Research.VehicleEdictsResearchCheat_T4)
                .SetCostsOne()
                .AddEdictToUnlock(
                    MyIDs.Eticts.Trucks.CapacityIncT4_CHEAT,
                    MyIDs.Eticts.Trucks.FuelReductionT4_CHEAT,
                    MyIDs.Eticts.Trucks.MaintenanceReductionT4_CHEAT);

            ResearchNodeProto research_cheat_t4 = research_state_cheat_t4.BuildAndAdd();
            research_cheat_t4.AddGridPos(research_cheat_t3);

            // Generate Cheat Research
            ResearchNodeProtoBuilder.State research_state_cheat_t5 = registrator.ResearchNodeProtoBuilder
                .Start("Vehicle Edict V Cheat", MyIDs.Research.VehicleEdictsResearchCheat_T5)
                .SetCostsOne()
                .AddEdictToUnlock(MyIDs.Eticts.Trucks.CapacityIncT5_CHEAT);

            ResearchNodeProto research_cheat_t5 = research_state_cheat_t5.BuildAndAdd();
            research_cheat_t5.AddGridPos(research_cheat_t4);
        }

        private void AddMaintenance(ProtoRegistrator registrator)
        {
            // Generate Edicts
            GenerateMaintenance(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT2, 30, 2f);
            GenerateMaintenance(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT3, 40, 2.7f);
            GenerateMaintenance(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT4, 50, 3.3f);
            GenerateMaintenance(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT5, 60, 4f);


            if (DisableCheats) return;

            // Add Cheats

            GenerateMaintenance(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT1_CHEAT, 30, CheatUpkeepEdicts, "CHEAT");
            GenerateMaintenance(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT2_CHEAT, 40, CheatUpkeepEdicts, "CHEAT");
            GenerateMaintenance(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT3_CHEAT, 50, CheatUpkeepEdicts, "CHEAT");
            GenerateMaintenance(registrator, MyIDs.Eticts.Trucks.MaintenanceReductionT4_CHEAT, 60, CheatUpkeepEdicts, "CHEAT");
        }

        private void GenerateMaintenance(ProtoRegistrator registrator, Proto.ID protoID, int maintenance, float monthlyUpointsCost, string title_add = "")
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

            registrator.PrototypesDb.Add(new MaintenanceReductionEdictProto(
                protoID,
                Proto.CreateStr(protoID, "Maintenance reducer T" + countMaintenanceEdicts.ToString() + title_add, descShort8, translationComment),
                monthlyUpointsCost.Upoints(),
                typeof(MaintenanceReductionEdict),
                maintenanceMultiplierReduction,
                new EdictProto.Gfx("Assets/Base/Icons/Edicts/MaintenanceReduced.svg"))
            );
        }

        private void AddTruckFuelCons(ProtoRegistrator registrator)
        {
            GenerateTruckFuelCons(registrator, MyIDs.Eticts.Trucks.FuelReductionT2, 30, 2f);
            GenerateTruckFuelCons(registrator, MyIDs.Eticts.Trucks.FuelReductionT3, 45, 3f);
            GenerateTruckFuelCons(registrator, MyIDs.Eticts.Trucks.FuelReductionT4, 60, 4f);
            GenerateTruckFuelCons(registrator, MyIDs.Eticts.Trucks.FuelReductionT5, 75, 5f);

            if (DisableCheats) return;

            // Add Cheats
            GenerateTruckFuelCons(registrator, MyIDs.Eticts.Trucks.FuelReductionT1_CHEAT, 25, CheatUpkeepEdicts, "CHEAT");
            GenerateTruckFuelCons(registrator, MyIDs.Eticts.Trucks.FuelReductionT2_CHEAT, 50, CheatUpkeepEdicts, "CHEAT");
            GenerateTruckFuelCons(registrator, MyIDs.Eticts.Trucks.FuelReductionT3_CHEAT, 70, CheatUpkeepEdicts, "CHEAT");
            GenerateTruckFuelCons(registrator, MyIDs.Eticts.Trucks.FuelReductionT4_CHEAT, 95, CheatUpkeepEdicts, "CHEAT");
        }

        private void GenerateTruckFuelCons(ProtoRegistrator registrator, Proto.ID protoID, int consume, float monthlyUpointsCost, string title_add = "")
        {
            countTruckFuelConsEdicts++;
            Percent fuelMultiplierReduction = consume.Percent();

            LocStr1 locStr3 = Loc.Str1(
                protoID.ToString() + "__desc",
                "Vehicles fuel consumption reduced by {0}",
                "policy / edict which can enabled by the player in their Captain's office. {0}=" + fuelMultiplierReduction + "%"
            );

            LocStr descShort3 = LocalizationManager.CreateAlreadyLocalizedStr(
                protoID.ToString() + "_formatted",
                locStr3.Format(fuelMultiplierReduction.ToString()).Value
            );

            registrator.PrototypesDb.Add(new FuelReductionEdictProto(
                protoID,
                Proto.CreateStr(protoID, "Fuel saver T" + countTruckFuelConsEdicts.ToString() + title_add, descShort3, translationComment),
                monthlyUpointsCost.Upoints(),
                typeof(FuelReductionEdict),
                fuelMultiplierReduction,
                new EdictProto.Gfx("Assets/Base/Icons/Edicts/FuelReduced.svg")
            ));
        }

        private void AddTruckCap(ProtoRegistrator registrator)
        {
            GenerateTruckCap(registrator, MyIDs.Eticts.Trucks.CapacityIncT2, 50, 25, 1);
            GenerateTruckCap(registrator, MyIDs.Eticts.Trucks.CapacityIncT3, 75, 37, 2);
            GenerateTruckCap(registrator, MyIDs.Eticts.Trucks.CapacityIncT4, 100, 50, 3);
            GenerateTruckCap(registrator, MyIDs.Eticts.Trucks.CapacityIncT5, 200, 100, 4);

            if (DisableCheats) return;

            // Add Cheats
            GenerateTruckCap(registrator, MyIDs.Eticts.Trucks.CapacityIncT1_CHEAT, 100, 0, CheatUpkeepEdicts, "CHEAT");
            GenerateTruckCap(registrator, MyIDs.Eticts.Trucks.CapacityIncT2_CHEAT, 200, 0, CheatUpkeepEdicts, "CHEAT");
            GenerateTruckCap(registrator, MyIDs.Eticts.Trucks.CapacityIncT3_CHEAT, 300, 0, CheatUpkeepEdicts, "CHEAT");
            GenerateTruckCap(registrator, MyIDs.Eticts.Trucks.CapacityIncT4_CHEAT, 400, 0, CheatUpkeepEdicts, "CHEAT");
            GenerateTruckCap(registrator, MyIDs.Eticts.Trucks.CapacityIncT5_CHEAT, 500, 0, CheatUpkeepEdicts, "CHEAT");
        }

        private void GenerateTruckCap(ProtoRegistrator registrator, Proto.ID protoID, int Capacity, int Maintenance, float monthlyUpointsCost, string title_add = "")
        {
            countTruckCapEdicts++;

            Percent trucksCapacityDiff = Capacity.Percent();
            Percent trucksMaintenanceDiff = Maintenance.Percent();

            LocStr2 locStr4 = Loc.Str2(
                protoID.ToString() + "__desc",
                "Trucks can get overloaded by {0} but they require extra {1} maintenance",
                "policy / edict which can enabled by the player in their Captain's office. {0}=" + Maintenance + "%"
            );

            LocStr descShort4 = LocalizationManager.CreateAlreadyLocalizedStr(
                protoID.ToString() + "_formatted",
                locStr4.Format(trucksCapacityDiff.ToString(), trucksMaintenanceDiff.ToString()).Value
            );

            registrator.PrototypesDb.Add(new TrucksCapacityIncreaseEdictProto(
                protoID,
                Proto.CreateStr(protoID, "Overloaded trucks T" + countTruckCapEdicts.ToString() + title_add, descShort4, translationComment),
                monthlyUpointsCost.Upoints(),
                typeof(TrucksCapacityIncreaseEdict),
                trucksCapacityDiff,
                trucksMaintenanceDiff,
                new EdictProto.Gfx("Assets/Base/Icons/Edicts/TrucksCapacity.svg")
            ));
        }
    }
}
