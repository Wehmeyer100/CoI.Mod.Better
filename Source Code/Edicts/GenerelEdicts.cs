using CoI.Mod.Better.MyIDs;
using CoI.Mod.Better.Shared;
using Mafi.Base;
using Mafi.Core.Mods;
using Mafi.Core.Population.Edicts;
using Mafi.Core.Prototypes;
using Mafi.Core.Research;
using static CoI.Mod.Better.Shared.Utilities.ResearchProtoUtility;

namespace CoI.Mod.Better.Edicts
{
	internal partial class GenerelEdicts : IModData
	{
		public static EdictCategoryProto Category;
		public static EdictCategoryProto CategoryCheats;
		private       float              _cheatUpkeepEdicts = -0.5f;

		public void RegisterData(ProtoRegistrator registrator)
		{
			if (!BetterMod.Config.Systems.GenerellEdicts) return;

			_cheatUpkeepEdicts = BetterMod.Config.Default.CheatUpkeepEdicts;
			Category = registrator.PrototypesDb.Add(new EdictCategoryProto(Eticts.BetterMod, Proto.CreateStr(Eticts.BetterMod, "Better mod")));
			CategoryCheats = registrator.PrototypesDb.Add(new EdictCategoryProto(Eticts.BetterModCheats, Proto.CreateStr(Eticts.BetterModCheats, "Better mod: Cheats")));

			AddUnityPoints(registrator);
			AddReduceService(registrator);
			AddFarmMultiplier(registrator);
			AddFarmWaterConsumMultiplier(registrator);
			AddRecyclingRatioDiff(registrator);
			AddSolarPower(registrator);

			GenerateResearch(registrator);
		}

		private void GenerateResearch(ProtoRegistrator registrator)
		{
			ResearchNodeProto masterResearch = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.CaptainsOffice);

			ResearchNodeProto researchT1 = GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchT1, "Generell Edict I", BetterMod.Config.GenerellEdicts.ResearchCostT1, new ResearchNodeUIData(masterResearch, false, Constants.UIStepSize, Constants.UIStepSize * 2));
			ResearchNodeProto researchT2 = GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchT2, "Generell Edict II", BetterMod.Config.GenerellEdicts.ResearchCostT2, researchT1, false);
			ResearchNodeProto researchT3 = GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchT3, "Generell Edict III", BetterMod.Config.GenerellEdicts.ResearchCostT3, researchT2, false);
			ResearchNodeProto researchT4 = GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchT4, "Generell Edict IV", BetterMod.Config.GenerellEdicts.ResearchCostT4, researchT3, false);
			GenerateResearchEdict(registrator, MyIDs.Research.GenerellEdictsResearchT5, "Generell Edict V", BetterMod.Config.GenerellEdicts.ResearchCostT5, researchT4, false);

			BetterDebug.Info("GenerelEdicts >> Generell Edict created!");

			if (BetterMod.Config.Systems.Cheats)
			{
				Cheats(registrator);
			}
		}
	}
}