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
            ResearchNodeProto research_t1 = GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchCheat_T1, "Generell Edict Cheat I", BetterMod.Config.Default.CheatResearchCosts, true, new ResearchNodeUIData(master_research, true, BetterMod.UI_StepSize * 2, -BetterMod.UI_StepSize), MyIDs.Eticts.Generell.UnityPointsT1_CHEAT, MyIDs.Eticts.Generell.ReduceServiceT1_CHEAT, MyIDs.Eticts.Generell.FarmMultiplierT1_CHEAT, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT1_CHEAT, MyIDs.Eticts.Generell.RecyclingRatioDiffT1_CHEAT);
            ResearchNodeProto research_t2 = GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchCheat_T2, "Generell Edict Cheat II", BetterMod.Config.Default.CheatResearchCosts, true, research_t1, true, MyIDs.Eticts.Generell.UnityPointsT2_CHEAT, MyIDs.Eticts.Generell.ReduceServiceT2_CHEAT, MyIDs.Eticts.Generell.FarmMultiplierT2_CHEAT, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT2_CHEAT, MyIDs.Eticts.Generell.RecyclingRatioDiffT2_CHEAT);
            ResearchNodeProto research_t3 = GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchCheat_T3, "Generell Edict Cheat III", BetterMod.Config.Default.CheatResearchCosts, true, research_t2, true, MyIDs.Eticts.Generell.UnityPointsT3_CHEAT, MyIDs.Eticts.Generell.ReduceServiceT3_CHEAT, MyIDs.Eticts.Generell.FarmMultiplierT3_CHEAT, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT3_CHEAT, MyIDs.Eticts.Generell.RecyclingRatioDiffT3_CHEAT);
            ResearchNodeProto research_t4 = GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchCheat_T4, "Generell Edict Cheat IV", BetterMod.Config.Default.CheatResearchCosts, true, research_t3, true, MyIDs.Eticts.Generell.UnityPointsT4_CHEAT, MyIDs.Eticts.Generell.ReduceServiceT4_CHEAT, MyIDs.Eticts.Generell.FarmMultiplierT4_CHEAT, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT4_CHEAT, MyIDs.Eticts.Generell.RecyclingRatioDiffT4_CHEAT);
            GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchCheat_T5, "Generell Edict Cheat V", BetterMod.Config.Default.CheatResearchCosts, true, research_t4, true, MyIDs.Eticts.Generell.UnityPointsT5_CHEAT, MyIDs.Eticts.Generell.ReduceServiceT5_CHEAT, MyIDs.Eticts.Generell.FarmMultiplierT5_CHEAT, MyIDs.Eticts.Generell.FarmWaterConsumMultiplierT5_CHEAT, MyIDs.Eticts.Generell.RecyclingRatioDiffT5_CHEAT);

            Debug.Log("BetterMod(V: " + BetterMod.MyVersion + ") >> GenerellEdicts >> Generell edict cheats created!");
        }
    }
}
