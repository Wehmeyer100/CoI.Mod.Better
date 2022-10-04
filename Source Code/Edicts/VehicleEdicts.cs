using CoI.Mod.Better.MyIDs;
using CoI.Mod.Better.Shared;
using CoI.Mod.Better.Shared.Lang;
using Mafi.Base;
using Mafi.Core.Mods;
using Mafi.Core.Research;
using static CoI.Mod.Better.Shared.Utilities.ResearchProtoUtility;

namespace CoI.Mod.Better.Edicts
{
	internal partial class VehicleEdicts : IModData
	{
		private const string translationComment       = "policy / edict which can enabled by the player in their Captain's office.";
		private       float  CheatUpkeepEdicts        = -0.5f;
		private       int    countTruckCapEdicts      = 1;
		private       int    countTruckFuelConsEdicts = 1;

		public void RegisterData(ProtoRegistrator registrator)
		{
			if (!BetterMod.Config.Systems.VehicleEdicts) return;

			CheatUpkeepEdicts = BetterMod.Config.Default.CheatUpkeepEdicts;

			AddTruckCap(registrator);
			AddTruckFuelCons(registrator);
			AddMaintenance(registrator);

			GenerateResearch(registrator);
		}

		private void GenerateResearch(ProtoRegistrator registrator)
		{
			// Generate LocStr
			string name = LangManager.Instance.Get("vehicle_edict_plus");
			
			ResearchNodeProto master_research = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.CaptainsOffice);

			ResearchNodeProto research_t1 = GenerateResearchEdict(registrator, MyIDs.Research.VehicleEdictsResearchT1, name + " I", BetterMod.Config.VehicleEdicts.ResearchCostT1, new ResearchNodeUIData(master_research, false, Constants.UIStepSize, Constants.UIStepSize * 5), Eticts.Trucks.CapacityIncT1, Eticts.Trucks.FuelReductionT1, Eticts.Trucks.MaintenanceReductionT1);

			ResearchNodeProto research_t2 = GenerateResearchEdict(registrator, MyIDs.Research.VehicleEdictsResearchT2, name + " II", BetterMod.Config.VehicleEdicts.ResearchCostT2, research_t1, false, Eticts.Trucks.CapacityIncT2, Eticts.Trucks.FuelReductionT2, Eticts.Trucks.MaintenanceReductionT2);

			ResearchNodeProto research_t3 = GenerateResearchEdict(registrator, MyIDs.Research.VehicleEdictsResearchT3, name + " III", BetterMod.Config.VehicleEdicts.ResearchCostT3, research_t2, false, Eticts.Trucks.CapacityIncT3, Eticts.Trucks.FuelReductionT3, Eticts.Trucks.MaintenanceReductionT3);

			ResearchNodeProto research_t4 =GenerateResearchEdict(registrator, MyIDs.Research.VehicleEdictsResearchT4, name + " IV", BetterMod.Config.VehicleEdicts.ResearchCostT4, research_t3, false, Eticts.Trucks.CapacityIncT4, Eticts.Trucks.FuelReductionT4, Eticts.Trucks.MaintenanceReductionT4);
			
			GenerateResearchEdict(registrator, MyIDs.Research.VehicleEdictsResearchT5, "Vehicle Edict Plus V", BetterMod.Config.VehicleEdicts.ResearchCostT4, research_t4, false);

			BetterDebug.Info("VehicleEdicts >> Vehicle Edict created!");

			if (BetterMod.Config.Systems.Cheats)
			{
				Cheats(registrator);
			}
		}
	}
}