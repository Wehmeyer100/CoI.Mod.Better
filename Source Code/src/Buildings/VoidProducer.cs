using CoI.Mod.Better.Extensions;
using Mafi;
using Mafi.Base;
using Mafi.Base.Prototypes.Machines.PowerGenerators;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core;
using Mafi.Core.Entities;
using Mafi.Core.Entities.Animations;
using Mafi.Core.Entities.Static;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Factory.Recipes;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using Mafi.Core.Research;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoI.Mod.Better
{
    public class VoidProducer : IModData
    {
        private Duration currentDuration = 20.Seconds();
        private int currentInputAmount = 0;

        private MachineProto machine;

        public void RegisterData(ProtoRegistrator registrator)
        {
            if (MoreRecipes.Config.DisableVoidProducer) return; 

            // Add Cheats
            if (!MoreRecipes.Config.DisableCheats)
            {
                Cheats(registrator);
            }
        }

        private void Cheats(ProtoRegistrator registrator)
        {
            currentDuration = MoreRecipes.Config.VoidProducerCheatDuration.Seconds();
            currentInputAmount = MoreRecipes.Config.VoidProducerCheatAmountInput;


            machine = registrator.MachineProtoBuilder
                .Start("Void Producer Liquid", MyIDs.Machines.VoidProducerLiquidCheat)
                .Description("Produce liquids without waste", "short description of a machine")
                .SetCost(Costs.Machines.SmokeStack)
                .SetElectricityConsumption(Electricity.FromKw(MoreRecipes.Config.VoidProducerCheatPowerConsume))
                .SetCategories(Ids.ToolbarCategories.Machines)
                .SetLayout("[2][7][7][2]   ", "[2][7][7][2]   ", "[2][7][7][2]   ", "[2][4][4]X2> @ ", "   [2][2][2]   ", "   [2][2][2]   ")
                .SetPrefabPath("Assets/Base/Machines/Pump/LandWaterPump.prefab")
                .SetAnimationParams(AnimationParams.Loop())
                .SetMachineSound("Assets/Base/Machines/Pump/LandWaterPump/LandWaterPump_Sound.prefab")
                .SetCustomIconPath(MoreRecipes.GetIconPath<MachineProto>(registrator, Ids.Machines.LandWaterPump))
                .BuildAndAdd();

            GenerateLiquidsRecipes(registrator);

            machine = registrator.MachineProtoBuilder
                .Start("Void Producer Products", MyIDs.Machines.VoidProducerProductCheat)
                .Description("Produce Products without waste", "short description of a machine")
                .SetCost(Costs.Machines.SmokeStack)
                .SetElectricityConsumption(Electricity.FromKw(MoreRecipes.Config.VoidProducerCheatPowerConsume))
                .SetCategories(Ids.ToolbarCategories.Machines)
                .SetLayout("[2][7][7][2]   ", "[2][7][7][2]   ", "[2][7][7][2]   ", "[2][4][4]X2> # ", "   [2][2][2]   ", "   [2][2][2]   ")
                .SetPrefabPath("Assets/Base/Machines/Pump/LandWaterPump.prefab")
                .SetAnimationParams(AnimationParams.Loop())
                .SetMachineSound("Assets/Base/Machines/Pump/LandWaterPump/LandWaterPump_Sound.prefab")
                .SetCustomIconPath(MoreRecipes.GetIconPath<MachineProto>(registrator, Ids.Machines.MicrochipMachine))
                .BuildAndAdd();

            GenerateProductsRecipes(registrator);

            machine = registrator.MachineProtoBuilder
                .Start("Void Producer Loose", MyIDs.Machines.VoidProducerLooseCheat)
                .Description("Produce Loose without waste", "short description of a machine")
                .SetCost(Costs.Machines.SmokeStack)
                .SetElectricityConsumption(Electricity.FromKw(MoreRecipes.Config.VoidProducerCheatPowerConsume))
                .SetCategories(Ids.ToolbarCategories.Machines)
                .SetLayout("[2][7][7][2]   ", "[2][7][7][2]   ", "[2][7][7][2]   ", "[2][4][4]X2> ~ ", "   [2][2][2]   ", "   [2][2][2]   ")
                .SetPrefabPath("Assets/Base/Machines/Pump/LandWaterPump.prefab")
                .SetAnimationParams(AnimationParams.Loop())
                .SetMachineSound("Assets/Base/Machines/Pump/LandWaterPump/LandWaterPump_Sound.prefab")
                .SetCustomIconPath(MoreRecipes.GetIconPath<MachineProto>(registrator, Ids.Machines.ArcFurnace))
                .BuildAndAdd();

            GenerateLooseRecipes(registrator);


            // Generate Research
            ResearchNodeProtoBuilder.State research_state_t1 = registrator.ResearchNodeProtoBuilder
                .Start("Void Producer CHEAT", MyIDs.Research.VoidProducerCheat)
                .SetCostsOne()
                .AddMachineToUnlock(MyIDs.Machines.VoidProducerLiquidCheat)
                .AddMachineToUnlock(MyIDs.Machines.VoidProducerLooseCheat)
                .AddMachineToUnlock(MyIDs.Machines.VoidProducerProductCheat)
                .AddAllRecipesOfMachineToUnlock(MyIDs.Machines.VoidProducerLiquidCheat)
                .AddAllRecipesOfMachineToUnlock(MyIDs.Machines.VoidProducerLooseCheat)
                .AddAllRecipesOfMachineToUnlock(MyIDs.Machines.VoidProducerProductCheat);

            ResearchNodeProto research_t1 = research_state_t1.BuildAndAdd();

            // Add parent to my research T1
            ResearchNodeProto master_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.VoidCrusherCheat);
            research_t1.AddGridPos(master_research);
        }

        private void GenerateLiquidsRecipes(ProtoRegistrator registrator)
        {
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_Fertilizer, Ids.Products.Fertilizer);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_Water, Ids.Products.Water);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_ChilledWater, Ids.Products.ChilledWater);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_Seawater, Ids.Products.Seawater);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_Brine, Ids.Products.Brine);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_WasteWater, Ids.Products.WasteWater);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_ToxicSlurry, Ids.Products.ToxicSlurry);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_Chlorine, Ids.Products.Chlorine);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_SteamHi, Ids.Products.SteamHi);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_SteamLo, Ids.Products.SteamLo);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_SteamDepleted, Ids.Products.SteamDepleted);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_Exhaust, Ids.Products.Exhaust);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_CrudeOil, Ids.Products.CrudeOil);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_Diesel, Ids.Products.Diesel);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_Naphtha, Ids.Products.Naphtha);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_FuelGas, Ids.Products.FuelGas);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_SourWater, Ids.Products.SourWater);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_Ammonia, Ids.Products.Ammonia);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_Acid, Ids.Products.Acid);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_HeavyOil, Ids.Products.HeavyOil);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_MediumOil, Ids.Products.MediumOil);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_LightOil, Ids.Products.LightOil);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_Hydrogen, Ids.Products.Hydrogen);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_Nitrogen, Ids.Products.Nitrogen);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_CarbonDioxide, Ids.Products.CarbonDioxide);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLiquids_Oxygen, Ids.Products.Oxygen);
        }

        private void GenerateProductsRecipes(ProtoRegistrator registrator)
        {
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_Wood, Ids.Products.Wood);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_Cement, Ids.Products.Cement);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_Iron, Ids.Products.Iron);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_Steel, Ids.Products.Steel);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_Copper, Ids.Products.Copper);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_Aluminum, Ids.Products.Aluminum);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_Gold, Ids.Products.Gold);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_Glass, Ids.Products.Glass);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_PolySilicon, Ids.Products.PolySilicon);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_Food, Ids.Products.Food);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_HouseholdGoods, Ids.Products.HouseholdGoods);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_HouseholdElectronics, Ids.Products.HouseholdElectronics);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_ConcreteSlab, Ids.Products.ConcreteSlab);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_ConstructionParts, Ids.Products.ConstructionParts);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_ConstructionParts2, Ids.Products.ConstructionParts2);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_ConstructionParts3, Ids.Products.ConstructionParts3);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_ConstructionParts4, Ids.Products.ConstructionParts4);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_Electronics, Ids.Products.Electronics);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_Electronics2, Ids.Products.Electronics2);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_Microchips, Ids.Products.Microchips);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_Graphite, Ids.Products.Graphite);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_ImpureCopper, Ids.Products.ImpureCopper);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_UraniumPellets, Ids.Products.UraniumPellets);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_UraniumRod, Ids.Products.UraniumRod);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerProdcuts_Rubber, Ids.Products.Rubber);
        }
        
        private void GenerateLooseRecipes(ProtoRegistrator registrator)
        {
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_Dirt, Ids.Products.Dirt);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_Digestate, Ids.Products.Digestate);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_Sludge, Ids.Products.Sludge);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_Limestone, Ids.Products.Limestone);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_Rock, Ids.Products.Rock);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_Gravel, Ids.Products.Gravel);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_FilterMedia, Ids.Products.FilterMedia);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_Bedrock, Ids.Products.Bedrock);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_Coal, Ids.Products.Coal);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_Slag, Ids.Products.Slag);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_SlagCrushed, Ids.Products.SlagCrushed);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_IronOre, Ids.Products.IronOre);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_IronOreCrushed, Ids.Products.IronOreCrushed);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_MetalScrap, Ids.Products.MetalScrap);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_CopperOre, Ids.Products.CopperOre);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_CopperOreCrushed, Ids.Products.CopperOreCrushed);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_Bauxite, Ids.Products.Bauxite);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_GoldOre, Ids.Products.GoldOre);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_GoldOreCrushed, Ids.Products.GoldOreCrushed);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_GoldOrePowder, Ids.Products.GoldOrePowder);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_GoldOreConcentrate, Ids.Products.GoldOreConcentrate);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_Sand, Ids.Products.Sand);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_GlassMix, Ids.Products.GlassMix);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_UraniumOre, Ids.Products.UraniumOre);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_UraniumOreCrushed, Ids.Products.UraniumOreCrushed);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_YellowCake, Ids.Products.YellowCake);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_Quartz, Ids.Products.Quartz);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_Salt, Ids.Products.Salt);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_Sulfur, Ids.Products.Sulfur);
            GenerateRecipes(registrator, MyIDs.Recipes.VoidProducerLoose_Stardust, Ids.Products.Stardust);
        }

        private void GenerateRecipes(ProtoRegistrator registrator, RecipeProto.ID recipeID, ProductProto.ID productID)
        {
            var productProto = registrator.PrototypesDb.GetOrThrow<ProductProto>(productID);

            registrator.RecipeProtoBuilder
                .Start("Produce " + productProto.Strings.Name, recipeID, machine)
                .SetDuration(currentDuration)
                .AddOutput("X", registrator.PrototypesDb.GetOrThrow<ProductProto>(productID), currentInputAmount.Quantity())
                .BuildAndAdd();
        }
    }
}
