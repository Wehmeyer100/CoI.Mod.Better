using CoI.Mod.Better.Extensions;
using CoI.Mod.Better.Utilities;
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
using static CoI.Mod.Better.Utilities.ResearchProtoUtility;

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
            AddFarmMultiplier(registrator);
            AddFarmWaterConsumMultiplier(registrator);
            AddRecyclingRatioDiff(registrator);

            GenerateResearch(registrator);
        }

        private void GenerateResearch(ProtoRegistrator registrator)
        {
            ResearchNodeProto master_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.CaptainsOffice);

            ResearchNodeProto research_t1 = GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchT1, "Generell Edict I", BetterMod.Config.GenerellEdicts.ResearchCostT1, false, new ResearchNodeUIData(master_research, false, BetterMod.UI_StepSize, (BetterMod.UI_StepSize * 2)));
            ResearchNodeProto research_t2 = GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchT2, "Generell Edict II", BetterMod.Config.GenerellEdicts.ResearchCostT2, false, research_t1, false);
            ResearchNodeProto research_t3 = GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchT3, "Generell Edict III", BetterMod.Config.GenerellEdicts.ResearchCostT3, false, research_t2, false);
            ResearchNodeProto research_t4 = GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchT4, "Generell Edict IV", BetterMod.Config.GenerellEdicts.ResearchCostT4, false, research_t3, false);
            GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchT5, "Generell Edict V", BetterMod.Config.GenerellEdicts.ResearchCostT5, false, research_t4, false);

            Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> GenerellEdicts >> Generell Edict created!");

            if (BetterMod.Config.Systems.Cheats)
            {
                Cheats(registrator);
            }
        }
    }
}
