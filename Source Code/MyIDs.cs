using Mafi.Base;
using Mafi.Core.Entities.Static;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Factory.Recipes;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using Mafi.Core.Research;

namespace CoI.Mod.Better.MyIDs
{

    public sealed class Products
    {
        public readonly static ProductProto.ID Nothing = new ProductProto.ID("NothingProduct");
    }

    public sealed class Research
    {
        public readonly static ResearchNodeProto.ID GenerellEdictsResearchT1 = new ResearchNodeProto.ID("MyGenerellEdictsResearchT1");
        public readonly static ResearchNodeProto.ID GenerellEdictsResearchT2 = new ResearchNodeProto.ID("MyGenerellEdictsResearchT2");
        public readonly static ResearchNodeProto.ID GenerellEdictsResearchT3 = new ResearchNodeProto.ID("MyGenerellEdictsResearchT3");
        public readonly static ResearchNodeProto.ID GenerellEdictsResearchT4 = new ResearchNodeProto.ID("MyGenerellEdictsResearchT4");
        public readonly static ResearchNodeProto.ID GenerellEdictsResearchT5 = new ResearchNodeProto.ID("MyGenerellEdictsResearchT5");

        public readonly static ResearchNodeProto.ID GenerellEdictsResearchCheat_T1 = new ResearchNodeProto.ID("MyGenerellEdictsResearchCheat");
        public readonly static ResearchNodeProto.ID GenerellEdictsResearchCheat_T2 = new ResearchNodeProto.ID("MyGenerellEdictsResearchCheatT2");
        public readonly static ResearchNodeProto.ID GenerellEdictsResearchCheat_T3 = new ResearchNodeProto.ID("MyGenerellEdictsResearchCheatT3");
        public readonly static ResearchNodeProto.ID GenerellEdictsResearchCheat_T4 = new ResearchNodeProto.ID("MyGenerellEdictsResearchCheatT4");
        public readonly static ResearchNodeProto.ID GenerellEdictsResearchCheat_T5 = new ResearchNodeProto.ID("MyGenerellEdictsResearchCheatT5");

        public readonly static ResearchNodeProto.ID VehicleEdictsResearchT1 = new ResearchNodeProto.ID("MyEdictsResearchT1");
        public readonly static ResearchNodeProto.ID VehicleEdictsResearchT2 = new ResearchNodeProto.ID("MyEdictsResearchT2");
        public readonly static ResearchNodeProto.ID VehicleEdictsResearchT3 = new ResearchNodeProto.ID("MyEdictsResearchT3");
        public readonly static ResearchNodeProto.ID VehicleEdictsResearchT4 = new ResearchNodeProto.ID("MyEdictsResearchT4");
        public readonly static ResearchNodeProto.ID VehicleEdictsResearchT5 = new ResearchNodeProto.ID("MyEdictsResearchT5");

