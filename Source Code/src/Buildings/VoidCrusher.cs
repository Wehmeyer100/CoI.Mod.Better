using CoI.Mod.Better.Extensions;
using Mafi;
using Mafi.Base;
using Mafi.Core.Entities.Animations;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Factory.Recipes;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Research;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoI.Mod.Better
{
    public class VoidCrusher : IModData
    {
        private Duration currentDuration = 20.Seconds();
        private int currentInputAmount = 0;
        private bool hasOutput = false;

        private MachineProto machine;

        public void RegisterData(ProtoRegistrator registrator)
        {
            if (MoreRecipes.Config.DisableVoidCrusher) return;
            currentDuration = 20.Seconds();
            currentInputAmount = 20;
            hasOutput = true;

            // Load Master Research Proto
            ResearchNodeProto master_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.ConcreteProduction);

            // Generate Machine
            machine = registrator.MachineProtoBuilder
                .Start("Void Crusher", MyIDs.Machines.VoidCrusher)
                .Description("Destroy Products without waste", "short description of a machine")
                .SetCost(Costs.Machines.Crusher)
                .SetElectricityConsumption(150.Kw())
                .SetCategories(Ids.ToolbarCategories.Waste)
                .SetLayout("   [3][4][3][3][3][3]   ", "#3 >3A[4][3][3][3]X3> ~ ", "   [3][4][3][3][3][3]   ", "   [2][3][2][2]         ")
                .SetPrefabPath("Assets/Base/Machines/MetalWorks/Mill.prefab")
                .SetAnimationParams(AnimationParams.Loop())
                .SetMachineSound("Assets/Base/Machines/MetalWorks/Mill/Mill_Sound.prefab")
                .SetEmissionWhenWorking(10)
                .SetAsLockedOnInit()
                .SetCustomIconPath(MoreRecipes.GetIconPath<MachineProto>(registrator, Ids.Machines.Crusher))
                .BuildAndAdd();

            // Generate Products Recipes for the Machine
            GenerateCountableProduct(registrator, MyIDs.Recipes.VoidCrusherIDs);

            // Generate Research
            ResearchNodeProtoBuilder.State research_state_t1 = registrator.ResearchNodeProtoBuilder
                .Start("Void Crusher", MyIDs.Research.VoidCrusher)
                .SetCosts(3)
                .AddMachineToUnlock(MyIDs.Machines.VoidCrusher)
                .AddAllRecipesOfMachineToUnlock(MyIDs.Machines.VoidCrusher)
                .AddParent(Ids.Research.ConcreteProduction);

            ResearchNodeProto research_t1 = research_state_t1.BuildAndAdd();
            // Add parent to my research T1
            research_t1.AddParentPlusGridPos(master_research, ui_stepSize_y: -5);

            // Add Cheats
            if (!MoreRecipes.Config.DisableCheats)
            {
                Cheats(registrator);
            }
        }

        private void Cheats(ProtoRegistrator registrator)
        {
            currentDuration = MoreRecipes.Config.VoidDestroyCheatDuration.Seconds();
            currentInputAmount = MoreRecipes.Config.VoidDestroyCheatAmountInput;
            hasOutput = false;

            machine = registrator.MachineProtoBuilder
                .Start("Void Crusher CHEAT", MyIDs.Machines.VoidCrusherCheat)
                .Description("Destroy Products without waste", "short description of a machine")
                .SetCost(Costs.Machines.SmokeStack)
                .SetElectricityConsumption(Electricity.FromKw(MoreRecipes.Config.VoidDestroyCheatPowerConsume))
                .SetCategories(Ids.ToolbarCategories.Waste)
                .SetLayout("   [3][4][3][3][3][3]   ", "#3 >3A[4][3][3][3][3]   ", "   [3][4][3][3][3][3]   ", "   [2][3][2][2]         ")
                .SetPrefabPath("Assets/Base/Machines/MetalWorks/Mill.prefab")
                .SetAnimationParams(AnimationParams.Loop())
                .SetMachineSound("Assets/Base/Machines/MetalWorks/Mill/Mill_Sound.prefab")
                .SetEmissionWhenWorking(MoreRecipes.Config.VoidDestroyCheatEmission)
                .SetCustomIconPath(MoreRecipes.GetIconPath<MachineProto>(registrator, Ids.Machines.Crusher))
                .SetAsLockedOnInit()
                .BuildAndAdd();

            GenerateCountableProduct(registrator, MyIDs.Recipes.VoidCrusherCheatIDs);


            // Generate Research
            ResearchNodeProtoBuilder.State research_state_t1 = registrator.ResearchNodeProtoBuilder
                .Start("Void Crusher CHEAT", MyIDs.Research.VoidCrusherCheat)
                .SetCostsOne()
                .AddMachineToUnlock(MyIDs.Machines.VoidCrusherCheat)
                .AddAllRecipesOfMachineToUnlock(MyIDs.Machines.VoidCrusherCheat);

            ResearchNodeProto research_t1 = research_state_t1.BuildAndAdd();

            // Add parent to my research T1
            ResearchNodeProto parent_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.VehicleCapIncreaseID_ZERO);
            research_t1.AddParentPlusGridPos(parent_research, MoreRecipes.UI_StepSize * 2, MoreRecipes.UI_StepSize);
        }

        private void GenerateCountableProduct(ProtoRegistrator registrator, MyIDs.Recipes.VoidCrusher crusher_ids)
        {
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_Cement, Ids.Products.Cement);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_Wood, Ids.Products.Wood);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_Rubber, Ids.Products.Rubber);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_Iron, Ids.Products.Iron);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_Steel, Ids.Products.Steel);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_Copper, Ids.Products.Copper);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_Aluminum, Ids.Products.Aluminum);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_Gold, Ids.Products.Gold);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_Glass, Ids.Products.Glass);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_PolySilicon, Ids.Products.PolySilicon);
            //GenerateRecipes(registrator, crusher_ids.VoidDestroy_SiliconWafer, Ids.Products.SiliconWafer);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_Food, Ids.Products.Food);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_HouseholdGoods, Ids.Products.HouseholdGoods);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_HouseholdElectronics, Ids.Products.HouseholdElectronics);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_ConcreteSlab, Ids.Products.ConcreteSlab);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_ConstructionParts, Ids.Products.ConstructionParts);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_ConstructionParts2, Ids.Products.ConstructionParts2);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_ConstructionParts3, Ids.Products.ConstructionParts3);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_ConstructionParts4, Ids.Products.ConstructionParts4);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_Electronics, Ids.Products.Electronics);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_Electronics2, Ids.Products.Electronics2);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_Microchips, Ids.Products.Microchips);
            //GenerateRecipes(registrator, crusher_ids.VoidDestroy_MicrochipsStage1A, Ids.Products.MicrochipsStage1A);
            //GenerateRecipes(registrator, crusher_ids.VoidDestroy_MicrochipsStage1B, Ids.Products.MicrochipsStage1B);
            //GenerateRecipes(registrator, crusher_ids.VoidDestroy_MicrochipsStage1C, Ids.Products.MicrochipsStage1C);
            //GenerateRecipes(registrator, crusher_ids.VoidDestroy_MicrochipsStage2A, Ids.Products.MicrochipsStage2A);
            //GenerateRecipes(registrator, crusher_ids.VoidDestroy_MicrochipsStage2B, Ids.Products.MicrochipsStage2B);
            //GenerateRecipes(registrator, crusher_ids.VoidDestroy_MicrochipsStage2C, Ids.Products.MicrochipsStage2C);
            //GenerateRecipes(registrator, crusher_ids.VoidDestroy_MicrochipsStage3A, Ids.Products.MicrochipsStage3A);
            //GenerateRecipes(registrator, crusher_ids.VoidDestroy_MicrochipsStage3B, Ids.Products.MicrochipsStage3B);
            //GenerateRecipes(registrator, crusher_ids.VoidDestroy_MicrochipsStage3C, Ids.Products.MicrochipsStage3C);
            //GenerateRecipes(registrator, crusher_ids.VoidDestroy_MicrochipsStage4A, Ids.Products.MicrochipsStage4A);
            //GenerateRecipes(registrator, crusher_ids.VoidDestroy_MicrochipsStage4B, Ids.Products.MicrochipsStage4B);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_Graphite, Ids.Products.Graphite);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_ImpureCopper, Ids.Products.ImpureCopper);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_UraniumPellets, Ids.Products.UraniumPellets);
            GenerateRecipes(registrator, crusher_ids.VoidDestroy_UraniumRod, Ids.Products.UraniumRod);
        }

        private void GenerateRecipes(ProtoRegistrator registrator, RecipeProto.ID recipeID, ProductProto.ID productID)
        {
            var productProto = registrator.PrototypesDb.GetOrThrow<ProductProto>(productID);

            var result = registrator.RecipeProtoBuilder
                .Start("Destroy " + productProto.Strings.Name, recipeID, machine)
                .SetDuration(currentDuration)
                .AddInput("A", registrator.PrototypesDb.GetOrThrow<ProductProto>(productID), currentInputAmount.Quantity());

            if (hasOutput)
            {
                result.AddOutput("X", registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.Rock), currentInputAmount.Quantity());
            }

            result.BuildAndAdd();
        }
    }
}
