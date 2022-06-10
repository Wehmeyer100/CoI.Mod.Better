using CoI.Mod.Better.Extensions;
using Mafi;
using Mafi.Base;
using Mafi.Core.Buildings.Mine;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Entities;
using Mafi.Core.Entities.Static;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Core.Research;
using Mafi.Core.UnlockingTree;
using Mafi.Localization;
using UnityEngine;
using static Mafi.Base.Ids;

namespace CoI.Mod.Better
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

            if (BetterMod.Config.DisableVehicleCapIncrease)
            {
                return;
            }

            if (!BetterMod.Config.DisableCheats)
            {
                LocStr1 locStr = Loc.Str1(MyIDs.Research.VehicleCapIncreaseID_ZERO.ToString() + "__desc", "Increases vehicle{0}", "{0}=" + StepSize);

                var cheat_research = registrator.ResearchNodeProtoBuilder
                    .Start(MyIDs.Research.VehicleCapIncreaseID_CHEAT.Value.Replace('_', ' '), MyIDs.Research.VehicleCapIncreaseID_CHEAT)
                    .Description(LocalizationManager.CreateAlreadyLocalizedStr(MyIDs.Research.VehicleCapIncreaseID_ZERO.ToString() + "_formatted_master", locStr.Format("").Value))
                    .SetCosts(1)
                    .AddVehicleCapIncrease(StepSize * 10, "Assets/Base/Icons/VehicleLimitIncrease.svg")
                    .BuildAndAdd();

                cheat_research.GridPosition = master_research.GridPosition + new Vector2i((BetterMod.UI_StepSize * 2), -BetterMod.UI_StepSize);
                cheat_research.AddParent(master_research);
            }

            int research_tier = 1;
            var level_1 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_I, research_tier, master_research);
            var level_2 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_II, (research_tier += 2), level_1);
            var level_3 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_III, (research_tier += 2), level_2);
            var level_4 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_IV, (research_tier += 2), level_3);
            var level_5 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_V, (research_tier += 2), level_4);
            var level_6 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_VI, (research_tier += 2), level_5);
            var level_7 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_VII, (research_tier += 2), level_6);
            var level_8 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_VIII, (research_tier += 2), level_7);
            var level_9 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_IX, (research_tier += 2), level_8);
            var level_10 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_X, (research_tier += 2), level_9);
            var level_11 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_XI, (research_tier += 2), level_10);
            var level_12 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_XII, (research_tier += 2), level_11);
            var level_13 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_XIII, (research_tier += 2), level_12);
            var level_14 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_XIV, (research_tier += 2), level_13);
            var level_15 = GenerateStage(registrator, MyIDs.Research.VehicleCapIncreaseID_XV, (research_tier += 2), level_14);
        }

        internal ResearchNodeProto GenerateStage(ProtoRegistrator registrator, ResearchNodeProto.ID protoID, int costLevel, ResearchNodeProto parent)
        {
            LocStr1 locStr = Loc.Str1(protoID.ToString() + "__desc", "Increases vehicle limit by {0}.", "{0}=" + StepSize);
            LocStr desc = LocalizationManager.CreateAlreadyLocalizedStr(protoID.ToString() + "_formatted" + StepSize, locStr.Format(StepSize.ToString()).Value);

            ResearchNodeProto result = registrator.ResearchNodeProtoBuilder
                .Start(protoID.Value.Replace('_', ' '), protoID)
                .Description(desc)
                .SetCosts(costLevel)
                .AddVehicleCapIncrease(StepSize, "Assets/Base/Icons/VehicleLimitIncrease.svg")
                .BuildAndAdd();

            Debug.Log("MyVehicleCapIncrease >> GenerateStage(id: "+protoID+", costlevel: "+costLevel+") >> created!");
            return result.AddParentPlusGridPos(parent);
        }

    }
}