        public readonly static ResearchNodeProto.ID VehicleEdictsResearchCheat_T1 = new ResearchNodeProto.ID("MyEdictsResearchCheat");
        public readonly static ResearchNodeProto.ID VehicleEdictsResearchCheat_T2 = new ResearchNodeProto.ID("MyEdictsResearchCheatT2");
        public readonly static ResearchNodeProto.ID VehicleEdictsResearchCheat_T3 = new ResearchNodeProto.ID("MyEdictsResearchCheatT3");
        public readonly static ResearchNodeProto.ID VehicleEdictsResearchCheat_T4 = new ResearchNodeProto.ID("MyEdictsResearchCheatT4");
        public readonly static ResearchNodeProto.ID VehicleEdictsResearchCheat_T5 = new ResearchNodeProto.ID("MyEdictsResearchCheatT5");

        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_ZERO = new ResearchNodeProto.ID("MyVehicle_Cap_Increase_ZERO");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_CHEAT = new ResearchNodeProto.ID("CHEAT_Vehicle_Cap_Increase_CHEAT");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_I = new ResearchNodeProto.ID("MyVehicle_Cap_Increase_I");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_II = new ResearchNodeProto.ID("MyVehicle_Cap_Increase_II");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_III = new ResearchNodeProto.ID("MyVehicle_Cap_Increase_III");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_IV = new ResearchNodeProto.ID("MyVehicle_Cap_Increase_IV");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_V = new ResearchNodeProto.ID("MyVehicle_Cap_Increase_V");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_VI = new ResearchNodeProto.ID("MyVehicle_Cap_Increase_VI");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_VII = new ResearchNodeProto.ID("MyVehicle_Cap_Increase_VII");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_VIII = new ResearchNodeProto.ID("MyVehicle_Cap_Increase_VIII");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_IX = new ResearchNodeProto.ID("MyVehicle_Cap_Increase_IX");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_X = new ResearchNodeProto.ID("MyVehicle_Cap_Increase_X");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_XI = new ResearchNodeProto.ID("MyVehicle_Cap_Increase_XI");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_XII = new ResearchNodeProto.ID("MyVehicle_Cap_Increase_XII");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_XIII = new ResearchNodeProto.ID("MyVehicle_Cap_Increase_XIII");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_XIV = new ResearchNodeProto.ID("MyVehicle_Cap_Increase_XIV");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_XV = new ResearchNodeProto.ID("MyVehicle_Cap_Increase_XV");

        public readonly static ResearchNodeProto.ID VoidCrusher = new ResearchNodeProto.ID("ResearchVoidCrusher");
        public readonly static ResearchNodeProto.ID VoidCrusherCheat = new ResearchNodeProto.ID("ResearchVoidCrusherCheat");
        public readonly static ResearchNodeProto.ID VoidCrusherRecyclablesCheat = new ResearchNodeProto.ID("ResearchVoidCrusherRecyclablesCheat");
        public readonly static ResearchNodeProto.ID VoidCrusherFluidCheat = new ResearchNodeProto.ID("ResearchVoidCrusherFluidCheat");
        public readonly static ResearchNodeProto.ID VoidCrusherLooseCheat = new ResearchNodeProto.ID("ResearchVoidCrusherLooseCheat");
        public readonly static ResearchNodeProto.ID VoidProducerCheat = new ResearchNodeProto.ID("ResearchVoidProducerCheat");
        public readonly static ResearchNodeProto.ID VoidDieselEnergyCheat = new ResearchNodeProto.ID("VoidEnergyCheat");
        public readonly static ResearchNodeProto.ID VoidPowerEnergyCheat = new ResearchNodeProto.ID("VoidPowerEnergyCheat");

        public readonly static ResearchNodeProto.ID StorageResearchT1 = new ResearchNodeProto.ID("MyStorageResearchT1");
        public readonly static ResearchNodeProto.ID StorageResearchT2 = new ResearchNodeProto.ID("MyStorageResearchT2");
        public readonly static ResearchNodeProto.ID StorageResearchT3 = new ResearchNodeProto.ID("MyStorageResearchT3");
        public readonly static ResearchNodeProto.ID StorageResearchT4 = new ResearchNodeProto.ID("MyStorageResearchT4");
        public readonly static ResearchNodeProto.ID StorageResearchT5 = new ResearchNodeProto.ID("MyStorageResearchT5");

    }

    public static class ToolbarCategories
    {
        public static readonly Proto.ID Storages = new Proto.ID("Better_Mod_Storages");
        public static readonly Proto.ID MachinesElectricity = new Proto.ID("Better_Mod_Electricity");
        public static readonly Proto.ID MachinesMetallurgy = new Proto.ID("Better_Mod_Metallurgy");
    }

    public sealed class Buildings
    {
        public static readonly StaticEntityProto.ID MineTowerNormal = new StaticEntityProto.ID("MyMineTowerNormal");
        public static readonly StaticEntityProto.ID MineTowerT2 = new StaticEntityProto.ID("MyMineTowerT2");
        public static readonly StaticEntityProto.ID MineTowerT3 = new StaticEntityProto.ID("MyMineTowerT3");

