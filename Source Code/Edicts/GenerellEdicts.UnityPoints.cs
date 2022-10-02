using CoI.Mod.Better.MyIDs;
using CoI.Mod.Better.Shared.Utilities;
using Mafi.Core;
using Mafi.Core.Mods;

namespace CoI.Mod.Better.Edicts
{
	internal partial class GenerelEdicts : IModData
	{
		private void AddUnityPoints(ProtoRegistrator registrator)
		{
			// Add Cheats
			if (!BetterMod.Config.Systems.Cheats) return;

			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.UnityPointsT1_CHEAT, CategoryCheats, "unity_points_t1", -5, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, 0, null, Mafi.Base.Assets.Base.Icons.Edicts.UnityIncreased_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.UnityPointsT2_CHEAT, CategoryCheats, "unity_points_t2", -10, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, 0, Eticts.Generell.UnityPointsT1_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.UnityIncreased_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.UnityPointsT3_CHEAT, CategoryCheats, "unity_points_t3", -20, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, 0, Eticts.Generell.UnityPointsT2_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.UnityIncreased_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.UnityPointsT4_CHEAT, CategoryCheats, "unity_points_t4", -50, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, 0, Eticts.Generell.UnityPointsT3_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.UnityIncreased_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.UnityPointsT5_CHEAT, CategoryCheats, "unity_points_t5", -100, IdsCore.PropertyIds.MaintenanceConsumptionMultiplier, 0, Eticts.Generell.UnityPointsT4_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.UnityIncreased_svg);
		}
	}
}