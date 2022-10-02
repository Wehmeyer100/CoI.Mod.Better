using CoI.Mod.Better.MyIDs;
using CoI.Mod.Better.Shared.Utilities;
using Mafi.Core;
using Mafi.Core.Mods;

namespace CoI.Mod.Better.Edicts
{
	internal partial class GenerelEdicts : IModData
	{
		private void AddRecyclingRatioDiff(ProtoRegistrator registrator)
		{
			if (!BetterMod.Config.Systems.Cheats) return;

			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.RecyclingRatioDiffT1_CHEAT, CategoryCheats, "recycling_ratio_t1", _cheatUpkeepEdicts, IdsCore.PropertyIds.RecyclingRatioDiff, 20, null, Mafi.Base.Assets.Base.Icons.Edicts.RecyclingIncrease2_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.RecyclingRatioDiffT2_CHEAT, CategoryCheats, "recycling_ratio_t2", _cheatUpkeepEdicts, IdsCore.PropertyIds.RecyclingRatioDiff, 40, Eticts.Generell.RecyclingRatioDiffT1_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.RecyclingIncrease2_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.RecyclingRatioDiffT3_CHEAT, CategoryCheats, "recycling_ratio_t3", _cheatUpkeepEdicts, IdsCore.PropertyIds.RecyclingRatioDiff, 60, Eticts.Generell.RecyclingRatioDiffT2_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.RecyclingIncrease2_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.RecyclingRatioDiffT4_CHEAT, CategoryCheats, "recycling_ratio_t4", _cheatUpkeepEdicts, IdsCore.PropertyIds.RecyclingRatioDiff, 80, Eticts.Generell.RecyclingRatioDiffT3_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.RecyclingIncrease2_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.RecyclingRatioDiffT5_CHEAT, CategoryCheats, "recycling_ratio_t5", _cheatUpkeepEdicts, IdsCore.PropertyIds.RecyclingRatioDiff, 100, Eticts.Generell.RecyclingRatioDiffT4_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.RecyclingIncrease2_svg);
		}
	}
}