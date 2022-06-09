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

namespace CoI.Mod.Better
{
    internal class GenerellEdicts : IModData
    {
        private int countUnityPointsEdicts = 1;
        private int countReduceServiceEdicts = 1;
        private float CheatUpkeepEdicts = -0.5f;

        private readonly string translationComment = "policy / edict which can enabled by the player in their Captain's office.";
        public static EdictCategoryProto category;

        public void RegisterData(ProtoRegistrator registrator)
        {
            if (BetterMod.Config.DisableGenerellEdicts) return;

            CheatUpkeepEdicts = BetterMod.Config.CheatUpkeepEdicts;

            category = registrator.PrototypesDb.Add(new EdictCategoryProto(MyIDs.Eticts.BetterMod, Proto.CreateStr(Ids.EdictCategories.Industry, "Better mod")));

            AddUnityPoints(registrator);
            AddReduceService(registrator);

            GenerateResearch(registrator);
        }

        private void GenerateResearch(ProtoRegistrator registrator)
        {
            // Generate Research T1
            ResearchNodeProtoBuilder.State research_state_t1 = registrator.ResearchNodeProtoBuilder
                .Start("Generell Edict", MyIDs.Research.GenerellEdictsResearchT1)
                .SetCostsWithDifficulty(BetterMod.Config.GenerellEdictsResearchCostT1);

            ResearchNodeProto research_t1 = research_state_t1.BuildAndAdd();

            ResearchNodeProto master_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.CaptainsOffice);
            // Add parent to my research T1
            research_t1.AddParentPlusGridPos(master_research, BetterMod.UI_StepSize, (BetterMod.UI_StepSize * 2));


            // Generate Research T2
            ResearchNodeProtoBuilder.State research_state_t2 = registrator.ResearchNodeProtoBuilder
               .Start("Generell Edict II", MyIDs.Research.GenerellEdictsResearchT2)
               //.AddEdictToUnlock(MyIDs.Eticts.Generell.UnityPointsT2)
               .SetCostsWithDifficulty(BetterMod.Config.GenerellEdictsResearchCostT2);

            ResearchNodeProto research_t2 = research_state_t2.BuildAndAdd();

            // Add parent to my research T2
            research_t2.AddParentPlusGridPos(research_t1);



            // Generate Research T3
            ResearchNodeProtoBuilder.State research_state_t3 = registrator.ResearchNodeProtoBuilder
               .Start("Generell Edict III", MyIDs.Research.GenerellEdictsResearchT3)
               //.AddEdictToUnlock(MyIDs.Eticts.Generell.UnityPointsT3)
               .SetCostsWithDifficulty(BetterMod.Config.GenerellEdictsResearchCostT3);

            ResearchNodeProto research_t3 = research_state_t3.BuildAndAdd();

            // Add parent to my research T3
            research_t3.AddParentPlusGridPos(research_t2);



            // Generate Research T4
            ResearchNodeProtoBuilder.State research_state_t4 = registrator.ResearchNodeProtoBuilder
               .Start("Generell Edict IV", MyIDs.Research.GenerellEdictsResearchT4)
               //.AddEdictToUnlock(MyIDs.Eticts.Generell.UnityPointsT4)
               .SetCostsWithDifficulty(BetterMod.Config.GenerellEdictsResearchCostT4);

            ResearchNodeProto research_t4 = research_state_t4.BuildAndAdd();

            // Add parent to my research T4
            research_t4.AddParentPlusGridPos(research_t3);



            // Generate Research T5
            ResearchNodeProtoBuilder.State research_state_t5 = registrator.ResearchNodeProtoBuilder
               .Start("Generell Edict V", MyIDs.Research.GenerellEdictsResearchT5)
               //.AddEdictToUnlock(MyIDs.Eticts.Generell.UnityPointsT5)
               .SetCostsWithDifficulty(BetterMod.Config.GenerellEdictsResearchCostT5);

            ResearchNodeProto research_t5 = research_state_t5.BuildAndAdd();

            // Add parent to my research T5
            research_t5.AddParentPlusGridPos(research_t4);

