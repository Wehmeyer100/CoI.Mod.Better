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

        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_ZERO = new ResearchNodeProto.ID("Vehicle_Cap_Increase_ZERO");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_CHEAT = new ResearchNodeProto.ID("CHEAT_Vehicle_Cap_Increase_CHEAT");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_I = new ResearchNodeProto.ID("Vehicle_Cap_Increase_I");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_II = new ResearchNodeProto.ID("Vehicle_Cap_Increase_II");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_III = new ResearchNodeProto.ID("Vehicle_Cap_Increase_III");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_IV = new ResearchNodeProto.ID("Vehicle_Cap_Increase_IV");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_V = new ResearchNodeProto.ID("Vehicle_Cap_Increase_V");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_VI = new ResearchNodeProto.ID("Vehicle_Cap_Increase_VI");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_VII = new ResearchNodeProto.ID("Vehicle_Cap_Increase_VII");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_VIII = new ResearchNodeProto.ID("Vehicle_Cap_Increase_VIII");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_IX = new ResearchNodeProto.ID("Vehicle_Cap_Increase_IX");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_X = new ResearchNodeProto.ID("Vehicle_Cap_Increase_X");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_XI = new ResearchNodeProto.ID("Vehicle_Cap_Increase_XI");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_XII = new ResearchNodeProto.ID("Vehicle_Cap_Increase_XII");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_XIII = new ResearchNodeProto.ID("Vehicle_Cap_Increase_XIII");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_XIV = new ResearchNodeProto.ID("Vehicle_Cap_Increase_XIV");
        public readonly static ResearchNodeProto.ID VehicleCapIncreaseID_XV = new ResearchNodeProto.ID("Vehicle_Cap_Increase_XV");

        public readonly static ResearchNodeProto.ID VoidCrusher = new ResearchNodeProto.ID("ResearchVoidCrusher");
        public readonly static ResearchNodeProto.ID VoidCrusherCheat = new ResearchNodeProto.ID("ResearchVoidCrusherCheat");
        public readonly static ResearchNodeProto.ID VoidProducerCheat = new ResearchNodeProto.ID("ResearchVoidProducerCheat");
        public readonly static ResearchNodeProto.ID VoidEnergyCheat = new ResearchNodeProto.ID("VoidEnergyCheat");

    }

    public sealed class Buildings
    {

    }

    public sealed class Machines
    {
        public readonly static MachineProto.ID VoidCrusher = new MachineProto.ID("VoidCrusher");
        public readonly static MachineProto.ID VoidCrusherCheat = new MachineProto.ID("VoidCrusherCheat");
        public readonly static MachineProto.ID VoidProducerLiquidCheat = new MachineProto.ID("VoidProducerLiquidCheat");
        public readonly static MachineProto.ID VoidProducerLooseCheat = new MachineProto.ID("VoidProducerLooseCheat");
        public readonly static MachineProto.ID VoidProducerProductCheat = new MachineProto.ID("VoidProducerProductCheat");


        public readonly static StaticEntityProto.ID VoidProducerEnergy10Cheat = new StaticEntityProto.ID("VoidProducerEnergy10Cheat");
        public readonly static StaticEntityProto.ID VoidProducerEnergy50Cheat = new StaticEntityProto.ID("VoidProducerEnergy50Cheat");
        public readonly static StaticEntityProto.ID VoidProducerEnergy100Cheat = new StaticEntityProto.ID("VoidProducerEnergy100Cheat");
        public readonly static StaticEntityProto.ID VoidProducerEnergy200Cheat = new StaticEntityProto.ID("VoidProducerEnergy200Cheat");
        public readonly static StaticEntityProto.ID VoidProducerEnergy1000Cheat = new StaticEntityProto.ID("VoidProducerEnergy1000Cheat");
    }

    public sealed class Eticts
    {
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

        public readonly static string CapacityIncStr = "TruckCapacityIncreaseT";
        public readonly static string FuelReductionStr = "FuelReductionT";
        public readonly static string MaintenanceReductionStr = "MaintenanceReductionT";
        public readonly static string UnityPlusStr = "UnityPlusT";
        public readonly static string ReduceServiceStr = "ReduceServiceT";

        public readonly static string Cheat_1 = "CHEAT1";
        public readonly static string Cheat_2 = "CHEAT2";
        public readonly static string Cheat_3 = "CHEAT3";
        public readonly static string Cheat_4 = "CHEAT4";
        public readonly static string Cheat_5 = "CHEAT5";
    }

    public sealed class Recipes
    {
        public readonly static VoidCrusher VoidCrusherIDs = new VoidCrusher();
        public readonly static VoidCrusher VoidCrusherCheatIDs = new VoidCrusher("_CHEAT");

        public sealed class VoidCrusher
        {
            public VoidCrusher(string addStr = "")
            {            
                 VoidDestroy_Cement = new RecipeProto.ID("VoidDestroy_Cement" + addStr);
                 VoidDestroy_Rubber = new RecipeProto.ID("VoidDestroy_Rubber" + addStr);
                 VoidDestroy_Wood = new RecipeProto.ID("VoidDestroy_Wood" + addStr);
                 VoidDestroy_Iron = new RecipeProto.ID("VoidDestroy_Iron" + addStr);
                 VoidDestroy_Steel = new RecipeProto.ID("VoidDestroy_Steel" + addStr);
                 VoidDestroy_Copper = new RecipeProto.ID("VoidDestroy_Copper" + addStr);
                 VoidDestroy_Aluminum = new RecipeProto.ID("VoidDestroy_Aluminum" + addStr);
                 VoidDestroy_Gold = new RecipeProto.ID("VoidDestroy_Gold" + addStr);
                 VoidDestroy_Glass = new RecipeProto.ID("VoidDestroy_Glass" + addStr);
                 VoidDestroy_PolySilicon = new RecipeProto.ID("VoidDestroy_PolySilicon" + addStr);
                 VoidDestroy_SiliconWafer = new RecipeProto.ID("VoidDestroy_SiliconWafer" + addStr);
                 VoidDestroy_Food = new RecipeProto.ID("VoidDestroy_Food" + addStr);
                 VoidDestroy_HouseholdGoods = new RecipeProto.ID("VoidDestroy_HouseholdGoods" + addStr);
                 VoidDestroy_HouseholdElectronics = new RecipeProto.ID("VoidDestroy_HouseholdElectronics" + addStr);
                 VoidDestroy_ConcreteSlab = new RecipeProto.ID("VoidDestroy_ConcreteSlab" + addStr);
                 VoidDestroy_ConstructionParts = new RecipeProto.ID("VoidDestroy_ConstructionParts" + addStr);
                 VoidDestroy_ConstructionParts2 = new RecipeProto.ID("VoidDestroy_ConstructionParts2" + addStr);
                 VoidDestroy_ConstructionParts3 = new RecipeProto.ID("VoidDestroy_ConstructionParts3" + addStr);
                 VoidDestroy_ConstructionParts4 = new RecipeProto.ID("VoidDestroy_ConstructionParts4" + addStr);
                 VoidDestroy_Electronics = new RecipeProto.ID("VoidDestroy_Electronics" + addStr);
                 VoidDestroy_Electronics2 = new RecipeProto.ID("VoidDestroy_Electronics2" + addStr);
                 VoidDestroy_Microchips = new RecipeProto.ID("VoidDestroy_Microchips" + addStr);
                 VoidDestroy_MicrochipsStage1A = new RecipeProto.ID("VoidDestroy_MicrochipsStage1A" + addStr);
                 VoidDestroy_MicrochipsStage1B = new RecipeProto.ID("VoidDestroy_MicrochipsStage1B" + addStr);
                 VoidDestroy_MicrochipsStage1C = new RecipeProto.ID("VoidDestroy_MicrochipsStage1C" + addStr);
                 VoidDestroy_MicrochipsStage2A = new RecipeProto.ID("VoidDestroy_MicrochipsStage2A" + addStr);
                 VoidDestroy_MicrochipsStage2B = new RecipeProto.ID("VoidDestroy_MicrochipsStage2B" + addStr);
                 VoidDestroy_MicrochipsStage2C = new RecipeProto.ID("VoidDestroy_MicrochipsStage2C" + addStr);
                 VoidDestroy_MicrochipsStage3A = new RecipeProto.ID("VoidDestroy_MicrochipsStage3A" + addStr);
                 VoidDestroy_MicrochipsStage3B = new RecipeProto.ID("VoidDestroy_MicrochipsStage3B" + addStr);
                 VoidDestroy_MicrochipsStage3C = new RecipeProto.ID("VoidDestroy_MicrochipsStage3C" + addStr);
                 VoidDestroy_MicrochipsStage4A = new RecipeProto.ID("VoidDestroy_MicrochipsStage4A" + addStr);
                 VoidDestroy_MicrochipsStage4B = new RecipeProto.ID("VoidDestroy_MicrochipsStage4B" + addStr);
                 VoidDestroy_Graphite = new RecipeProto.ID("VoidDestroy_Graphite" + addStr);
                 VoidDestroy_ImpureCopper = new RecipeProto.ID("VoidDestroy_ImpureCopper" + addStr);
                 VoidDestroy_UraniumPellets = new RecipeProto.ID("VoidDestroy_UraniumPellets" + addStr);
                 VoidDestroy_UraniumRod = new RecipeProto.ID("VoidDestroy_UraniumRod" + addStr);
        }


            public RecipeProto.ID VoidDestroy_Cement;
            public RecipeProto.ID VoidDestroy_Wood;
            public RecipeProto.ID VoidDestroy_Iron;
            public RecipeProto.ID VoidDestroy_Rubber;
            public RecipeProto.ID VoidDestroy_Steel;
            public RecipeProto.ID VoidDestroy_Copper;
            public RecipeProto.ID VoidDestroy_Aluminum;
            public RecipeProto.ID VoidDestroy_Gold;
            public RecipeProto.ID VoidDestroy_Glass;
            public RecipeProto.ID VoidDestroy_PolySilicon;
            public RecipeProto.ID VoidDestroy_SiliconWafer;
            public RecipeProto.ID VoidDestroy_Food;
            public RecipeProto.ID VoidDestroy_HouseholdGoods;
            public RecipeProto.ID VoidDestroy_HouseholdElectronics;
            public RecipeProto.ID VoidDestroy_ConcreteSlab ;
            public RecipeProto.ID VoidDestroy_ConstructionParts ;
            public RecipeProto.ID VoidDestroy_ConstructionParts2 ;
            public RecipeProto.ID VoidDestroy_ConstructionParts3;
            public RecipeProto.ID VoidDestroy_ConstructionParts4 ;
            public RecipeProto.ID VoidDestroy_Electronics ;
            public RecipeProto.ID VoidDestroy_Electronics2;
            public RecipeProto.ID VoidDestroy_Microchips ;
            public RecipeProto.ID VoidDestroy_MicrochipsStage1A;
            public RecipeProto.ID VoidDestroy_MicrochipsStage1B;
            public RecipeProto.ID VoidDestroy_MicrochipsStage1C;
            public RecipeProto.ID VoidDestroy_MicrochipsStage2A;
            public RecipeProto.ID VoidDestroy_MicrochipsStage2B;
            public RecipeProto.ID VoidDestroy_MicrochipsStage2C;
            public RecipeProto.ID VoidDestroy_MicrochipsStage3A;
            public RecipeProto.ID VoidDestroy_MicrochipsStage3B;
            public RecipeProto.ID VoidDestroy_MicrochipsStage3C;
            public RecipeProto.ID VoidDestroy_MicrochipsStage4A;
            public RecipeProto.ID VoidDestroy_MicrochipsStage4B;
            public RecipeProto.ID VoidDestroy_Graphite;
            public RecipeProto.ID VoidDestroy_ImpureCopper;
            public RecipeProto.ID VoidDestroy_UraniumPellets;
            public RecipeProto.ID VoidDestroy_UraniumRod;
        }


        public readonly static RecipeProto.ID VoidProducerLiquids_Fertilizer = new RecipeProto.ID("VoidProducerLiquids_Fertilizer");
        public readonly static RecipeProto.ID VoidProducerLiquids_Water = new RecipeProto.ID("VoidProducerLiquids_Water");
        public readonly static RecipeProto.ID VoidProducerLiquids_ChilledWater = new RecipeProto.ID("VoidProducerLiquids_ChilledWater");
        public readonly static RecipeProto.ID VoidProducerLiquids_Seawater = new RecipeProto.ID("VoidProducerLiquids_Seawater");
        public readonly static RecipeProto.ID VoidProducerLiquids_Brine = new RecipeProto.ID("VoidProducerLiquids_Brine");
        public readonly static RecipeProto.ID VoidProducerLiquids_WasteWater = new RecipeProto.ID("VoidProducerLiquids_WasteWater");
        public readonly static RecipeProto.ID VoidProducerLiquids_ToxicSlurry = new RecipeProto.ID("VoidProducerLiquids_ToxicSlurry");
        public readonly static RecipeProto.ID VoidProducerLiquids_Chlorine = new RecipeProto.ID("VoidProducerLiquids_Chlorine");
        public readonly static RecipeProto.ID VoidProducerLiquids_SteamHi = new RecipeProto.ID("VoidProducerLiquids_SteamHi");
        public readonly static RecipeProto.ID VoidProducerLiquids_SteamLo = new RecipeProto.ID("VoidProducerLiquids_SteamLo");
        public readonly static RecipeProto.ID VoidProducerLiquids_SteamDepleted = new RecipeProto.ID("VoidProducerLiquids_SteamDepleted");
        public readonly static RecipeProto.ID VoidProducerLiquids_Exhaust = new RecipeProto.ID("VoidProducerLiquids_Exhaust");
        public readonly static RecipeProto.ID VoidProducerLiquids_CrudeOil = new RecipeProto.ID("VoidProducerLiquids_CrudeOil");
        public readonly static RecipeProto.ID VoidProducerLiquids_Diesel = new RecipeProto.ID("VoidProducerLiquids_Diesel");
        public readonly static RecipeProto.ID VoidProducerLiquids_Naphtha = new RecipeProto.ID("VoidProducerLiquids_Naphtha");
        public readonly static RecipeProto.ID VoidProducerLiquids_FuelGas = new RecipeProto.ID("VoidProducerLiquids_FuelGas");
        public readonly static RecipeProto.ID VoidProducerLiquids_SourWater = new RecipeProto.ID("VoidProducerLiquids_SourWater");
        public readonly static RecipeProto.ID VoidProducerLiquids_Ammonia = new RecipeProto.ID("VoidProducerLiquids_Ammonia");
        public readonly static RecipeProto.ID VoidProducerLiquids_Acid = new RecipeProto.ID("VoidProducerLiquids_Acid");
        public readonly static RecipeProto.ID VoidProducerLiquids_HeavyOil = new RecipeProto.ID("VoidProducerLiquids_HeavyOil");
        public readonly static RecipeProto.ID VoidProducerLiquids_MediumOil = new RecipeProto.ID("VoidProducerLiquids_MediumOil");
        public readonly static RecipeProto.ID VoidProducerLiquids_LightOil = new RecipeProto.ID("VoidProducerLiquids_LightOil");
        public readonly static RecipeProto.ID VoidProducerLiquids_Hydrogen = new RecipeProto.ID("VoidProducerLiquids_Hydrogen");
        public readonly static RecipeProto.ID VoidProducerLiquids_Nitrogen = new RecipeProto.ID("VoidProducerLiquids_Nitrogen");
        public readonly static RecipeProto.ID VoidProducerLiquids_CarbonDioxide = new RecipeProto.ID("VoidProducerLiquids_CarbonDioxide");
        public readonly static RecipeProto.ID VoidProducerLiquids_Oxygen = new RecipeProto.ID("VoidProducerLiquids_Oxygen");


        public readonly static RecipeProto.ID VoidProducerProdcuts_Cement = new RecipeProto.ID("VoidProducerProdcuts_Cement");
        public readonly static RecipeProto.ID VoidProducerProdcuts_Wood = new RecipeProto.ID("VoidProducerProdcuts_Wood");
        public readonly static RecipeProto.ID VoidProducerProdcuts_Iron = new RecipeProto.ID("VoidProducerProdcuts_Iron");
        public readonly static RecipeProto.ID VoidProducerProdcuts_Steel = new RecipeProto.ID("VoidProducerProdcuts_Steel");
        public readonly static RecipeProto.ID VoidProducerProdcuts_Copper = new RecipeProto.ID("VoidProducerProdcuts_Copper");
        public readonly static RecipeProto.ID VoidProducerProdcuts_Aluminum = new RecipeProto.ID("VoidProducerProdcuts_Aluminum");
        public readonly static RecipeProto.ID VoidProducerProdcuts_Gold = new RecipeProto.ID("VoidProducerProdcuts_Gold");
        public readonly static RecipeProto.ID VoidProducerProdcuts_Glass = new RecipeProto.ID("VoidProducerProdcuts_Glass");
        public readonly static RecipeProto.ID VoidProducerProdcuts_PolySilicon = new RecipeProto.ID("VoidProducerProdcuts_PolySilicon");
        public readonly static RecipeProto.ID VoidProducerProdcuts_Food = new RecipeProto.ID("VoidProducerProdcuts_Food");
        public readonly static RecipeProto.ID VoidProducerProdcuts_HouseholdGoods = new RecipeProto.ID("VoidProducerProdcuts_HouseholdGoods");
        public readonly static RecipeProto.ID VoidProducerProdcuts_HouseholdElectronics = new RecipeProto.ID("VoidProducerProdcuts_HouseholdElectronics");
        public readonly static RecipeProto.ID VoidProducerProdcuts_ConcreteSlab = new RecipeProto.ID("VoidProducerProdcuts_ConcreteSlab");
        public readonly static RecipeProto.ID VoidProducerProdcuts_ConstructionParts = new RecipeProto.ID("VoidProducerProdcuts_ConstructionParts");
        public readonly static RecipeProto.ID VoidProducerProdcuts_ConstructionParts2 = new RecipeProto.ID("VoidProducerProdcuts_ConstructionParts2");
        public readonly static RecipeProto.ID VoidProducerProdcuts_ConstructionParts3 = new RecipeProto.ID("VoidProducerProdcuts_ConstructionParts3");
        public readonly static RecipeProto.ID VoidProducerProdcuts_ConstructionParts4 = new RecipeProto.ID("VoidProducerProdcuts_ConstructionParts4");
        public readonly static RecipeProto.ID VoidProducerProdcuts_Electronics = new RecipeProto.ID("VoidProducerProdcuts_Electronics");
        public readonly static RecipeProto.ID VoidProducerProdcuts_Electronics2 = new RecipeProto.ID("VoidProducerProdcuts_Electronics2");
        public readonly static RecipeProto.ID VoidProducerProdcuts_Microchips = new RecipeProto.ID("VoidProducerProdcuts_Microchips");
        public readonly static RecipeProto.ID VoidProducerProdcuts_Graphite = new RecipeProto.ID("VoidProducerProdcuts_Graphite");
        public readonly static RecipeProto.ID VoidProducerProdcuts_ImpureCopper = new RecipeProto.ID("VoidProducerProdcuts_ImpureCopper");
        public readonly static RecipeProto.ID VoidProducerProdcuts_UraniumPellets = new RecipeProto.ID("VoidProducerProdcuts_UraniumPellets");
        public readonly static RecipeProto.ID VoidProducerProdcuts_UraniumRod = new RecipeProto.ID("VoidProducerProdcuts_UraniumRod");
        public readonly static RecipeProto.ID VoidProducerProdcuts_Rubber = new RecipeProto.ID("VoidProducerProdcuts_Rubber");

        public readonly static RecipeProto.ID VoidProducerLoose_Dirt = new RecipeProto.ID("VoidProducerLoose_Dirt");
        public readonly static RecipeProto.ID VoidProducerLoose_Digestate = new RecipeProto.ID("VoidProducerLoose_Digestate");
        public readonly static RecipeProto.ID VoidProducerLoose_Sludge = new RecipeProto.ID("VoidProducerLoose_Sludge");
        public readonly static RecipeProto.ID VoidProducerLoose_Limestone = new RecipeProto.ID("VoidProducerLoose_Limestone");
        public readonly static RecipeProto.ID VoidProducerLoose_Rock = new RecipeProto.ID("VoidProducerLoose_Rock");
        public readonly static RecipeProto.ID VoidProducerLoose_Gravel = new RecipeProto.ID("VoidProducerLoose_Gravel");
        public readonly static RecipeProto.ID VoidProducerLoose_FilterMedia = new RecipeProto.ID("VoidProducerLoose_FilterMedia");
        public readonly static RecipeProto.ID VoidProducerLoose_Bedrock = new RecipeProto.ID("VoidProducerLoose_Bedrock");
        public readonly static RecipeProto.ID VoidProducerLoose_Coal = new RecipeProto.ID("VoidProducerLoose_Coal");
        public readonly static RecipeProto.ID VoidProducerLoose_Slag = new RecipeProto.ID("VoidProducerLoose_Slag");
        public readonly static RecipeProto.ID VoidProducerLoose_SlagCrushed = new RecipeProto.ID("VoidProducerLoose_SlagCrushed");
        public readonly static RecipeProto.ID VoidProducerLoose_IronOre = new RecipeProto.ID("VoidProducerLoose_IronOre");
        public readonly static RecipeProto.ID VoidProducerLoose_IronOreCrushed = new RecipeProto.ID("VoidProducerLoose_IronOreCrushed");
        public readonly static RecipeProto.ID VoidProducerLoose_MetalScrap = new RecipeProto.ID("VoidProducerLoose_MetalScrap");
        public readonly static RecipeProto.ID VoidProducerLoose_CopperOre = new RecipeProto.ID("VoidProducerLoose_CopperOre");
        public readonly static RecipeProto.ID VoidProducerLoose_CopperOreCrushed = new RecipeProto.ID("VoidProducerLoose_CopperOreCrushed");
        public readonly static RecipeProto.ID VoidProducerLoose_Bauxite = new RecipeProto.ID("VoidProducerLoose_Bauxite");
        public readonly static RecipeProto.ID VoidProducerLoose_GoldOre = new RecipeProto.ID("VoidProducerLoose_GoldOre");
        public readonly static RecipeProto.ID VoidProducerLoose_GoldOreCrushed = new RecipeProto.ID("VoidProducerLoose_GoldOreCrushed");
        public readonly static RecipeProto.ID VoidProducerLoose_GoldOrePowder = new RecipeProto.ID("VoidProducerLoose_GoldOrePowder");
        public readonly static RecipeProto.ID VoidProducerLoose_GoldOreConcentrate = new RecipeProto.ID("VoidProducerLoose_GoldOreConcentrate");
        public readonly static RecipeProto.ID VoidProducerLoose_Sand = new RecipeProto.ID("VoidProducerLoose_Sand");
        public readonly static RecipeProto.ID VoidProducerLoose_GlassMix = new RecipeProto.ID("VoidProducerLoose_GlassMix");
        public readonly static RecipeProto.ID VoidProducerLoose_UraniumOre = new RecipeProto.ID("VoidProducerLoose_UraniumOre");
        public readonly static RecipeProto.ID VoidProducerLoose_UraniumOreCrushed = new RecipeProto.ID("VoidProducerLoose_UraniumOreCrushed");
        public readonly static RecipeProto.ID VoidProducerLoose_YellowCake = new RecipeProto.ID("VoidProducerLoose_YellowCake");
        public readonly static RecipeProto.ID VoidProducerLoose_Quartz = new RecipeProto.ID("VoidProducerLoose_Quartz");
        public readonly static RecipeProto.ID VoidProducerLoose_Salt = new RecipeProto.ID("VoidProducerLoose_Salt");
        public readonly static RecipeProto.ID VoidProducerLoose_Sulfur = new RecipeProto.ID("VoidProducerLoose_Sulfur");
        public readonly static RecipeProto.ID VoidProducerLoose_Stardust = new RecipeProto.ID("VoidProducerLoose_Stardust");
    }
}
