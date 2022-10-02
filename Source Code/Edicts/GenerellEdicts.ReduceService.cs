using CoI.Mod.Better.MyIDs;
using CoI.Mod.Better.Shared.Utilities;
using Mafi.Core;
using Mafi.Core.Mods;

namespace CoI.Mod.Better.Edicts
{
	internal partial class GenerelEdicts : IModData
	{
		private void AddReduceService(ProtoRegistrator registrator)
		{
			// Add Cheats
			if (!BetterMod.Config.Systems.Cheats) return;

			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.ReduceServiceT1_CHEAT, CategoryCheats, "reduce_service_t1", _cheatUpkeepEdicts, IdsCore.PropertyIds.FoodConsumptionMultiplier, 20, null, Mafi.Base.Assets.Base.Icons.Edicts.FoodReduced_png);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.ReduceServiceT2_CHEAT, CategoryCheats, "reduce_service_t2", _cheatUpkeepEdicts, IdsCore.PropertyIds.FoodConsumptionMultiplier, 40, Eticts.Generell.ReduceServiceT1_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FoodReduced_png);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.ReduceServiceT3_CHEAT, CategoryCheats, "reduce_service_t3", _cheatUpkeepEdicts, IdsCore.PropertyIds.FoodConsumptionMultiplier, 60, Eticts.Generell.ReduceServiceT2_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FoodReduced_png);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.ReduceServiceT4_CHEAT, CategoryCheats, "reduce_service_t4", _cheatUpkeepEdicts, IdsCore.PropertyIds.FoodConsumptionMultiplier, 80, Eticts.Generell.ReduceServiceT3_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FoodReduced_png);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.ReduceServiceT5_CHEAT, CategoryCheats, "reduce_service_t5", _cheatUpkeepEdicts, IdsCore.PropertyIds.FoodConsumptionMultiplier, 100, Eticts.Generell.ReduceServiceT4_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FoodReduced_png);
		}
	}
}