        public static readonly StaticEntityProto.ID StorageUnitT4 = new StaticEntityProto.ID("MyStorageUnitT4");
        public static readonly StaticEntityProto.ID StorageUnitT3 = new StaticEntityProto.ID("MyStorageUnitT3");
        public static readonly StaticEntityProto.ID StorageUnitT2 = new StaticEntityProto.ID("MyStorageUnitT2");
        public static readonly StaticEntityProto.ID StorageUnitT1 = new StaticEntityProto.ID("MyStorageUnitT1");

        public static readonly StaticEntityProto.ID StorageLooseT4 = new StaticEntityProto.ID("MyStorageLooseT4");
        public static readonly StaticEntityProto.ID StorageLooseT3 = new StaticEntityProto.ID("MyStorageLooseT3");
        public static readonly StaticEntityProto.ID StorageLooseT2 = new StaticEntityProto.ID("MyStorageLooseT2");
        public static readonly StaticEntityProto.ID StorageLooseT1 = new StaticEntityProto.ID("MyStorageLooseT1");

        public static readonly StaticEntityProto.ID StorageFluidT4 = new StaticEntityProto.ID("MyStorageFluidT4");
        public static readonly StaticEntityProto.ID StorageFluidT3 = new StaticEntityProto.ID("MyStorageFluidT3");
        public static readonly StaticEntityProto.ID StorageFluidT2 = new StaticEntityProto.ID("MyStorageFluidT2");
        public static readonly StaticEntityProto.ID StorageFluidT1 = new StaticEntityProto.ID("MyStorageFluidT1");

        public static readonly StaticEntityProto.ID NuclearWasteStorage = new StaticEntityProto.ID("MyNuclearWasteStorage");
    }

    public sealed class Machines
    {
        public readonly static MachineProto.ID VoidCrusher = new MachineProto.ID("VoidCrusher");
        public readonly static MachineProto.ID VoidCrusherCheat = new MachineProto.ID("VoidCrusherCheat");
        public readonly static MachineProto.ID VoidCrusherLooseCheat = new MachineProto.ID("VoidCrusherLooseCheat");
        public readonly static MachineProto.ID VoidCrusherFluidCheat = new MachineProto.ID("VoidCrusherFluidCheat");
        public readonly static MachineProto.ID VoidCrusherRecyclablesCheat = new MachineProto.ID("VoidCrusherRecyclablesCheat");
        public readonly static MachineProto.ID VoidProducerFluidCheat = new MachineProto.ID("VoidProducerLiquidCheat");
        public readonly static MachineProto.ID VoidProducerLooseCheat = new MachineProto.ID("VoidProducerLooseCheat");
        public readonly static MachineProto.ID VoidProducerProductCheat = new MachineProto.ID("VoidProducerProductCheat");


        public readonly static StaticEntityProto.ID VoidDieselEnergy10Cheat = new StaticEntityProto.ID("VoidProducerEnergy10Cheat");
        public readonly static StaticEntityProto.ID VoidDieselEnergy50Cheat = new StaticEntityProto.ID("VoidProducerEnergy50Cheat");
        public readonly static StaticEntityProto.ID VoidDieselEnergy100Cheat = new StaticEntityProto.ID("VoidProducerEnergy100Cheat");
        public readonly static StaticEntityProto.ID VoidDieselEnergy200Cheat = new StaticEntityProto.ID("VoidProducerEnergy200Cheat");
        public readonly static StaticEntityProto.ID VoidDieselEnergy1000Cheat = new StaticEntityProto.ID("VoidProducerEnergy1000Cheat");


        public readonly static StaticEntityProto.ID VoidPowerEnergyT1Cheat = new StaticEntityProto.ID("VoidPowerEnergyT1Cheat");
        public readonly static StaticEntityProto.ID VoidPowerEnergyT2Cheat = new StaticEntityProto.ID("VoidPowerEnergyT2Cheat");
        public readonly static StaticEntityProto.ID VoidPowerEnergyT3Cheat = new StaticEntityProto.ID("VoidPowerEnergyT3Cheat");
        public readonly static StaticEntityProto.ID VoidPowerEnergyT4Cheat = new StaticEntityProto.ID("VoidPowerEnergyT4Cheat");
        public readonly static StaticEntityProto.ID VoidPowerEnergyT5Cheat = new StaticEntityProto.ID("VoidPowerEnergyT5Cheat");
    }

