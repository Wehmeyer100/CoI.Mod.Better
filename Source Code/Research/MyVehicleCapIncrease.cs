using CoI.Mod.Better.Shared;
using CoI.Mod.Better.Shared.Extensions;
using Mafi;
using Mafi.Base;
using Mafi.Core.Mods;
using Mafi.Core.Research;
using Mafi.Localization;

namespace CoI.Mod.Better.Research
{
	internal class MyVehicleCapIncrease : IResearchNodesData, IModData
	{
		public const int StepSize = 25;

		public void RegisterData(ProtoRegistrator registrator)
		{
			ResearchNodeProto master_research = registrator.ResearchNodeProtoBuilder
				.Start("Better Research", MyIDs.Research.VehicleCapIncreaseID_ZERO)
				.Description("Coi:Better mod main research.")
				.SetCostsFree()
				.AddVehicleCapIncrease(1, "Assets/Base/Icons/VehicleLimitIncrease.svg")
				.BuildAndAdd();



			master_research.GridPosition += new Vector2i(0, -2);

			if (!BetterMod.Config.Systems.VehicleCapIncrease)
			{
				return;
			}

			GenerateCheats(registrator, master_research);

			int research_tier = 1;
			ResearchNodeProto level_1 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_I, research_tier, master_research);
			ResearchNodeProto level_2 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_II, research_tier += 2, level_1);
			ResearchNodeProto level_3 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_III, research_tier += 2, level_2);
			ResearchNodeProto level_4 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_IV, research_tier += 2, level_3);
			ResearchNodeProto level_5 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_V, research_tier += 2, level_4);
			ResearchNodeProto level_6 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_VI, research_tier += 2, level_5);
			ResearchNodeProto level_7 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_VII, research_tier += 2, level_6);
			ResearchNodeProto level_8 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_VIII, research_tier += 2, level_7);
			ResearchNodeProto level_9 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_IX, research_tier += 2, level_8);
			ResearchNodeProto level_10 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_X, research_tier += 2, level_9);
			ResearchNodeProto level_11 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_XI, research_tier += 2, level_10);
			ResearchNodeProto level_12 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_XII, research_tier += 2, level_11);
			ResearchNodeProto level_13 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_XIII, research_tier += 2, level_12);
			ResearchNodeProto level_14 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_XIV, research_tier += 2, level_13);
			ResearchNodeProto level_15 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_XV, research_tier += 2, level_14);
		}

		private static void GenerateCheats(ProtoRegistrator registrator, ResearchNodeProto master_research)
		{
			if (BetterMod.Config.Systems.Cheats)
			{
				LocStr1 locStr = Loc.Str1(MyIDs.Research.VehicleCapIncreaseID_ZERO + "__desc", "Increases vehicle{0}", "{0}=" + StepSize);

				ResearchNodeProtoBuilder.State cheat_research_proto = registrator.ResearchNodeProtoBuilder
					.Start(MyIDs.Research.VehicleCapIncreaseID_CHEAT.Value.Replace('_', ' '), MyIDs.Research.VehicleCapIncreaseID_CHEAT)
					.Description(LocalizationManager.CreateAlreadyLocalizedStr(MyIDs.Research.VehicleCapIncreaseID_ZERO + "_formatted_master", locStr.Format("").Value))
					.AddVehicleCapIncrease(StepSize * 10, "Assets/Base/Icons/VehicleLimitIncrease.svg");

				if (BetterMod.Config.Default.UnlockAllCheatsResearches)
				{
					cheat_research_proto.SetCostsFree();
				}
				else
				{
					cheat_research_proto.SetCostsOne();
				}
				ResearchNodeProto cheat_research = cheat_research_proto.BuildAndAdd();

				cheat_research.GridPosition = master_research.GridPosition + new Vector2i(Constants.UIStepSize * 2, -Constants.UIStepSize);
				cheat_research.AddParent(master_research);
			}
		}

		internal ResearchNodeProto GenerateStage(ProtoRegistrator registrator, ResearchNodeProto.ID protoID, int costLevel, ResearchNodeProto parent)
		{
			LocStr1 locStr = Loc.Str1(protoID + "__desc", "Increases vehicle limit by {0}.", "{0}=" + StepSize);
			LocStr desc = LocalizationManager.CreateAlreadyLocalizedStr(protoID + "_formatted" + StepSize, locStr.Format(StepSize.ToString()).Value);

			ResearchNodeProto result = registrator.ResearchNodeProtoBuilder
				.Start(protoID.Value.Replace('_', ' '), protoID)
				.Description(desc)
				.SetCosts(costLevel)
				.AddVehicleCapIncrease(StepSize, "Assets/Base/Icons/VehicleLimitIncrease.svg")
				.BuildAndAdd();

			BetterDebug.Info("MyVehicleCapIncrease >> GenerateStage(id: " + protoID + ", costlevel: " + costLevel + ") >> created!");
			return result.AddParentPlusGridPos(parent);
		}
	}
}