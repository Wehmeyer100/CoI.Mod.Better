using CoI.Mod.Better.MyIDs;
using CoI.Mod.Better.Shared.Utilities;
using Mafi.Core;
using Mafi.Core.Mods;

namespace CoI.Mod.Better.Edicts
{
	internal partial class GenerelEdicts : IModData
	{
		private void AddFarmMultiplier(ProtoRegistrator registrator)
		{
			if (!BetterMod.Config.Systems.Cheats) return;

			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.FarmMultiplierT1_CHEAT, CategoryCheats, "farm_multiplier_t1", _cheatUpkeepEdicts, IdsCore.PropertyIds.FarmYieldMultiplier, 25, null, Mafi.Base.Assets.Base.Icons.Edicts.FarmingBoost_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.FarmMultiplierT2_CHEAT, CategoryCheats, "farm_multiplier_t2", _cheatUpkeepEdicts, IdsCore.PropertyIds.FarmYieldMultiplier, 50, Eticts.Generell.FarmMultiplierT1_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FarmingBoost_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.FarmMultiplierT3_CHEAT, CategoryCheats, "farm_multiplier_t3", _cheatUpkeepEdicts, IdsCore.PropertyIds.FarmYieldMultiplier, 100, Eticts.Generell.FarmMultiplierT2_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FarmingBoost_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.FarmMultiplierT4_CHEAT, CategoryCheats, "farm_multiplier_t4", _cheatUpkeepEdicts, IdsCore.PropertyIds.FarmYieldMultiplier, 200, Eticts.Generell.FarmMultiplierT3_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FarmingBoost_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.FarmMultiplierT5_CHEAT, CategoryCheats, "farm_multiplier_t5", _cheatUpkeepEdicts, IdsCore.PropertyIds.FarmYieldMultiplier, 300, Eticts.Generell.FarmMultiplierT4_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.FarmingBoost_svg);
		}
	}
}