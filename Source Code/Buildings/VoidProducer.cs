using CoI.Mod.Better.Extensions;
using CoI.Mod.Better.Utilities;
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

namespace CoI.Mod.Better.Buildings
{
    public class VoidProducerData
    {
        public MachineProto Machine;

        public Duration currentDuration = 20.Seconds();
        public int currentInputAmount = 0;
    }

    public class VoidProducer : IModData
    {
        public void RegisterData(ProtoRegistrator registrator)
        {
            if (!BetterMod.Config.Systems.VoidProducer || !BetterMod.Config.Systems.Cheats) return;

            // Add Cheats
            if (BetterMod.Config.Systems.Cheats)
            {
                Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> VoidCrusher >> Generate cheats machines.");
                GenerateCheatMachines(registrator);

                Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> VoidCrusher >> Generate cheats researches.");
                GenerateCheatResearches(registrator);
            }
        }

        #region Researches
        private void GenerateCheatResearches(ProtoRegistrator registrator)
        {
            // Generate Research
            ResearchNodeProtoBuilder.State research_state_t1 = registrator.ResearchNodeProtoBuilder
                .Start("Void Producer CHEAT", MyIDs.Research.VoidProducerCheat)
                .AddMachineToUnlock(MyIDs.Machines.VoidProducerFluidCheat)
                .AddMachineToUnlock(MyIDs.Machines.VoidProducerLooseCheat)
                .AddMachineToUnlock(MyIDs.Machines.VoidProducerProductCheat)
                .AddAllRecipesOfMachineToUnlock(MyIDs.Machines.VoidProducerFluidCheat)
                .AddAllRecipesOfMachineToUnlock(MyIDs.Machines.VoidProducerLooseCheat)
                .AddAllRecipesOfMachineToUnlock(MyIDs.Machines.VoidProducerProductCheat);

            if (BetterMod.Config.Default.UnlockAllCheatsResearches)
            {
                research_state_t1.SetCostsFree();
            }
            else
            {
                research_state_t1.SetCostsOne();
            }
            ResearchNodeProto research_t1 = research_state_t1.BuildAndAdd();

            // Add parent to my research T1
            ResearchNodeProto master_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.VoidCrusherCheat);
            research_t1.AddGridPos(master_research, -BetterMod.UI_StepSize);
        }
        #endregion

        #region Machines
        private MachineProto GenerateMachine(ProtoRegistrator registrator, MachineProto.ID protoID, string name, string desc, EntityCostsTpl costs, int powerConsume, Proto.ID categorie, string middleLayout, MachineProto.ID icon)
        {
            return registrator.MachineProtoBuilder
                .Start(name, protoID)
                .Description(desc, "short description of a machine")
                .SetCost(costs)
                .SetElectricityConsumption(Electricity.FromKw(powerConsume))
                .SetCategories(categorie)
                .SetLayout(
                    "[2][7][7][2]   ",
                    "[2][7][7][2]   ",
                    "[2][7][7][2]   ",
                    middleLayout,
                    "   [2][2][2]   ",
                    "   [2][2][2]   "
                )
                .SetPrefabPath("Assets/Base/Machines/Pump/LandWaterPump.prefab")
                .SetAnimationParams(AnimationParams.Loop())
                .SetMachineSound("Assets/Base/Machines/Pump/LandWaterPump/LandWaterPump_Sound.prefab")
                .SetCustomIconPath(EntityProtoUtility.GetIconPath<MachineProto>(registrator, icon))
                .BuildAndAdd();
        }

        private void GenerateCheatMachines(ProtoRegistrator registrator)
        {
            GenerateMachineCountable(registrator);
            GenerateMachineLoose(registrator);
            GenerateMachineFluid(registrator);
        }

        private void GenerateMachineCountable(ProtoRegistrator registrator)
        {
            MachineProto machine = GenerateMachine
            (
                registrator,
                MyIDs.Machines.VoidProducerProductCheat,
                "Void Producer Products",
                "Produce Products without waste",
                Costs.Machines.SmokeStack,
                BetterMod.Config.VoidProducer.PowerConsume,
                MyIDs.ToolbarCategories.MachinesMetallurgy,
                "[2][4][4]X2> # ",
                Ids.Machines.MicrochipMachine
            );

            VoidProducerData producerData = new VoidProducerData()
            {
                Machine = machine,
                currentDuration = BetterMod.Config.VoidProducer.Duration.Seconds(),
                currentInputAmount = BetterMod.Config.VoidProducer.AmountInput
            };

            GenerateCountableProduct(registrator, producerData, "Unit");
        }

