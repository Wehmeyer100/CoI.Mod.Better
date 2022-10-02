using CoI.Mod.Better.lang;
using CoI.Mod.Better.Shared;
using Mafi.Base;
using Mafi.Core.Mods;
using Mafi.Core.Research;
using static CoI.Mod.Better.Shared.Utilities.ResearchProtoUtility;

namespace CoI.Mod.Better.Buildings
{
	public partial class SteamStorages : IModData
	{
		private void GenerateResearch(ProtoRegistrator registrator)
		{
			string Name = LangManager.Instance.Get("research_steam_storage");

			ResearchNodeProto parent_t1 = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.PowerGeneration2);
			ResearchNodeProto research_t1 = GenerateResearchBuildings(registrator, MyIDs.Research.SteamStorageResearchT1, Name + " I", "", 5, new ResearchNodeUIData(parent_t1, false, Constants.UIStepSize, Constants.UIStepSize * 5), MyIDs.Buildings.StorageSteamT1);
			GenerateResearchBuildings(registrator, MyIDs.Research.SteamStorageResearchT2, Name + " II", "", 7, new ResearchNodeUIData(research_t1, false, Constants.UIStepSize * 2, 0), MyIDs.Buildings.StorageSteamT2);

			ResearchNodeProto parent_t2 = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.PowerGeneration3);
			ResearchNodeProto research_t3 = GenerateResearchBuildings(registrator, MyIDs.Research.SteamStorageResearchT3, Name + " III", "", 14, new ResearchNodeUIData(parent_t2, false, Constants.UIStepSize, Constants.UIStepSize * 5), MyIDs.Buildings.StorageSteamT3);
			GenerateResearchBuildings(registrator, MyIDs.Research.SteamStorageResearchT4, Name + " IV", "", 20, new ResearchNodeUIData(research_t3, false, Constants.UIStepSize * 6, 0), MyIDs.Buildings.StorageSteamT4);
		}
	}
}