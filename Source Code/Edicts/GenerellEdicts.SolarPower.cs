using CoI.Mod.Better.MyIDs;
using CoI.Mod.Better.Shared.Utilities;
using Mafi.Core;
using Mafi.Core.Mods;

namespace CoI.Mod.Better.Edicts
{
	internal partial class GenerelEdicts : IModData
	{
		private void AddSolarPower(ProtoRegistrator registrator)
		{
			// Add Cheats
			if (!BetterMod.Config.Systems.Cheats) return;

			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.SolarPowerT1_CHEAT, CategoryCheats, "solar_power_t1", _cheatUpkeepEdicts, IdsCore.PropertyIds.SolarPowerMultiplier, 25, null, Mafi.Base.Assets.Base.Icons.Edicts.SolarBoost_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.SolarPowerT2_CHEAT, CategoryCheats, "solar_power_t2", _cheatUpkeepEdicts, IdsCore.PropertyIds.SolarPowerMultiplier, 50, Eticts.Generell.SolarPowerT1_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.SolarBoost_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.SolarPowerT3_CHEAT, CategoryCheats, "solar_power_t3", _cheatUpkeepEdicts, IdsCore.PropertyIds.SolarPowerMultiplier, 100, Eticts.Generell.SolarPowerT2_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.SolarBoost_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.SolarPowerT4_CHEAT, CategoryCheats, "solar_power_t4", _cheatUpkeepEdicts, IdsCore.PropertyIds.SolarPowerMultiplier, 200, Eticts.Generell.SolarPowerT3_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.SolarBoost_svg);
			EdictUtility.GenerateEdict2(registrator, Eticts.Generell.SolarPowerT5_CHEAT, CategoryCheats, "solar_power_t5", _cheatUpkeepEdicts, IdsCore.PropertyIds.SolarPowerMultiplier, 300, Eticts.Generell.SolarPowerT4_CHEAT, Mafi.Base.Assets.Base.Icons.Edicts.SolarBoost_svg);
		}
	}
}