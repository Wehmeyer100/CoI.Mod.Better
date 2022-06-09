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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CoI.Mod.Better
{
    public class VoidProducer : IModData
    {
        private Duration currentDuration = 20.Seconds();
        private int currentInputAmount = 0;

        private MachineProto machine;

        public void RegisterData(ProtoRegistrator registrator)
        {
            if (BetterMod.Config.DisableVoidProducer || BetterMod.Config.DisableCheats) return;

            // Add Cheats
            if (!BetterMod.Config.DisableCheats)
            {
                Cheats(registrator);
            }
        }

        private void Cheats(ProtoRegistrator registrator)
        {
            currentDuration = BetterMod.Config.VoidProducerCheatDuration.Seconds();
            currentInputAmount = BetterMod.Config.VoidProducerCheatAmountInput;


            machine = registrator.MachineProtoBuilder
                .Start("Void Producer Liquid", MyIDs.Machines.VoidProducerLiquidCheat)
                .Description("Produce liquids without waste", "short description of a machine")
                .SetCost(Costs.Machines.SmokeStack)
                .SetElectricityConsumption(Electricity.FromKw(BetterMod.Config.VoidProducerCheatPowerConsume))
                .SetCategories(MyIDs.ToolbarCategories.MachinesMetallurgy)
                .SetLayout(
                    "[2][7][7][2]   ",
                    "[2][7][7][2]   ",
                    "[2][7][7][2]   ",
                    "[2][4][4]X2> @ ",
                    "   [2][2][2]   ",
                    "   [2][2][2]   "
                )
                .SetPrefabPath("Assets/Base/Machines/Pump/LandWaterPump.prefab")
                .SetAnimationParams(AnimationParams.Loop())
                .SetMachineSound("Assets/Base/Machines/Pump/LandWaterPump/LandWaterPump_Sound.prefab")
                .SetCustomIconPath(BetterMod.GetIconPath<MachineProto>(registrator, Ids.Machines.LandWaterPump))
                .BuildAndAdd();

            GenerateLiquidsRecipes(registrator);

            machine = registrator.MachineProtoBuilder
                .Start("Void Producer Products", MyIDs.Machines.VoidProducerProductCheat)
                .Description("Produce Products without waste", "short description of a machine")
                .SetCost(Costs.Machines.SmokeStack)
                .SetElectricityConsumption(Electricity.FromKw(BetterMod.Config.VoidProducerCheatPowerConsume))
                .SetCategories(MyIDs.ToolbarCategories.MachinesMetallurgy)
                .SetLayout(
                    "[2][7][7][2]   ",
                    "[2][7][7][2]   ",
                    "[2][7][7][2]   ",
                    "[2][4][4]X2> # ",
                    "   [2][2][2]   ",
                    "   [2][2][2]   "
                )
                .SetPrefabPath("Assets/Base/Machines/Pump/LandWaterPump.prefab")
                .SetAnimationParams(AnimationParams.Loop())
                .SetMachineSound("Assets/Base/Machines/Pump/LandWaterPump/LandWaterPump_Sound.prefab")
                .SetCustomIconPath(BetterMod.GetIconPath<MachineProto>(registrator, Ids.Machines.MicrochipMachine))
                .BuildAndAdd();

            GenerateProductsRecipes(registrator);

            machine = registrator.MachineProtoBuilder
                .Start("Void Producer Loose", MyIDs.Machines.VoidProducerLooseCheat)
                .Description("Produce Loose without waste", "short description of a machine")
                .SetCost(Costs.Machines.SmokeStack)
                .SetElectricityConsumption(Electricity.FromKw(BetterMod.Config.VoidProducerCheatPowerConsume))
                .SetCategories(MyIDs.ToolbarCategories.MachinesMetallurgy)
                .SetLayout(
                    "[2][7][7][2]   ",
                    "[2][7][7][2]   ",
                    "[2][7][7][2]   ",
                    "[2][4][4]X2> ~ ",
                    "   [2][2][2]   ",
                    "   [2][2][2]   "
                )
                .SetPrefabPath("Assets/Base/Machines/Pump/LandWaterPump.prefab")
                .SetAnimationParams(AnimationParams.Loop())
                .SetMachineSound("Assets/Base/Machines/Pump/LandWaterPump/LandWaterPump_Sound.prefab")
                .SetCustomIconPath(BetterMod.GetIconPath<MachineProto>(registrator, Ids.Machines.ArcFurnace))
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
            research_t1.AddGridPos(master_research, -BetterMod.UI_StepSize);
        }

        private void GenerateRecipesByProductFields<T>(ProtoRegistrator registrator, string typeName)
        {
            IEnumerable<FieldInfo> result = BetterMod.GetAllFields(typeof(Ids.Products));

            foreach (FieldInfo field in result)
            {
                string fieldName = field.Name;
                object value = field.GetValue(null);
                if (field.IsStatic && value != null && value is ProductProto.ID && field.GetCustomAttributes(typeof(T), false).Length > 0)
                {
                    ProductProto.ID fieldValue = (ProductProto.ID)value;
                    Option<ProductProto> resultProduct = registrator.PrototypesDb.Get<ProductProto>(fieldValue);
                    if (resultProduct.HasValue && resultProduct.Value.IsStorable && resultProduct.Value.Radioactivity == 0)
                    {
                        RecipeProto.ID recipeID = new RecipeProto.ID("MyVoidProducer" + typeName + "Recipe" + fieldName.Trim());
                        GenerateRecipes(registrator, recipeID, resultProduct.Value);

                        Debug.Log("VoidProducer >> GenerateRecipesByProductFields<" + typeof(T).Name + ">(id: " + recipeID + ") >> Output: " + resultProduct.Value.Id);
                    }
                }
            }
        }

        private void GenerateLiquidsRecipes(ProtoRegistrator registrator)
        {
            GenerateRecipesByProductFields<FluidProductAttribute>(registrator, "Fluid");
        }

        private void GenerateProductsRecipes(ProtoRegistrator registrator)
        {
            GenerateRecipesByProductFields<CountableProductAttribute>(registrator, "Unit");
        }

        private void GenerateLooseRecipes(ProtoRegistrator registrator)
        {
            GenerateRecipesByProductFields<LooseProductAttribute>(registrator, "Loose");
        }

        private void GenerateRecipes(ProtoRegistrator registrator, RecipeProto.ID recipeID, ProductProto inputProduct)
        {
            registrator.RecipeProtoBuilder
                .Start("Produce " + inputProduct.Strings.Name, recipeID, machine)
                .SetDuration(currentDuration)
                .AddOutput("X", inputProduct, currentInputAmount.Quantity())
                .BuildAndAdd();
        }
    }
}