            if (!BetterMod.Config.DisableCheats)
            {
                Cheats(registrator, master_research);
            }
        }

        private static void Cheats(ProtoRegistrator registrator, ResearchNodeProto master_research)
        {
            // Generate Cheat Research
            ResearchNodeProtoBuilder.State research_state_cheat_t1 = registrator.ResearchNodeProtoBuilder
                .Start("Generell Edict Cheat", MyIDs.Research.GenerellEdictsResearchCheat_T1)
                .SetCostsOne()
                .AddEdictToUnlock(MyIDs.Eticts.Generell.UnityPointsT1_CHEAT, MyIDs.Eticts.Generell.ReduceServiceT1_CHEAT);

            ResearchNodeProto research_cheat_t1 = research_state_cheat_t1.BuildAndAdd();

            // Add parent to my research CHEAT
            ResearchNodeProto master_cheat_research = registrator.PrototypesDb.Get<ResearchNodeProto>(MyIDs.Research.VehicleCapIncreaseID_CHEAT).ValueOrNull;
            research_cheat_t1.AddGridPos(master_cheat_research, BetterMod.UI_StepSize, -BetterMod.UI_StepSize);


            // Generate Cheat Research
            ResearchNodeProtoBuilder.State research_state_cheat_t2 = registrator.ResearchNodeProtoBuilder
                .Start("Generell Edict II Cheat", MyIDs.Research.GenerellEdictsResearchCheat_T2)
                .SetCostsOne()
                .AddEdictToUnlock(MyIDs.Eticts.Generell.UnityPointsT2_CHEAT, MyIDs.Eticts.Generell.ReduceServiceT2_CHEAT);

            ResearchNodeProto research_cheat_t2 = research_state_cheat_t2.BuildAndAdd();
            research_cheat_t2.AddGridPos(research_cheat_t1);


            // Generate Cheat Research
            ResearchNodeProtoBuilder.State research_state_cheat_t3 = registrator.ResearchNodeProtoBuilder
                .Start("Generell Edict III Cheat", MyIDs.Research.GenerellEdictsResearchCheat_T3)
                .SetCostsOne()
                .AddEdictToUnlock(MyIDs.Eticts.Generell.UnityPointsT3_CHEAT, MyIDs.Eticts.Generell.ReduceServiceT3_CHEAT);

            ResearchNodeProto research_cheat_t3 = research_state_cheat_t3.BuildAndAdd();
            research_cheat_t3.AddGridPos(research_cheat_t2);

            // Generate Cheat Research
            ResearchNodeProtoBuilder.State research_state_cheat_t4 = registrator.ResearchNodeProtoBuilder
                .Start("Generell Edict IV Cheat", MyIDs.Research.GenerellEdictsResearchCheat_T4)
                .SetCostsOne()
                .AddEdictToUnlock(MyIDs.Eticts.Generell.UnityPointsT4_CHEAT, MyIDs.Eticts.Generell.ReduceServiceT4_CHEAT);

            ResearchNodeProto research_cheat_t4 = research_state_cheat_t4.BuildAndAdd();
            research_cheat_t4.AddGridPos(research_cheat_t3);

            // Generate Cheat Research
            ResearchNodeProtoBuilder.State research_state_cheat_t5 = registrator.ResearchNodeProtoBuilder
                .Start("Generell Edict V Cheat", MyIDs.Research.GenerellEdictsResearchCheat_T5)
                .SetCostsOne()
                .AddEdictToUnlock(MyIDs.Eticts.Generell.UnityPointsT5_CHEAT, MyIDs.Eticts.Generell.ReduceServiceT5_CHEAT);

            ResearchNodeProto research_cheat_t5 = research_state_cheat_t5.BuildAndAdd();
            research_cheat_t5.AddGridPos(research_cheat_t4);
        }

