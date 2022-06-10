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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CoI.Mod.Better.Buildings
{
    public class VoidCrusher : IModData
    {
        private Duration currentDuration = 20.Seconds();
        private int currentInputAmount = 0;
        private bool hasOutput = false;

        private MachineProto machine;

        private ProductProto.ID output = Ids.Products.Recyclables;
        private ProductProto.ID outputNoneRecyclables = Ids.Products.Gravel;

        public void RegisterData(ProtoRegistrator registrator)
        {
            if (!BetterMod.Config.Systems.VoidCrusher) return;

            Debug.Log("VoidCrusher >> Generate Research");

            GenerateMachineVanilla(registrator);

            // Generate Research
            ResearchNodeProtoBuilder.State research_state_t1 = registrator.ResearchNodeProtoBuilder
                .Start("Void Crusher", MyIDs.Research.VoidCrusher)
                .SetCosts(3)
                .AddMachineToUnlock(MyIDs.Machines.VoidCrusher)
                .AddAllRecipesOfMachineToUnlock(MyIDs.Machines.VoidCrusher)
                .AddParent(Ids.Research.ConcreteProduction);

            ResearchNodeProto research_t1 = research_state_t1.BuildAndAdd();
            // Load Master Research Proto
            ResearchNodeProto master_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.ConcreteProduction);
            // Add parent to my research T1
            research_t1.AddParentPlusGridPos(master_research, ui_stepSize_x: BetterMod.UI_StepSize * 2, ui_stepSize_y: -6);

            // Add Cheats
            if (BetterMod.Config.Systems.Cheats)
            {
                Cheats(registrator);
            }
        }

        private void Cheats(ProtoRegistrator registrator)
        {
            Debug.Log("VoidCrusher >> Generate Cheat Research");

            GenerateMachineCheat(registrator);
            GenerateMachineCheatFluid(registrator);
            GenerateMachineCheatLoose(registrator);

            GenerateMachineRecyclables(registrator);

            // Generate Research
            ResearchNodeProtoBuilder.State research_state_t1 = registrator.ResearchNodeProtoBuilder
                .Start("Void Crusher CHEAT", MyIDs.Research.VoidCrusherCheat)
                .AddMachineToUnlock(MyIDs.Machines.VoidCrusherCheat)
                .AddMachineToUnlock(MyIDs.Machines.VoidCrusherRecyclablesCheat)
                .AddAllRecipesOfMachineToUnlock(MyIDs.Machines.VoidCrusherCheat)
                .AddAllRecipesOfMachineToUnlock(MyIDs.Machines.VoidCrusherRecyclablesCheat);

            if (BetterMod.Config.Default.UnlockAllCheatsResearches)
            {
                research_state_t1.SetCostsFree();
            }
            else
            {
                research_state_t1.SetCostsOne();
            }
            ResearchNodeProto research_t1 = research_state_t1.BuildAndAdd();

            // Add parent to my research T2
            ResearchNodeProto parent_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.VehicleCapIncreaseID_ZERO);
            research_t1.AddParentPlusGridPos(parent_research, BetterMod.UI_StepSize, BetterMod.UI_StepSize);

            // Generate Research
            ResearchNodeProtoBuilder.State research_state_t2 = registrator.ResearchNodeProtoBuilder
                .Start("Void Crusher Fluid CHEAT", MyIDs.Research.VoidCrusherFluidCheat)
                .AddMachineToUnlock(MyIDs.Machines.VoidCrusherFluidCheat)
                .AddAllRecipesOfMachineToUnlock(MyIDs.Machines.VoidCrusherFluidCheat);

            if (BetterMod.Config.Default.UnlockAllCheatsResearches)
            {
                research_state_t2.SetCostsFree();
            }
            else
            {
                research_state_t2.SetCostsOne();
            }
            ResearchNodeProto research_t2 = research_state_t2.BuildAndAdd();

            // Add parent to my research T3
            research_t2.AddParentPlusGridPos(research_t1);

            // Generate Research
            ResearchNodeProtoBuilder.State research_state_t3 = registrator.ResearchNodeProtoBuilder
                .Start("Void Crusher Loose CHEAT", MyIDs.Research.VoidCrusherLooseCheat)
                .AddMachineToUnlock(MyIDs.Machines.VoidCrusherLooseCheat)
                .AddAllRecipesOfMachineToUnlock(MyIDs.Machines.VoidCrusherLooseCheat);

            if (BetterMod.Config.Default.UnlockAllCheatsResearches)
            {
                research_state_t3.SetCostsFree();
            }
            else
            {
                research_state_t3.SetCostsOne();
            }
            ResearchNodeProto research_t3 = research_state_t3.BuildAndAdd();

            // Add parent to my research T1
            research_t3.AddParentPlusGridPos(research_t2);
        }

        private void GenerateMachineVanilla(ProtoRegistrator registrator)
        {
            currentDuration = 20.Seconds();
            currentInputAmount = 20;
            hasOutput = true;

            // Generate Machine
            machine = registrator.MachineProtoBuilder
                .Start("Void Crusher", MyIDs.Machines.VoidCrusher)
                .Description("Destroy Products with waste", "short description of a machine")
                .SetCost(Costs.Machines.Crusher)
                .SetElectricityConsumption(150.Kw())
                .SetCategories(MyIDs.ToolbarCategories.MachinesMetallurgy)
                .SetLayout(
                    "   [3][4][3][3][3][3]   ",
                    " # >3A[4][3][3][3]X3> ~ ",
                    "   [3][4][3][3][3][3]   ",
                    "   [2][3][2][2]         ")
                .SetPrefabPath("Assets/Base/Machines/MetalWorks/Mill.prefab")
                .SetAnimationParams(AnimationParams.Loop())
                .SetMachineSound("Assets/Base/Machines/MetalWorks/Mill/Mill_Sound.prefab")
                .SetEmissionWhenWorking(10)
                .SetAsLockedOnInit()
                .SetCustomIconPath(BetterMod.GetIconPath<MachineProto>(registrator, Ids.Machines.Crusher))
                .BuildAndAdd();

            output = Ids.Products.Recyclables;
            outputNoneRecyclables = Ids.Products.Gravel;
            // Generate Products Recipes for the Machine
            GenerateCountableProduct<CountableProductAttribute>(registrator);
        }

        private void GenerateMachineCheat(ProtoRegistrator registrator)
        {
            currentDuration = BetterMod.Config.VoidDestroy.Duration.Seconds();
            currentInputAmount = BetterMod.Config.VoidDestroy.AmountInput;
            hasOutput = false;

            machine = registrator.MachineProtoBuilder
                .Start("Void Crusher Cheat", MyIDs.Machines.VoidCrusherCheat)
                .Description("Destroy Products without waste", "short description of a machine")
                .SetCost(Costs.Machines.SmokeStack)
                .SetElectricityConsumption(Electricity.FromKw(BetterMod.Config.VoidDestroy.PowerConsume))
                .SetCategories(MyIDs.ToolbarCategories.MachinesMetallurgy)
                .SetLayout(
                "   [3][4][3][3][3][3]   ",
                "#3 >3A[4][3][3][3][3]   ",
                "   [3][4][3][3][3][3]   ",
                "   [2][3][2][2]         ")
                .SetPrefabPath("Assets/Base/Machines/MetalWorks/Mill.prefab")
                .SetAnimationParams(AnimationParams.Loop())
                .SetMachineSound("Assets/Base/Machines/MetalWorks/Mill/Mill_Sound.prefab")
                .SetEmissionWhenWorking(BetterMod.Config.VoidDestroy.Emission)
                .SetCustomIconPath(BetterMod.GetIconPath<MachineProto>(registrator, Ids.Machines.Crusher))
                .SetAsLockedOnInit()
                .BuildAndAdd();

            // Generate Products Recipes for the Machine
            GenerateCountableProduct<CountableProductAttribute>(registrator, true);
        }

        private void GenerateMachineRecyclables(ProtoRegistrator registrator)
        {
            currentDuration = BetterMod.Config.VoidDestroy.Duration.Seconds();
            currentInputAmount = BetterMod.Config.VoidDestroy.AmountInput;
            hasOutput = true;

            machine = registrator.MachineProtoBuilder
                .Start("Void Crusher Cheat Recyclables", MyIDs.Machines.VoidCrusherRecyclablesCheat)
                .Description("Destroy Products to recyclables", "short description of a machine")
                .SetCost(Costs.Machines.Crusher)
                .SetElectricityConsumption(Electricity.FromKw(BetterMod.Config.VoidDestroy.PowerConsume))
                .SetCategories(MyIDs.ToolbarCategories.MachinesMetallurgy)
                .SetLayout(
                    "   [3][4][3][3][3][3]   ",
                    " # >3A[4][3][3][3]X3> ~ ",
                    "   [3][4][3][3][3][3]   ",
                    "   [2][3][2][2]         ")
                .SetPrefabPath("Assets/Base/Machines/MetalWorks/Mill.prefab")
                .SetAnimationParams(AnimationParams.Loop())
                .SetMachineSound("Assets/Base/Machines/MetalWorks/Mill/Mill_Sound.prefab")
                .SetEmissionWhenWorking(BetterMod.Config.VoidDestroy.Emission)
                .SetAsLockedOnInit()
                .SetCustomIconPath(BetterMod.GetIconPath<MachineProto>(registrator, Ids.Machines.Crusher))
                .BuildAndAdd();

            output = Ids.Products.Recyclables;
            outputNoneRecyclables = Ids.Products.Recyclables;

            // Generate Products Recipes for the Machine
            GenerateCountableProduct<CountableProductAttribute>(registrator, true, "Recyclables");
        }

        private void GenerateMachineCheatLoose(ProtoRegistrator registrator)
        {
            currentDuration = BetterMod.Config.VoidDestroy.Duration.Seconds();
            currentInputAmount = BetterMod.Config.VoidDestroy.AmountInput;
            hasOutput = false;

            machine = registrator.MachineProtoBuilder
                .Start("Void Crusher Loose Cheat", MyIDs.Machines.VoidCrusherLooseCheat)
                .Description("Destroy Loose without waste", "short description of a machine")
                .SetCost(Costs.Machines.SmokeStack)
                .SetElectricityConsumption(Electricity.FromKw(BetterMod.Config.VoidDestroy.PowerConsume))
                .SetCategories(MyIDs.ToolbarCategories.MachinesMetallurgy)
                .SetLayout(
                "   [3][4][3][3][3][3]   ",
                "~3 >3A[4][3][3][3][3]   ",
                "   [3][4][3][3][3][3]   ",
                "   [2][3][2][2]         ")
                .SetPrefabPath("Assets/Base/Machines/MetalWorks/Mill.prefab")
                .SetAnimationParams(AnimationParams.Loop())
                .SetMachineSound("Assets/Base/Machines/MetalWorks/Mill/Mill_Sound.prefab")
                .SetEmissionWhenWorking(BetterMod.Config.VoidDestroy.Emission)
                .SetCustomIconPath(BetterMod.GetIconPath<MachineProto>(registrator, Ids.Machines.Crusher))
                .SetAsLockedOnInit()
                .BuildAndAdd();

            // Generate Products Recipes for the Machine
            GenerateCountableProduct<LooseProductAttribute>(registrator, true, "loose");
        }

        private void GenerateMachineCheatFluid(ProtoRegistrator registrator)
        {
            currentDuration = BetterMod.Config.VoidDestroy.Duration.Seconds();
            currentInputAmount = BetterMod.Config.VoidDestroy.AmountInput;
            hasOutput = false;

            machine = registrator.MachineProtoBuilder
                .Start("Void Crusher Fluid Cheat", MyIDs.Machines.VoidCrusherFluidCheat)
                .Description("Destroy fluid without waste", "short description of a machine")
                .SetCost(Costs.Machines.SmokeStack)
                .SetElectricityConsumption(Electricity.FromKw(BetterMod.Config.VoidDestroy.PowerConsume))
                .SetCategories(MyIDs.ToolbarCategories.MachinesMetallurgy)
                .SetLayout(
                "   [3][4][3][3][3][3]   ",
                "@3 >3A[4][3][3][3][3]   ",
                "   [3][4][3][3][3][3]   ",
                "   [2][3][2][2]         ")
                .SetPrefabPath("Assets/Base/Machines/MetalWorks/Mill.prefab")
                .SetAnimationParams(AnimationParams.Loop())
                .SetMachineSound("Assets/Base/Machines/MetalWorks/Mill/Mill_Sound.prefab")
                .SetEmissionWhenWorking(BetterMod.Config.VoidDestroy.Emission)
                .SetCustomIconPath(BetterMod.GetIconPath<MachineProto>(registrator, Ids.Machines.Crusher))
                .SetAsLockedOnInit()
                .BuildAndAdd();

            // Generate Products Recipes for the Machine
            GenerateCountableProduct<FluidProductAttribute>(registrator, true, "fluid");
        }

        private void GenerateCountableProduct<T>(ProtoRegistrator registrator, bool cheat = false, string title_addr = "") where T : ProductAttribute
        {
            IEnumerable<FieldInfo> result = BetterMod.GetAllFields(typeof(Ids.Products));

            foreach (FieldInfo field in result)
            {
                string fieldName = field.Name;
                object value = field.GetValue(null);
                if (field.IsStatic && value != null && value is ProductProto.ID && field.GetCustomAttributes(typeof(T), false).Length > 0)
                {
                    ProductProto.ID fieldValue = (ProductProto.ID)value;
                    if (fieldValue == output)
                    {
                        continue;
                    }

                    Option<ProductProto> resultProduct = registrator.PrototypesDb.Get<ProductProto>(fieldValue);
                    if (resultProduct.HasValue && resultProduct.Value.IsStorable && resultProduct.Value.Radioactivity == 0)
                    {
                        GenerateRecipes(registrator, new RecipeProto.ID("MyVoidCrusherRecipeDynamic" + title_addr + (cheat ? "Cheat" : "") + fieldName.Trim()), resultProduct.Value, cheat, title_addr);
                    }
                }
            }
        }

        private void GenerateRecipes(ProtoRegistrator registrator, RecipeProto.ID recipeID, ProductProto inputProduct, bool cheat, string title_addr)
        {
            RecipeProtoBuilder.State result = registrator.RecipeProtoBuilder
                .Start("Destroy" + title_addr + (cheat ? " Cheat" : "") + " " + inputProduct.Strings.Name, recipeID, machine)
                .SetDuration(currentDuration)
                .AddInput("A", inputProduct, currentInputAmount.Quantity());

            ProductProto.ID output_product = (inputProduct.IsRecyclable ? output : outputNoneRecyclables);
            if (hasOutput)
            {
                result.AddOutput("X", registrator.PrototypesDb.GetOrThrow<ProductProto>(output_product), currentInputAmount.Quantity());
            }

            result.BuildAndAdd();

            Debug.Log("VoidCrusher >> GenerateRecipes(id: " + recipeID + ") >> Input: " + inputProduct.Id + "(IsRecyclable: " + inputProduct.IsRecyclable + "), Output: " + output_product);
        }
    }
}
