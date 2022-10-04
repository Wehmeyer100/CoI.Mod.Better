using CoI.Mod.Better.MyIDs;
using CoI.Mod.Better.Shared;
using CoI.Mod.Better.Shared.Lang;
using Mafi.Core.Mods;
using Mafi.Core.Research;
using static CoI.Mod.Better.Shared.Utilities.ResearchProtoUtility;

namespace CoI.Mod.Better.Edicts
{
	internal partial class VehicleEdicts : IModData
	{
		private static void Cheats(ProtoRegistrator registrator)
		{
			ResearchNodeProto master_cheat_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(MyIDs.Research.VehicleCapIncreaseID_CHEAT);

			// Generate LocStr
			string name = LangManager.Instance.Get("vehicle_edict_plus_cheat");
			int cheatCost = BetterMod.Config.Default.UnlockAllCheatsResearches ? 0 : BetterMod.Config.Default.CheatResearchCosts;
			ResearchNodeProto research_cheat_t1 = GenerateResearchEdict(registrator, MyIDs.Research.VehicleEdictsResearchCheat_T1, name + " I", cheatCost, new ResearchNodeUIData(master_cheat_research, true, Constants.UIStepSize * 2, 0), Eticts.Trucks.CapacityIncT1_CHEAT, Eticts.Trucks.FuelReductionT1_CHEAT, Eticts.Trucks.MaintenanceReductionT1_CHEAT);
			ResearchNodeProto research_cheat_t2 = GenerateResearchEdict(registrator, MyIDs.Research.VehicleEdictsResearchCheat_T2, name + " II", cheatCost, research_cheat_t1, true, Eticts.Trucks.CapacityIncT2_CHEAT, Eticts.Trucks.FuelReductionT2_CHEAT, Eticts.Trucks.MaintenanceReductionT2_CHEAT);
			ResearchNodeProto research_cheat_t3 = GenerateResearchEdict(registrator, MyIDs.Research.VehicleEdictsResearchCheat_T3, name + " III", cheatCost, research_cheat_t2, true, Eticts.Trucks.CapacityIncT3_CHEAT, Eticts.Trucks.FuelReductionT3_CHEAT, Eticts.Trucks.MaintenanceReductionT3_CHEAT);
			ResearchNodeProto research_cheat_t4 = GenerateResearchEdict(registrator, MyIDs.Research.VehicleEdictsResearchCheat_T4, name + " IV", cheatCost, research_cheat_t3, true, Eticts.Trucks.CapacityIncT4_CHEAT, Eticts.Trucks.FuelReductionT4_CHEAT, Eticts.Trucks.MaintenanceReductionT4_CHEAT);
			GenerateResearchEdict(registrator, MyIDs.Research.VehicleEdictsResearchCheat_T5, name + " V", cheatCost, research_cheat_t4, true, Eticts.Trucks.CapacityIncT5_CHEAT);

			BetterDebug.Info("VehicleEdicts >> Vehicle Edict cheats created!");
		}
	}
}