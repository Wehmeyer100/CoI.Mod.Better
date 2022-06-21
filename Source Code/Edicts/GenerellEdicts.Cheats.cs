using CoI.Mod.Better.Extensions;
using Mafi;
using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core;
using Mafi.Core.Mods;
using Mafi.Core.Population.Edicts;
using Mafi.Core.Prototypes;
using Mafi.Core.Research;
using Mafi.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CoI.Mod.Better.Edicts
{
    internal partial class GenerellEdicts : IModData
    {

        private static void Cheats(ProtoRegistrator registrator, ResearchNodeProto master_research)
        {
            // Generate Cheat Research
            ResearchNodeProtoBuilder.State research_state_cheat_t1 = registrator.ResearchNodeProtoBuilder
                .Start("Generell Edict Cheat", MyIDs.Research.GenerellEdictsResearchCheat_T1)
                .AddEdictToUnlock(MyIDs.Eticts.Generell.UnityPointsT1_CHEAT, MyIDs.Eticts.Generell.ReduceServiceT1_CHEAT);

            if (BetterMod.Config.Default.UnlockAllCheatsResearches)
            {
                research_state_cheat_t1.SetCostsFree();
            }
            else
            {
                research_state_cheat_t1.SetCostsOne();
            }
            ResearchNodeProto research_cheat_t1 = research_state_cheat_t1.BuildAndAdd();

            // Add parent to my research CHEAT
            ResearchNodeProto master_cheat_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.VehicleCapIncreaseID_CHEAT);
            research_cheat_t1.AddGridPos(master_cheat_research, BetterMod.UI_StepSize, -BetterMod.UI_StepSize);


            // Generate Cheat Research
            ResearchNodeProtoBuilder.State research_state_cheat_t2 = registrator.ResearchNodeProtoBuilder
                .Start("Generell Edict II Cheat", MyIDs.Research.GenerellEdictsResearchCheat_T2)
                .AddEdictToUnlock(MyIDs.Eticts.Generell.UnityPointsT2_CHEAT, MyIDs.Eticts.Generell.ReduceServiceT2_CHEAT);

            if (BetterMod.Config.Default.UnlockAllCheatsResearches)
            {
                research_state_cheat_t2.SetCostsFree();
            }
            else
            {
                research_state_cheat_t2.SetCostsOne();
            }
            ResearchNodeProto research_cheat_t2 = research_state_cheat_t2.BuildAndAdd();
            research_cheat_t2.AddGridPos(research_cheat_t1);


            // Generate Cheat Research
            ResearchNodeProtoBuilder.State research_state_cheat_t3 = registrator.ResearchNodeProtoBuilder
                .Start("Generell Edict III Cheat", MyIDs.Research.GenerellEdictsResearchCheat_T3)
                .AddEdictToUnlock(MyIDs.Eticts.Generell.UnityPointsT3_CHEAT, MyIDs.Eticts.Generell.ReduceServiceT3_CHEAT);

            if (BetterMod.Config.Default.UnlockAllCheatsResearches)
            {
                research_state_cheat_t3.SetCostsFree();
            }
            else
            {
                research_state_cheat_t3.SetCostsOne();
            }
            ResearchNodeProto research_cheat_t3 = research_state_cheat_t3.BuildAndAdd();
            research_cheat_t3.AddGridPos(research_cheat_t2);

            // Generate Cheat Research
            ResearchNodeProtoBuilder.State research_state_cheat_t4 = registrator.ResearchNodeProtoBuilder
                .Start("Generell Edict IV Cheat", MyIDs.Research.GenerellEdictsResearchCheat_T4)
                .AddEdictToUnlock(MyIDs.Eticts.Generell.UnityPointsT4_CHEAT, MyIDs.Eticts.Generell.ReduceServiceT4_CHEAT);

            if (BetterMod.Config.Default.UnlockAllCheatsResearches)
            {
                research_state_cheat_t4.SetCostsFree();
            }
            else
            {
                research_state_cheat_t4.SetCostsOne();
            }
            ResearchNodeProto research_cheat_t4 = research_state_cheat_t4.BuildAndAdd();
            research_cheat_t4.AddGridPos(research_cheat_t3);

            // Generate Cheat Research
            ResearchNodeProtoBuilder.State research_state_cheat_t5 = registrator.ResearchNodeProtoBuilder
                .Start("Generell Edict V Cheat", MyIDs.Research.GenerellEdictsResearchCheat_T5)
                .AddEdictToUnlock(MyIDs.Eticts.Generell.UnityPointsT5_CHEAT, MyIDs.Eticts.Generell.ReduceServiceT5_CHEAT);

            if (BetterMod.Config.Default.UnlockAllCheatsResearches)
            {
                research_state_cheat_t5.SetCostsFree();
            }
            else
            {
                research_state_cheat_t5.SetCostsOne();
            }
            ResearchNodeProto research_cheat_t5 = research_state_cheat_t5.BuildAndAdd();
            research_cheat_t5.AddGridPos(research_cheat_t4);

            Debug.Log("GenerellEdicts >> Generell Edict cheats created!");
        }
    }
}