        private void AddUnityPoints(ProtoRegistrator registrator)
        {
            // Add Cheats
            if (BetterMod.Config.DisableCheats) return;

            // Add Cheats
            GenerateUnityPoints(registrator, MyIDs.Eticts.Generell.UnityPointsT1_CHEAT, 5.0f, "CHEAT");
            GenerateUnityPoints(registrator, MyIDs.Eticts.Generell.UnityPointsT2_CHEAT, 10.0f, "CHEAT");
            GenerateUnityPoints(registrator, MyIDs.Eticts.Generell.UnityPointsT3_CHEAT, 20.0f, "CHEAT");
            GenerateUnityPoints(registrator, MyIDs.Eticts.Generell.UnityPointsT4_CHEAT, 50.0f, "CHEAT");
            GenerateUnityPoints(registrator, MyIDs.Eticts.Generell.UnityPointsT5_CHEAT, 100.0f, "CHEAT");
        }

        private void GenerateUnityPoints(ProtoRegistrator registrator, Proto.ID protoID, float monthlyUpointsCost, string title_add = "")
        {
            countUnityPointsEdicts++;

            LocStr1 locStr = Loc.Str1(
                protoID.ToString() + "__desc",
                "Unity increase by {0}",
                "policy / edict which can enabled by the player in their Captain's office. {0}=" + monthlyUpointsCost + "%"
            );

            LocStr descShort = LocalizationManager.CreateAlreadyLocalizedStr(
                protoID.ToString() + "_formatted",
                locStr.Format(monthlyUpointsCost.ToString()).Value
            );

            registrator.PrototypesDb.Add(new EdictWithPropertiesProto(
                protoID,
                Proto.CreateStr(protoID, "Unity Plus T" + countUnityPointsEdicts.ToString() + title_add, descShort, translationComment),
                category,
                (-monthlyUpointsCost).Upoints(),
                ImmutableArray.Create(Make.Kvp(IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, 0.Percent())),
                Option<EdictProto>.None,
                new EdictProto.Gfx("Assets/Base/Icons/Edicts/UnityIncreased.svg"))
            );
        }

        private void AddReduceService(ProtoRegistrator registrator)
        {
            // Add Cheats
            if (BetterMod.Config.DisableCheats) return;

            // Add Cheats
            GenerateReduceService(registrator, MyIDs.Eticts.Generell.ReduceServiceT1_CHEAT, 30, "CHEAT");
            GenerateReduceService(registrator, MyIDs.Eticts.Generell.ReduceServiceT2_CHEAT, 40, "CHEAT");
            GenerateReduceService(registrator, MyIDs.Eticts.Generell.ReduceServiceT3_CHEAT, 50, "CHEAT");
            GenerateReduceService(registrator, MyIDs.Eticts.Generell.ReduceServiceT4_CHEAT, 60, "CHEAT");
            GenerateReduceService(registrator, MyIDs.Eticts.Generell.ReduceServiceT5_CHEAT, 75, "CHEAT");
        }

        private void GenerateReduceService(ProtoRegistrator registrator, Proto.ID protoID, int reduceServiceConsum, string title_add = "")
        {
            countReduceServiceEdicts++;

            LocStr1 locStr = Loc.Str1(
                protoID.ToString() + "__desc",
                "All settlement services consumption increased by {0}",
                "policy / edict which can enabled by the player in their Captain's office. {0}=" + reduceServiceConsum + "%"
            );

            LocStr descShort = LocalizationManager.CreateAlreadyLocalizedStr(
                protoID.ToString() + "_formatted",
                locStr.Format(reduceServiceConsum.ToString()).Value
            );

            registrator.PrototypesDb.Add(new EdictWithPropertiesProto(
                protoID,
                Proto.CreateStr(protoID, "Settlement Consumption T" + countReduceServiceEdicts.ToString() + title_add, descShort, translationComment),
                category,
                CheatUpkeepEdicts.Upoints(),
                ImmutableArray.Create(Make.Kvp(IdsCore.PropertyIds.FoodConsumptionMultiplier, reduceServiceConsum.Percent())),
                Option<EdictProto>.None,
                new EdictProto.Gfx("Assets/Base/Icons/Edicts/FoodReduced.svg"))
            );
        }
    }
}
