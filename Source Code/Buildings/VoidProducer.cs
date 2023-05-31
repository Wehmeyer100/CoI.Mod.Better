using CoI.Mod.Better.MyIDs;
using CoI.Mod.Better.Shared;
using CoI.Mod.Better.Shared.Extensions;
using CoI.Mod.Better.Shared.Utilities;
using Mafi;
using Mafi.Base;
using Mafi.Core.Entities.Animations;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Factory.Recipes;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using Mafi.Core.Research;

namespace CoI.Mod.Better.Buildings
{
	public class VoidProducerData
	{
		public Duration     currentDuration = 20.Seconds();
		public int          currentInputAmount;
		public MachineProto Machine;
	}

	public class VoidProducer : IModData
	{
		public void RegisterData(ProtoRegistrator registrator)
		{
			if (!BetterMod.Config.Systems.VoidProducer || !BetterMod.Config.Systems.Cheats) return;

			// Add Cheats
			if (BetterMod.Config.Systems.Cheats)
			{
				BetterDebug.Info("VoidProducer >> Generate cheats machines.");
				GenerateCheatMachines(registrator);

				BetterDebug.Info("VoidProducer >> Generate cheats researches.");
				GenerateCheatResearches(registrator);
			}
		}

        #region Researches

		private void GenerateCheatResearches(ProtoRegistrator registrator)
		{
			// Generate Research
			ResearchNodeProtoBuilder.State research_state_t1 = registrator.ResearchNodeProtoBuilder
				.Start("Void Producer CHEAT", MyIDs.Research.VoidProducerCheat)
				.AddMachineToUnlock(Machines.VoidProducerFluidCheat)
				.AddMachineToUnlock(Machines.VoidProducerLooseCheat)
				.AddMachineToUnlock(Machines.VoidProducerProductCheat)
				.AddAllRecipesOfMachineToUnlock(Machines.VoidProducerFluidCheat)
				.AddAllRecipesOfMachineToUnlock(Machines.VoidProducerLooseCheat)
				.AddAllRecipesOfMachineToUnlock(Machines.VoidProducerProductCheat);

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
			research_t1.AddGridPos(master_research, -Constants.UIStepSize);
		}

        #endregion

		private RecipeProto.ID _getRecipeID(string typeName, string fieldName)
		{
			return new RecipeProto.ID("MyVoidProducer" + typeName + "Recipe" + fieldName.Trim());
		}

        #region Machines

		private MachineProto GenerateMachine(ProtoRegistrator registrator, MachineProto.ID protoID, string name, string desc, EntityCostsTpl costs, int powerConsume, Proto.ID categorie, string middleLayout, MachineProto.ID icon)
		{
			return registrator.MachineProtoBuilder
				.Start(name, protoID)
				.Description(desc)
				.SetCost(costs)
				.SetElectricityConsumption(Electricity.FromKw(powerConsume))
				.SetCategories(categorie)
				.SetLayout(
					"   [3][3][3]   ", "   [3][3][3]   ", middleLayout, "   [3][3][3]   ", "   [3][3][3]   ", "   [3][3][3]   " // "A#>[4][4][4]>~X"
				)
				.SetPrefabPath("Assets/Base/Machines/Waste/Shredder.prefab")
				.SetAnimationParams(AnimationParams.Loop())
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
				Machines.VoidProducerProductCheat,
				"Void Producer Products",
				"Produce Products without waste",
				Costs.Machines.SmokeStack,
				BetterMod.Config.VoidProducer.PowerConsume,
				ToolbarCategories.MachinesMetallurgy,
				"   [4][4][4]>#X",
				Ids.Machines.MicrochipMachine
			);

			VoidProducerData producerData = new VoidProducerData
			{
				Machine = machine,
				currentDuration = BetterMod.Config.VoidProducer.Duration.Seconds(),
				currentInputAmount = BetterMod.Config.VoidProducer.AmountInput,
			};

			GenerateCountableProduct(registrator, producerData, "Unit");
		}

		private void GenerateMachineLoose(ProtoRegistrator registrator)
		{
			MachineProto machine = GenerateMachine
			(
				registrator,
				Machines.VoidProducerLooseCheat,
				"Void Producer Loose",
				"Produce Loose without waste",
				Costs.Machines.SmokeStack,
				BetterMod.Config.VoidProducer.PowerConsume,
				ToolbarCategories.MachinesMetallurgy,
				"   [4][4][4]>~X",
				Ids.Machines.ArcFurnace
			);

			VoidProducerData producerData = new VoidProducerData
			{
				Machine = machine,
				currentDuration = BetterMod.Config.VoidProducer.Duration.Seconds(),
				currentInputAmount = BetterMod.Config.VoidProducer.AmountInput,
			};

			GenerateLooseProduct(registrator, producerData, "Loose");
		}

		private void GenerateMachineFluid(ProtoRegistrator registrator)
		{
			MachineProto machine = GenerateMachine
			(
				registrator,
				Machines.VoidProducerFluidCheat,
				"Void Producer Fluid",
				"Produce Fluids without waste",
				Costs.Machines.SmokeStack,
				BetterMod.Config.VoidProducer.PowerConsume,
				ToolbarCategories.MachinesMetallurgy,
				"   [4][4][4]>@X",
				Ids.Machines.LandWaterPump
			);

			VoidProducerData producerData = new VoidProducerData
			{
				Machine = machine,
				currentDuration = BetterMod.Config.VoidProducer.Duration.Seconds(),
				currentInputAmount = BetterMod.Config.VoidProducer.AmountInput,
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

				BetterDebug.Info("VoidProducer >> GenerateCountableProduct(id: " + recipeID + ") >> Output: " + product.Id);
			}
		}

		private void GenerateLooseProduct(ProtoRegistrator registrator, VoidProducerData data, string typeName)
		{
			foreach ((string fieldName, ProductProto product) in ProductUtility.GetLooseProducts(registrator))
			{
				RecipeProto.ID recipeID = _getRecipeID(typeName, fieldName);
				GenerateRecipes(registrator, data, recipeID, product);

				BetterDebug.Info("VoidProducer >> GenerateLooseProduct(id: " + recipeID + ") >> Output: " + product.Id);
			}

			GenerateRecipes(registrator, data, _getRecipeID(typeName, "Recyclables"), registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.Recyclables));
		}

		private void GenerateFluidProduct(ProtoRegistrator registrator, VoidProducerData data, string typeName)
		{
			foreach ((string fieldName, ProductProto product) in ProductUtility.GetFluidProducts(registrator))
			{
				RecipeProto.ID recipeID = _getRecipeID(typeName, fieldName);
				GenerateRecipes(registrator, data, recipeID, product);

				BetterDebug.Info("VoidProducer >> GenerateFluidProduct(id: " + recipeID + ") >> Output: " + product.Id);
			}

		}

		private void GenerateRecipes(ProtoRegistrator registrator, VoidProducerData data, RecipeProto.ID recipeID, ProductProto inputProduct, bool isEmpty = false)
		{
			RecipeProtoBuilder.State result = registrator.RecipeProtoBuilder
				.Start("Produce " + inputProduct.Strings.Name, recipeID, data.Machine)
				.SetDuration(data.currentDuration);

			if (isEmpty)
			{
				result.EnableEmptyRecipe();
			}
			else
			{
				result.AddOutput("X", inputProduct, data.currentInputAmount.Quantity());
			}
			result.BuildAndAdd();
		}

        #endregion
	}
}