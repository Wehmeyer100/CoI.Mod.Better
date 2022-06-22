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

        private static void Cheats(ProtoRegistrator registrator)
        {
            // Add parent to my research CHEAT
            ResearchNodeProto master_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.VehicleCapIncreaseID_CHEAT);

            // Generate Cheat Research
            ResearchNodeProto research_t1 = ResearchProtoUtility.GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchCheat_T1, "Generell Edict Cheat I", BetterMod.Config.Default.CheatResearchCosts, false, new ResearchNodeUIData(master_research, true, BetterMod.UI_StepSize, -BetterMod.UI_StepSize), MyIDs.Eticts.Generell.UnityPointsT1_CHEAT, MyIDs.Eticts.Generell.ReduceServiceT1_CHEAT);
            ResearchNodeProto research_t2 = ResearchProtoUtility.GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchCheat_T2, "Generell Edict Cheat II", BetterMod.Config.Default.CheatResearchCosts, false, research_t1, false, MyIDs.Eticts.Generell.UnityPointsT2_CHEAT, MyIDs.Eticts.Generell.ReduceServiceT2_CHEAT);
            ResearchNodeProto research_t3 = ResearchProtoUtility.GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchCheat_T3, "Generell Edict Cheat III", BetterMod.Config.Default.CheatResearchCosts, false, research_t2, false, MyIDs.Eticts.Generell.UnityPointsT3_CHEAT, MyIDs.Eticts.Generell.ReduceServiceT3_CHEAT);
            ResearchNodeProto research_t4 = ResearchProtoUtility.GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchCheat_T4, "Generell Edict Cheat IV", BetterMod.Config.Default.CheatResearchCosts, false, research_t3, false, MyIDs.Eticts.Generell.UnityPointsT4_CHEAT, MyIDs.Eticts.Generell.ReduceServiceT4_CHEAT);
            ResearchNodeProto research_t5 = ResearchProtoUtility.GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchCheat_T5, "Generell Edict Cheat V", BetterMod.Config.Default.CheatResearchCosts, false, research_t4, false, MyIDs.Eticts.Generell.UnityPointsT5_CHEAT, MyIDs.Eticts.Generell.ReduceServiceT5_CHEAT);

            Debug.Log("GenerellEdicts >> Generell edict cheats created!");
        }
    }
}
