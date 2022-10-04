using CoI.Mod.Better.MyIDs;
using CoI.Mod.Better.Shared;
using CoI.Mod.Better.Shared.Lang;
using Mafi.Core.Mods;
using Mafi.Core.Research;
using static CoI.Mod.Better.Shared.Utilities.ResearchProtoUtility;

namespace CoI.Mod.Better.Edicts
{
	internal partial class GenerelEdicts : IModData
	{
		private static void Cheats(ProtoRegistrator registrator)
		{
			// Add parent to my research CHEAT
			ResearchNodeProto master_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.VehicleCapIncreaseID_CHEAT);

			int cheatCost = BetterMod.Config.Default.UnlockAllCheatsResearches ? 0 : BetterMod.Config.Default.CheatResearchCosts;
			
			// Generate LocStr
			string name = LangManager.Instance.Get("generell_edict_cheat");

			// Generate Cheat Research
			ResearchNodeProto research_t1 = GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchCheat_T1, name+ " I", cheatCost, new ResearchNodeUIData(master_research, true, Constants.UIStepSize * 2, -Constants.UIStepSize),
				Eticts.Generell.UnityPointsT1_CHEAT,
				Eticts.Generell.ReduceServiceT1_CHEAT,
				Eticts.Generell.FarmMultiplierT1_CHEAT,
				Eticts.Generell.FarmWaterConsumMultiplierT1_CHEAT,
				Eticts.Generell.RecyclingRatioDiffT1_CHEAT,
				Eticts.Generell.SolarPowerT1_CHEAT);

			ResearchNodeProto research_t2 = GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchCheat_T2, name+ " II", cheatCost, research_t1, true,
				Eticts.Generell.UnityPointsT2_CHEAT,
				Eticts.Generell.ReduceServiceT2_CHEAT,
				Eticts.Generell.FarmMultiplierT2_CHEAT,
				Eticts.Generell.FarmWaterConsumMultiplierT2_CHEAT,
				Eticts.Generell.RecyclingRatioDiffT2_CHEAT,
				Eticts.Generell.SolarPowerT2_CHEAT);

			ResearchNodeProto research_t3 = GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchCheat_T3, name+ " III", cheatCost, research_t2, true,
				Eticts.Generell.UnityPointsT3_CHEAT,
				Eticts.Generell.ReduceServiceT3_CHEAT,
				Eticts.Generell.FarmMultiplierT3_CHEAT,
				Eticts.Generell.FarmWaterConsumMultiplierT3_CHEAT,
				Eticts.Generell.RecyclingRatioDiffT3_CHEAT,
				Eticts.Generell.SolarPowerT3_CHEAT);

			ResearchNodeProto research_t4 = GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchCheat_T4, name+ " IV", cheatCost, research_t3, true,
				Eticts.Generell.UnityPointsT4_CHEAT,
				Eticts.Generell.ReduceServiceT4_CHEAT,
				Eticts.Generell.FarmMultiplierT4_CHEAT,
				Eticts.Generell.FarmWaterConsumMultiplierT4_CHEAT,
				Eticts.Generell.RecyclingRatioDiffT4_CHEAT,
				Eticts.Generell.SolarPowerT4_CHEAT);

			GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchCheat_T5, name+ " V", cheatCost, research_t4, true,
				Eticts.Generell.UnityPointsT5_CHEAT,
				Eticts.Generell.ReduceServiceT5_CHEAT,
				Eticts.Generell.FarmMultiplierT5_CHEAT,
				Eticts.Generell.FarmWaterConsumMultiplierT5_CHEAT,
				Eticts.Generell.RecyclingRatioDiffT5_CHEAT,
				Eticts.Generell.SolarPowerT5_CHEAT);

			BetterDebug.Info("GenerelEdicts >> Generell edict cheats created!");
		}
	}
}