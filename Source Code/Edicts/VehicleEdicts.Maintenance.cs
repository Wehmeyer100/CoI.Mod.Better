using CoI.Mod.Better.MyIDs;
using CoI.Mod.Better.Shared.Utilities;
using Mafi.Core;
using Mafi.Core.Mods;

namespace CoI.Mod.Better.Edicts
{
	internal partial class VehicleEdicts : IModData
	{
		private void AddMaintenance(ProtoRegistrator registrator)
		{
			EdictUtility.GenerateEdict2(registrator, Eticts.Trucks.MaintenanceReductionT1, GenerelEdicts.Category, "maintenance_reduction_t1", 2, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, -30, null, Mafi.Base.Assets.Base.Icons.Edicts.MaintenanceReduced_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Trucks.MaintenanceReductionT2, GenerelEdicts.Category, "maintenance_reduction_t2", 2.7f, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, -40, Eticts.Trucks.MaintenanceReductionT1, Mafi.Base.Assets.Base.Icons.Edicts.MaintenanceReduced_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Trucks.MaintenanceReductionT3, GenerelEdicts.Category, "maintenance_reduction_t3", 3.3f, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, -50, Eticts.Trucks.MaintenanceReductionT2, Mafi.Base.Assets.Base.Icons.Edicts.MaintenanceReduced_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Trucks.MaintenanceReductionT4, GenerelEdicts.Category, "maintenance_reduction_t4", 4, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, -60, Eticts.Trucks.MaintenanceReductionT3, Mafi.Base.Assets.Base.Icons.Edicts.MaintenanceReduced_svg);


			if (!BetterMod.Config.Systems.Cheats) return;

			// Add Cheats
			EdictUtility.GenerateEdict2(registrator, Eticts.Trucks.MaintenanceReductionT1_CHEAT, GenerelEdicts.CategoryCheats, "maintenance_reduction_t1_cheat", CheatUpkeepEdicts, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, -30, null, Mafi.Base.Assets.Base.Icons.Edicts.MaintenanceReduced_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Trucks.MaintenanceReductionT2_CHEAT, GenerelEdicts.CategoryCheats, "maintenance_reduction_t2_cheat", CheatUpkeepEdicts, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, -50, Eticts.Trucks.MaintenanceReductionT1_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.MaintenanceReduced_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Trucks.MaintenanceReductionT3_CHEAT, GenerelEdicts.CategoryCheats, "maintenance_reduction_t3_cheat", CheatUpkeepEdicts, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, -75, Eticts.Trucks.MaintenanceReductionT2_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.MaintenanceReduced_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Trucks.MaintenanceReductionT4_CHEAT, GenerelEdicts.CategoryCheats, "maintenance_reduction_t4_cheat", CheatUpkeepEdicts, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, -100, Eticts.Trucks.MaintenanceReductionT3_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.MaintenanceReduced_svg);

		}
	}
}