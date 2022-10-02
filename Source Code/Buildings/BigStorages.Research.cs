using CoI.Mod.Better.lang;
using CoI.Mod.Better.Shared;
using Mafi.Core.Mods;
using Mafi.Core.Research;
using static CoI.Mod.Better.Shared.Utilities.ResearchProtoUtility;

namespace CoI.Mod.Better.Buildings
{
	internal partial class BigStorages : IModData
	{
		private void GenerateResearch(ProtoRegistrator registrator)
		{
			if (!BetterMod.Config.Storage.OverrideVanilla)
			{
				BetterDebug.Info("BigStorages >> GenerateResearches...");
				string Name = LangManager.Instance.Get("research_storage");

				ResearchNodeProto parent = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.VehicleCapIncreaseID_ZERO);

				ResearchNodeProto research_t1 = GenerateResearchBuildings(registrator, MyIDs.Research.StorageResearchT1, Name + " I", "", 1, new ResearchNodeUIData(parent, false, 0, Constants.UIStepSize * 2), MyIDs.Buildings.StorageFluidT1, MyIDs.Buildings.StorageLooseT1, MyIDs.Buildings.StorageUnitT1);
				ResearchNodeProto research_t2 = GenerateResearchBuildings(registrator, MyIDs.Research.StorageResearchT2, Name + " II", "", 4, new ResearchNodeUIData(research_t1, false), MyIDs.Buildings.StorageFluidT2, MyIDs.Buildings.StorageLooseT2, MyIDs.Buildings.StorageUnitT2);
				ResearchNodeProto research_t3 = GenerateResearchBuildings(registrator, MyIDs.Research.StorageResearchT3, Name + " III", "", 8, new ResearchNodeUIData(research_t2, false), MyIDs.Buildings.StorageFluidT3, MyIDs.Buildings.StorageLooseT3, MyIDs.Buildings.StorageUnitT3);
				GenerateResearchBuildings(registrator, MyIDs.Research.StorageResearchT4, Name + " IV", "", 16, new ResearchNodeUIData(research_t3, false), MyIDs.Buildings.StorageFluidT4, MyIDs.Buildings.StorageLooseT4, MyIDs.Buildings.StorageUnitT4);

				BetterDebug.Info("BigStorages >> GenerateResearches... done.");
			}
		}
	}
}