    public sealed class Eticts
    {
        public static readonly Proto.ID BetterMod = new ProductProto.ID("BetterModCatergory");
        public static readonly Proto.ID BetterModCheats = new ProductProto.ID("BetterModCheatCatergory");

        public sealed class Trucks
        {
            public readonly static ResearchNodeProto.ID CapacityIncT2 = new ResearchNodeProto.ID(Utilities.CapacityIncStr + "2");
            public readonly static ResearchNodeProto.ID CapacityIncT3 = new ResearchNodeProto.ID(Utilities.CapacityIncStr + "3");
            public readonly static ResearchNodeProto.ID CapacityIncT4 = new ResearchNodeProto.ID(Utilities.CapacityIncStr + "4");
            public readonly static ResearchNodeProto.ID CapacityIncT5 = new ResearchNodeProto.ID(Utilities.CapacityIncStr + "5");

            public readonly static ResearchNodeProto.ID CapacityIncT1_CHEAT = new ResearchNodeProto.ID(Utilities.CapacityIncStr + Utilities.Cheat_1);
            public readonly static ResearchNodeProto.ID CapacityIncT2_CHEAT = new ResearchNodeProto.ID(Utilities.CapacityIncStr + Utilities.Cheat_2);
            public readonly static ResearchNodeProto.ID CapacityIncT3_CHEAT = new ResearchNodeProto.ID(Utilities.CapacityIncStr + Utilities.Cheat_3);
            public readonly static ResearchNodeProto.ID CapacityIncT4_CHEAT = new ResearchNodeProto.ID(Utilities.CapacityIncStr + Utilities.Cheat_4);
            public readonly static ResearchNodeProto.ID CapacityIncT5_CHEAT = new ResearchNodeProto.ID(Utilities.CapacityIncStr + Utilities.Cheat_5);



            public readonly static ResearchNodeProto.ID FuelReductionT2 = new ResearchNodeProto.ID(Utilities.FuelReductionStr + "2");
            public readonly static ResearchNodeProto.ID FuelReductionT3 = new ResearchNodeProto.ID(Utilities.FuelReductionStr + "3");
            public readonly static ResearchNodeProto.ID FuelReductionT4 = new ResearchNodeProto.ID(Utilities.FuelReductionStr + "4");
            public readonly static ResearchNodeProto.ID FuelReductionT5 = new ResearchNodeProto.ID(Utilities.FuelReductionStr + "5");

            public readonly static ResearchNodeProto.ID FuelReductionT1_CHEAT = new ResearchNodeProto.ID(Utilities.FuelReductionStr + Utilities.Cheat_1);
            public readonly static ResearchNodeProto.ID FuelReductionT2_CHEAT = new ResearchNodeProto.ID(Utilities.FuelReductionStr + Utilities.Cheat_2);
            public readonly static ResearchNodeProto.ID FuelReductionT3_CHEAT = new ResearchNodeProto.ID(Utilities.FuelReductionStr + Utilities.Cheat_3);
            public readonly static ResearchNodeProto.ID FuelReductionT4_CHEAT = new ResearchNodeProto.ID(Utilities.FuelReductionStr + Utilities.Cheat_4);



            public readonly static ResearchNodeProto.ID MaintenanceReductionT2 = new ResearchNodeProto.ID(Utilities.MaintenanceReductionStr + "2");
            public readonly static ResearchNodeProto.ID MaintenanceReductionT3 = new ResearchNodeProto.ID(Utilities.MaintenanceReductionStr + "3");
            public readonly static ResearchNodeProto.ID MaintenanceReductionT4 = new ResearchNodeProto.ID(Utilities.MaintenanceReductionStr + "4");
            public readonly static ResearchNodeProto.ID MaintenanceReductionT5 = new ResearchNodeProto.ID(Utilities.MaintenanceReductionStr + "5");

