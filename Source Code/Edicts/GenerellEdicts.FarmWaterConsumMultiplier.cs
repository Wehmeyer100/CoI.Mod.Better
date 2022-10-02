using CoI.Mod.Better.MyIDs;
using CoI.Mod.Better.Shared.Utilities;
using Mafi.Core;
using Mafi.Core.Mods;

namespace CoI.Mod.Better.Edicts
{
	internal partial class GenerelEdicts : IModData
	{
		private void AddFarmWaterConsumMultiplier(ProtoRegistrator registrator)
		{
			if (!BetterMod.Config.Systems.Cheats) return;

			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.FarmWaterConsumMultiplierT1_CHEAT, CategoryCheats, "farm_consume_multiplier_t1", _cheatUpkeepEdicts, IdsCore.PropertyIds.FarmWaterConsumptionMultiplier, -20, null, Mafi.Base.Assets.Base.Icons.Edicts.FarmingBoost_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.FarmWaterConsumMultiplierT2_CHEAT, CategoryCheats, "farm_consume_multiplier_t2", _cheatUpkeepEdicts, IdsCore.PropertyIds.FarmWaterConsumptionMultiplier, -40, Eticts.Generell.FarmWaterConsumMultiplierT1_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FarmingBoost_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.FarmWaterConsumMultiplierT3_CHEAT, CategoryCheats, "farm_consume_multiplier_t3", _cheatUpkeepEdicts, IdsCore.PropertyIds.FarmWaterConsumptionMultiplier, -50, Eticts.Generell.FarmWaterConsumMultiplierT2_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FarmingBoost_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.FarmWaterConsumMultiplierT4_CHEAT, CategoryCheats, "farm_consume_multiplier_t4", _cheatUpkeepEdicts, IdsCore.PropertyIds.FarmWaterConsumptionMultiplier, -75, Eticts.Generell.FarmWaterConsumMultiplierT3_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FarmingBoost_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.FarmWaterConsumMultiplierT5_CHEAT, CategoryCheats, "farm_consume_multiplier_t5", _cheatUpkeepEdicts, IdsCore.PropertyIds.FarmWaterConsumptionMultiplier, -95, Eticts.Generell.FarmWaterConsumMultiplierT4_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FarmingBoost_svg);
		}
	}
}