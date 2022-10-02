using System.Collections.Generic;
using CoI.Mod.Better.lang;
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
	public class VoidCrusherData
	{
		public Duration currentDuration = 20.Seconds();
		public int      currentInputAmount;

		public bool         hasOutput;
		public MachineProto Machine;

		public ProductProto.ID output                = Ids.Products.Recyclables;
		public ProductProto.ID outputNoneRecyclables = Ids.Products.Gravel;
	}

	public class VoidCrusher : IModData
	{
		public void RegisterData(ProtoRegistrator registrator)
		{
			if (!BetterMod.Config.Systems.VoidCrusher) return;


			BetterDebug.Info("Void Crusher >> Generate vanilla machines.");
			GenerateVanillaMachine(registrator);

			BetterDebug.Info("Void Crusher >> Generate vanilla research.");
			GenerateVanillaResearch(registrator);

			// Add Cheats
			if (BetterMod.Config.Systems.Cheats)
			{
				BetterDebug.Info("Void Crusher >> Generate cheat machines.");
				GenerateCheatsMachine(registrator);

				BetterDebug.Info("Void Crusher >> Generate cheat researches.");
				GenerateCheatsResearch(registrator);
			}
		}


		private RecipeProto.ID _getRecipeID(string title_addr, string fieldName, bool cheat)
		{
			return new RecipeProto.ID("MyVoidCrusherRecipeDynamic" + title_addr + (cheat ? "Cheat" : "") + fieldName.Trim());
		}

        #region Researches

		private void GenerateVanillaResearch(ProtoRegistrator registrator)
		{
			BetterDebug.Info("Void Crusher >> Generate Researchs..");

			// Generate Research T1
			string Name = LangManager.Instance.Get("void_crusher_machine");
			ResearchNodeProto research_t0 = GenerateResearch(registrator, MyIDs.Research.VoidCrusher, Name, Machines.VoidCrusher, Ids.Research.ConcreteProduction, Constants.UIStepSize * 2, (int)-(Constants.UIStepSize * 1.5f), new ResearchCostsTpl(3));


			BetterDebug.Info("Void Crusher >> Generate Researchs done.");
		}

		private void GenerateCheatsResearch(ProtoRegistrator registrator)
		{
			BetterDebug.Info("Void Crusher >> Generate Cheat Researchs..");

			// Generate Research T1
			string name_t0 = LangManager.Instance.Get("void_crusher_machine_cheat");
			ResearchNodeProto research_t0 = GenerateResearch(registrator, MyIDs.Research.VoidCrusherCheat, name_t0, Machines.VoidCrusherCheat, MyIDs.Research.VehicleCapIncreaseID_ZERO, Constants.UIStepSize, Constants.UIStepSize);

			// Generate Research T1
			string name_t1 = LangManager.Instance.Get("void_crusher_machine_recyclables_cheat");
			ResearchNodeProto research_t1 = GenerateResearch(registrator, MyIDs.Research.VoidCrusherRecyclablesCheat, name_t1, Machines.VoidCrusherRecyclablesCheat, research_t0);

			// Generate Research T2
			string name_t2 = LangManager.Instance.Get("void_crusher_machine_fluid_cheat");
			ResearchNodeProto research_t2 = GenerateResearch(registrator, MyIDs.Research.VoidCrusherFluidCheat, name_t2, Machines.VoidCrusherFluidCheat, research_t1);

			// Generate Research T3
			string name_t3 = LangManager.Instance.Get("void_crusher_machine_loose_cheat");
			GenerateResearch(registrator, MyIDs.Research.VoidCrusherLooseCheat, name_t3, Machines.VoidCrusherLooseCheat, research_t2);

			BetterDebug.Info("Void Crusher >> Generate Cheat Researchs done.");
		}

		private ResearchNodeProto GenerateResearch(ProtoRegistrator registrator, ResearchNodeProto.ID protoID, string name, MachineProto.ID machineID, ResearchNodeProto.ID previusResearchID, int ui_stepSize_x = Constants.UIStepSize, int ui_stepSize_y = 0, ResearchCostsTpl costs = null)
		{
			return GenerateResearch(registrator, protoID, name, machineID, registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(previusResearchID), ui_stepSize_x, ui_stepSize_y, costs);
		}

		private ResearchNodeProto GenerateResearch(ProtoRegistrator registrator, ResearchNodeProto.ID protoID, string name, MachineProto.ID machineID, ResearchNodeProto previusResearch, int ui_stepSize_x = Constants.UIStepSize, int ui_stepSize_y = 0, ResearchCostsTpl costs = null)
		{
			// Generate Research
			ResearchNodeProtoBuilder.State research_state = registrator.ResearchNodeProtoBuilder
				.Start(name, protoID)
				.AddMachineToUnlock(machineID)
				.AddAllRecipesOfMachineToUnlock(machineID);

			if (costs == null)
			{
				if (BetterMod.Config.Default.UnlockAllCheatsResearches)
				{
					research_state.SetCostsFree();
				}
				else
				{
					research_state.SetCostsOne();
				}
			}
			else
			{
				research_state.SetCosts(costs);
			}
			ResearchNodeProto research = research_state.BuildAndAdd();

			// Add parent to my research T1
			research.AddParentPlusGridPos(previusResearch, ui_stepSize_x, ui_stepSize_y);
			return research;
		}

        #endregion

        #region Machines

		private void GenerateVanillaMachine(ProtoRegistrator registrator)
		{
			MachineProto machine = GenerateMachine
			(
				registrator,
				Machines.VoidCrusher,
				LangManager.Instance.Get("void_crusher_machine"),
				LangManager.Instance.Get("void_crusher_machine_desc"),
				Costs.Machines.Crusher,
				150,
				10,
				ToolbarCategories.MachinesMetallurgy,
				" # >3A[4][3][3][3]X3> ~ "
			);

			// Generate Products Recipes for the Machine
			VoidCrusherData crusherData = new VoidCrusherData
			{
				Machine = machine,
				currentDuration = 20.Seconds(),
				currentInputAmount = 20,

				hasOutput = true,
				output = Ids.Products.Recyclables,
				outputNoneRecyclables = Ids.Products.Gravel,
			};
			GenerateProductRecipes(registrator, crusherData);
		}

		private void GenerateCheatsMachine(ProtoRegistrator registrator)
		{
			BetterDebug.Info("Void Crusher >> Generate Machines..");

			GenerateMachineCountable(registrator);
			GenerateMachineCheatFluid(registrator);
			GenerateMachineCheatLoose(registrator);
			GenerateMachineCheatRecyclables(registrator);

			BetterDebug.Info("Void Crusher >> Generate Machines done.");
		}

		private void GenerateMachineCountable(ProtoRegistrator registrator)
		{
			MachineProto machine = GenerateMachine
			(
				registrator,
				Machines.VoidCrusherCheat,
				LangManager.Instance.Get("void_crusher_machine_cheat"),
				LangManager.Instance.Get("void_crusher_machine_cheat_desc"),
				Costs.Machines.Crusher,
				BetterMod.Config.VoidDestroy.PowerConsume,
				BetterMod.Config.VoidDestroy.Emission,
				ToolbarCategories.MachinesMetallurgy,
				"#3 >3A[4][3][3][3][3]   "
			);

			// Generate Products Recipes for the Machine
			VoidCrusherData crusherData = new VoidCrusherData
			{
				Machine = machine,
				currentDuration = BetterMod.Config.VoidDestroy.Duration.Seconds(),
				currentInputAmount = BetterMod.Config.VoidDestroy.AmountInput,

				hasOutput = false,
			};
			GenerateProductRecipes(registrator, crusherData, true);
		}

		private void GenerateMachineCheatRecyclables(ProtoRegistrator registrator)
		{
			MachineProto machine = GenerateMachine
			(
				registrator,
				Machines.VoidCrusherRecyclablesCheat,
				LangManager.Instance.Get("void_crusher_machine_recyclables_cheat"),
				LangManager.Instance.Get("void_crusher_machine_recyclables_cheat_desc"),
				Costs.Machines.Crusher,
				BetterMod.Config.VoidDestroy.PowerConsume,
				BetterMod.Config.VoidDestroy.Emission,
				ToolbarCategories.MachinesMetallurgy,
				" # >3A[4][3][3][3]X3> ~ "
			);

			// Generate Products Recipes for the Machine
			VoidCrusherData crusherData = new VoidCrusherData
			{
				Machine = machine,
				currentDuration = BetterMod.Config.VoidDestroy.Duration.Seconds(),
				currentInputAmount = BetterMod.Config.VoidDestroy.AmountInput,

				hasOutput = true,
				output = Ids.Products.Recyclables,
				outputNoneRecyclables = Ids.Products.Recyclables,
			};
			GenerateProductRecipes(registrator, crusherData, true, "Recyclables");
		}

		private void GenerateMachineCheatLoose(ProtoRegistrator registrator)
		{
			MachineProto machine = GenerateMachine
			(
				registrator,
				Machines.VoidCrusherLooseCheat,
				LangManager.Instance.Get("void_crusher_machine_loose_cheat"),
				LangManager.Instance.Get("void_crusher_machine_loose_cheat_desc"),
				Costs.Machines.Crusher,
				BetterMod.Config.VoidDestroy.PowerConsume,
				BetterMod.Config.VoidDestroy.Emission,
				ToolbarCategories.MachinesMetallurgy,
				"~3 >3A[4][3][3][3][3]   "
			);

			// Generate Products Recipes for the Machine
			VoidCrusherData crusherData = new VoidCrusherData
			{
				Machine = machine,
				currentDuration = BetterMod.Config.VoidDestroy.Duration.Seconds(),
				currentInputAmount = BetterMod.Config.VoidDestroy.AmountInput,
				hasOutput = false,
			};
			GenerateLooseRecipes(registrator, crusherData, true, "loose");
		}

		private void GenerateMachineCheatFluid(ProtoRegistrator registrator)
		{
			MachineProto machine = GenerateMachine
			(
				registrator,
				Machines.VoidCrusherFluidCheat,
				LangManager.Instance.Get("void_crusher_machine_fluid_cheat"),
				LangManager.Instance.Get("void_crusher_machine_fluid_cheat_desc"),
				Costs.Machines.Crusher,
				BetterMod.Config.VoidDestroy.PowerConsume,
				BetterMod.Config.VoidDestroy.Emission,
				ToolbarCategories.MachinesMetallurgy,
				"@3 >3A[4][3][3][3][3]   "
			);

			// Generate Products Recipes for the Machine
			VoidCrusherData crusherData = new VoidCrusherData
			{
				Machine = machine,
				currentDuration = BetterMod.Config.VoidDestroy.Duration.Seconds(),
				currentInputAmount = BetterMod.Config.VoidDestroy.AmountInput,
				hasOutput = false,
			};
			GenerateFluidRecipes(registrator, crusherData, true, "fluid");
		}

		private MachineProto GenerateMachine(ProtoRegistrator registrator, MachineProto.ID protoID, string name, string desc, EntityCostsTpl costs, int powerConsume, int emission, Proto.ID categorie, string middleLayout)
		{
			return registrator.MachineProtoBuilder
				.Start(name, protoID)
				.Description(desc)
				.SetCost(costs)
				.SetElectricityConsumption(Electricity.FromKw(powerConsume))
				.SetCategories(categorie)
				.SetLayout(
					"   [3][4][3][3][3][3]   ",
					middleLayout,
					"   [3][4][3][3][3][3]   ",
					"   [2][3][2][2]         ")
				.SetPrefabPath("Assets/Base/Machines/MetalWorks/Mill.prefab")
				.SetAnimationParams(AnimationParams.Loop())
				.SetMachineSound("Assets/Base/Machines/MetalWorks/Mill/Mill_Sound.prefab")
				.SetEmissionWhenWorking(BetterMod.Config.VoidDestroy.Emission)
				.SetCustomIconPath(EntityProtoUtility.GetIconPath<MachineProto>(registrator, Ids.Machines.Crusher))
				.SetAsLockedOnInit()
				.BuildAndAdd();
		}

        #endregion

        #region Recipes

		private void GenerateProductRecipes(ProtoRegistrator registrator, VoidCrusherData data, bool cheat = false, string title_addr = "")
		{
			List<(string, ProductProto)> result = ProductUtility.GetCountableProducts(registrator, ignorList: new List<ProductProto.ID>
			{
				data.output,
			});
			foreach ((string fieldName, ProductProto product) in result)
			{
				RecipeProto.ID recipeID = _getRecipeID(title_addr, fieldName, cheat);
				GenerateRecipes(registrator, data, recipeID, product, cheat, title_addr);
			}
		}

		private void GenerateLooseRecipes(ProtoRegistrator registrator, VoidCrusherData data, bool cheat = false, string title_addr = "")
		{
			List<(string, ProductProto)> result = ProductUtility.GetLooseProducts(registrator, new List<ProductProto.ID>
			{
				data.output,
			});
			foreach ((string fieldName, ProductProto product) in result)
			{
				RecipeProto.ID recipeID = _getRecipeID(title_addr, fieldName, cheat);
				GenerateRecipes(registrator, data, recipeID, product, cheat, title_addr);
			}
		}

		private void GenerateFluidRecipes(ProtoRegistrator registrator, VoidCrusherData data, bool cheat = false, string title_addr = "")
		{
			List<(string, ProductProto)> result = ProductUtility.GetFluidProducts(registrator, new List<ProductProto.ID>
			{
				data.output,
			});
			foreach ((string fieldName, ProductProto product) in result)
			{
				RecipeProto.ID recipeID = _getRecipeID(title_addr, fieldName, cheat);
				GenerateRecipes(registrator, data, recipeID, product, cheat, title_addr);
			}
		}

		private void GenerateRecipes(ProtoRegistrator registrator, VoidCrusherData data, RecipeProto.ID recipeID, ProductProto inputProduct, bool cheat, string title_addr)
		{
			RecipeProtoBuilder.State result = registrator.RecipeProtoBuilder
				.Start("Destroy" + title_addr + (cheat ? "(Cheat)" : "") + " " + inputProduct.Strings.Name.TranslatedString, recipeID, data.Machine)
				.SetDuration(data.currentDuration)
				.AddInput("A", inputProduct, data.currentInputAmount.Quantity());

			ProductProto.ID output_product = inputProduct.IsRecyclable ? data.output : data.outputNoneRecyclables;
			if (data.hasOutput)
			{
				result.AddOutput("X", registrator.PrototypesDb.GetOrThrow<ProductProto>(output_product), data.currentInputAmount.Quantity());
			}

			result.BuildAndAdd();
			BetterDebug.Info("Void Crusher >> GenerateRecipes(id: " + recipeID + ") >> Input: " + inputProduct.Id + "(IsRecyclable: " + inputProduct.IsRecyclable + "), Output: " + (data.hasOutput ? output_product.ToString() : "none"));
		}

        #endregion
	}
}