            public readonly static ResearchNodeProto.ID MaintenanceReductionT1_CHEAT = new ResearchNodeProto.ID(Utilities.MaintenanceReductionStr + Utilities.Cheat_1);
            public readonly static ResearchNodeProto.ID MaintenanceReductionT2_CHEAT = new ResearchNodeProto.ID(Utilities.MaintenanceReductionStr + Utilities.Cheat_2);
            public readonly static ResearchNodeProto.ID MaintenanceReductionT3_CHEAT = new ResearchNodeProto.ID(Utilities.MaintenanceReductionStr + Utilities.Cheat_3);
            public readonly static ResearchNodeProto.ID MaintenanceReductionT4_CHEAT = new ResearchNodeProto.ID(Utilities.MaintenanceReductionStr + Utilities.Cheat_4);
        }

        public sealed class Generell
        {
            public readonly static Proto.ID UnityPointsT1 = new Proto.ID(Utilities.UnityPlusStr + "1");
            public readonly static Proto.ID UnityPointsT2 = new Proto.ID(Utilities.UnityPlusStr + "2");
            public readonly static Proto.ID UnityPointsT3 = new Proto.ID(Utilities.UnityPlusStr + "3");
            public readonly static Proto.ID UnityPointsT4 = new Proto.ID(Utilities.UnityPlusStr + "4");
            public readonly static Proto.ID UnityPointsT5 = new Proto.ID(Utilities.UnityPlusStr + "5");

            public readonly static Proto.ID UnityPointsT1_CHEAT = new Proto.ID(Utilities.UnityPlusStr + Utilities.Cheat_1);
            public readonly static Proto.ID UnityPointsT2_CHEAT = new Proto.ID(Utilities.UnityPlusStr + Utilities.Cheat_2);
            public readonly static Proto.ID UnityPointsT3_CHEAT = new Proto.ID(Utilities.UnityPlusStr + Utilities.Cheat_3);
            public readonly static Proto.ID UnityPointsT4_CHEAT = new Proto.ID(Utilities.UnityPlusStr + Utilities.Cheat_4);
            public readonly static Proto.ID UnityPointsT5_CHEAT = new Proto.ID(Utilities.UnityPlusStr + Utilities.Cheat_5);

            public readonly static Proto.ID ReduceServiceT1_CHEAT = new Proto.ID(Utilities.ReduceServiceStr + Utilities.Cheat_1);
            public readonly static Proto.ID ReduceServiceT2_CHEAT = new Proto.ID(Utilities.ReduceServiceStr + Utilities.Cheat_2);
            public readonly static Proto.ID ReduceServiceT3_CHEAT = new Proto.ID(Utilities.ReduceServiceStr + Utilities.Cheat_3);
            public readonly static Proto.ID ReduceServiceT4_CHEAT = new Proto.ID(Utilities.ReduceServiceStr + Utilities.Cheat_4);
            public readonly static Proto.ID ReduceServiceT5_CHEAT = new Proto.ID(Utilities.ReduceServiceStr + Utilities.Cheat_5);
        }
    }

    public sealed class Utilities
    {
        public readonly static Proto.ID BeaconSchedule = new Proto.ID("BeaconSchedule");

        public readonly static string CapacityIncStr = "MyTruckCapacityIncreaseT";
        public readonly static string FuelReductionStr = "MyFuelReductionT";
        public readonly static string MaintenanceReductionStr = "MyMaintenanceReductionT";
        public readonly static string UnityPlusStr = "MyUnityPlusT";
        public readonly static string ReduceServiceStr = "MyReduceServiceT";

        public readonly static string Cheat_1 = "CHEAT1";
        public readonly static string Cheat_2 = "CHEAT2";
        public readonly static string Cheat_3 = "CHEAT3";
        public readonly static string Cheat_4 = "CHEAT4";
        public readonly static string Cheat_5 = "CHEAT5";
    }
}
