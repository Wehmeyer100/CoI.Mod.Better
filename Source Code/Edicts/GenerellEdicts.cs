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
        private int countUnityPointsEdicts = 1;
        private int countReduceServiceEdicts = 1;
        private float CheatUpkeepEdicts = -0.5f;

        private readonly string translationComment = "policy / edict which can enabled by the player in their Captain's office.";
        public static EdictCategoryProto category;
        public static EdictCategoryProto categoryCheats;

        public void RegisterData(ProtoRegistrator registrator)
        {
            if (!BetterMod.Config.Systems.GenerellEdicts) return;

            CheatUpkeepEdicts = BetterMod.Config.Default.CheatUpkeepEdicts;
            category = registrator.PrototypesDb.Add(new EdictCategoryProto(MyIDs.Eticts.BetterMod, Proto.CreateStr(MyIDs.Eticts.BetterMod, "Better mod")));
            categoryCheats = registrator.PrototypesDb.Add(new EdictCategoryProto(MyIDs.Eticts.BetterModCheats, Proto.CreateStr(MyIDs.Eticts.BetterModCheats, "Better mod: Cheats")));

            AddUnityPoints(registrator);
            AddReduceService(registrator);

            GenerateResearch(registrator);
        }

        private void GenerateResearch(ProtoRegistrator registrator)
        {
            // Generate Research T1
            ResearchNodeProtoBuilder.State research_state_t1 = registrator.ResearchNodeProtoBuilder
                .Start("Generell Edict", MyIDs.Research.GenerellEdictsResearchT1)
                .SetCosts(BetterMod.Config.GenerellEdicts.ResearchCostT1);

            ResearchNodeProto research_t1 = research_state_t1.BuildAndAdd();

            ResearchNodeProto master_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.CaptainsOffice);
            // Add parent to my research T1
            research_t1.AddParentPlusGridPos(master_research, BetterMod.UI_StepSize, (BetterMod.UI_StepSize * 2));


            // Generate Research T2
            ResearchNodeProtoBuilder.State research_state_t2 = registrator.ResearchNodeProtoBuilder
               .Start("Generell Edict II", MyIDs.Research.GenerellEdictsResearchT2)
               //.AddEdictToUnlock(MyIDs.Eticts.Generell.UnityPointsT2)
               .SetCosts(BetterMod.Config.GenerellEdicts.ResearchCostT2);

            ResearchNodeProto research_t2 = research_state_t2.BuildAndAdd();

            // Add parent to my research T2
            research_t2.AddParentPlusGridPos(research_t1);



            // Generate Research T3
            ResearchNodeProtoBuilder.State research_state_t3 = registrator.ResearchNodeProtoBuilder
               .Start("Generell Edict III", MyIDs.Research.GenerellEdictsResearchT3)
               //.AddEdictToUnlock(MyIDs.Eticts.Generell.UnityPointsT3)
               .SetCosts(BetterMod.Config.GenerellEdicts.ResearchCostT3);

            ResearchNodeProto research_t3 = research_state_t3.BuildAndAdd();

            // Add parent to my research T3
            research_t3.AddParentPlusGridPos(research_t2);



            // Generate Research T4
            ResearchNodeProtoBuilder.State research_state_t4 = registrator.ResearchNodeProtoBuilder
               .Start("Generell Edict IV", MyIDs.Research.GenerellEdictsResearchT4)
               //.AddEdictToUnlock(MyIDs.Eticts.Generell.UnityPointsT4)
               .SetCosts(BetterMod.Config.GenerellEdicts.ResearchCostT4);

            ResearchNodeProto research_t4 = research_state_t4.BuildAndAdd();

            // Add parent to my research T4
            research_t4.AddParentPlusGridPos(research_t3);



            // Generate Research T5
            ResearchNodeProtoBuilder.State research_state_t5 = registrator.ResearchNodeProtoBuilder
               .Start("Generell Edict V", MyIDs.Research.GenerellEdictsResearchT5)
               //.AddEdictToUnlock(MyIDs.Eticts.Generell.UnityPointsT5)
               .SetCosts(BetterMod.Config.GenerellEdicts.ResearchCostT5);

            ResearchNodeProto research_t5 = research_state_t5.BuildAndAdd();

            // Add parent to my research T5
            research_t5.AddParentPlusGridPos(research_t4);

            Debug.Log("GenerellEdicts >> Generell Edict created!");

            if (BetterMod.Config.Systems.Cheats)
            {
                Cheats(registrator, master_research);
            }
        }
    }
}