        private void GenerateMachineLoose(ProtoRegistrator registrator)
        {
            MachineProto machine = GenerateMachine
            (
                registrator,
                MyIDs.Machines.VoidProducerLooseCheat,
                "Void Producer Loose",
                "Produce Loose without waste",
                Costs.Machines.SmokeStack,
                BetterMod.Config.VoidProducer.PowerConsume,
                MyIDs.ToolbarCategories.MachinesMetallurgy,
                "[2][4][4]X2> ~ ",
                Ids.Machines.ArcFurnace
            );

            VoidProducerData producerData = new VoidProducerData()
            {
                Machine = machine,
                currentDuration = BetterMod.Config.VoidProducer.Duration.Seconds(),
                currentInputAmount = BetterMod.Config.VoidProducer.AmountInput
            };

            GenerateLooseProduct(registrator, producerData, "Loose");
        }

        private void GenerateMachineFluid(ProtoRegistrator registrator)
        {
            MachineProto machine = GenerateMachine
           (
               registrator,
               MyIDs.Machines.VoidProducerFluidCheat,
               "Void Producer Fluid",
               "Produce Fluids without waste",
               Costs.Machines.SmokeStack,
               BetterMod.Config.VoidProducer.PowerConsume,
               MyIDs.ToolbarCategories.MachinesMetallurgy,
               "[2][4][4]X2> @ ",
               Ids.Machines.LandWaterPump
           );

            VoidProducerData producerData = new VoidProducerData()
            {
                Machine = machine,
                currentDuration = BetterMod.Config.VoidProducer.Duration.Seconds(),
                currentInputAmount = BetterMod.Config.VoidProducer.AmountInput
            };
            GenerateFluidProduct(registrator, producerData, "Fluid");
        }

        #endregion

        #region Recipes


        private void GenerateCountableProduct(ProtoRegistrator registrator, VoidProducerData data, string typeName)
        {
            foreach ((string fieldName, ProductProto product) in ProductUtility.GetCountableProducts(registrator))
            {
                RecipeProto.ID recipeID = _getRecipeID(typeName, fieldName);
                GenerateRecipes(registrator, data, recipeID, product);

                Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> VoidProducer >> GenerateCountableProduct(id: " + recipeID + ") >> Output: " + product.Id);
            }
        }
        private void GenerateLooseProduct(ProtoRegistrator registrator, VoidProducerData data, string typeName)
        {
            foreach ((string fieldName, ProductProto product) in ProductUtility.GetLooseProducts(registrator))
            {
                RecipeProto.ID recipeID = _getRecipeID(typeName, fieldName);
                GenerateRecipes(registrator, data, recipeID, product);

                Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> VoidProducer >> GenerateLooseProduct(id: " + recipeID + ") >> Output: " + product.Id);
            }
        }

        private void GenerateFluidProduct(ProtoRegistrator registrator, VoidProducerData data, string typeName)
        {
            foreach ((string fieldName, ProductProto product) in ProductUtility.GetFluidProducts(registrator))
            {
                RecipeProto.ID recipeID = _getRecipeID(typeName, fieldName);
                GenerateRecipes(registrator, data, recipeID, product);

                Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> VoidProducer >> GenerateFluidProduct(id: " + recipeID + ") >> Output: " + product.Id);
            }
        }

        private void GenerateRecipes(ProtoRegistrator registrator, VoidProducerData data, RecipeProto.ID recipeID, ProductProto inputProduct)
        {
            registrator.RecipeProtoBuilder
                .Start("Produce " + inputProduct.Strings.Name, recipeID, data.Machine)
                .SetDuration(data.currentDuration)
                .AddOutput("X", inputProduct, data.currentInputAmount.Quantity())
                .BuildAndAdd();
        }

        #endregion

        private RecipeProto.ID _getRecipeID(string typeName, string fieldName)
        {
            return new RecipeProto.ID("MyVoidProducer" + typeName + "Recipe" + fieldName.Trim());
        }
    }


}
